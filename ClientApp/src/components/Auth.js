

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
            window.location.href = "/login";
        }
        return userResponse;
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