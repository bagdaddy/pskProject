class Auth {
    constructor(){
        this.authenticated = false;
    }

    login(requestOptions, cb){
        fetch('api/Auth/login', requestOptions)
            .then(response => {
                if(response.ok){
                    this.authenticated = true;
                    cb();
                }
            });
    }

    logout(cb){
        const requestOptions = {
            method: 'POST'
        };
        fetch('api/Auth/Logout', requestOptions)
            .then(response => {
                console.log(response);
                this.authenticated = false;
                cb();
            })
    }

    isAuthenticated() {
        return this.authenticated;
    }
}

export default new Auth();