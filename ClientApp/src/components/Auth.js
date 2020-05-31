

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
                cookies.remove("user_logged_in");
                this.authenticated = false;
                cb();
            })
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