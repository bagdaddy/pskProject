

import Cookies from 'universal-cookie';

const cookies = new Cookies();
class Auth {
    login(requestOptions, cb){
        fetch('api/Auth/login', requestOptions)
            .then(response => {
                if(response.ok){
                    this.authenticated = true;
                    cookies.set("user_logged_in", true, {path: '/'})
                    cb();
                }else{
                    window.location.href = "/login?errors=true";
                }
            });
    }

    logout(cb){
        console.log("logout");
        const requestOptions = {
            method: 'POST'
        };
        fetch('api/Auth/Logout', requestOptions)
            .then(response => {
                cookies.remove("user_logged_in", {path: '/'});
                this.authenticated = false;
                cb();
            })
    }

    async getCurrentUser(){
        let userResponse = await fetch('api/Auth/self')
        if(userResponse.status === 401){
            this.logout(() => {
                window.location.href = "/login";
            })
        }
        return userResponse;
    }

    async isTeamLeader(){
        let userResponse = await fetch('api/Auth/self');
        if(userResponse.status === 401){
            return false;
        }
        let userData = await userResponse.json();
        let teamResponse = await fetch('api/GetTeams/' + userData.id);
        if(teamResponse.ok){
            let teamData = await teamResponse.json();
            if(await teamData[0].subordinates !== undefined){
                return teamData[0].subordinates.length > 0;
            }else{
                return false;
            }
        }
    }

    isAuthenticated() {
        let cookie = cookies.get("user_logged_in");
        if(cookie){
            return true;
        }else{
            return false;
        }
    }
}

export default new Auth();