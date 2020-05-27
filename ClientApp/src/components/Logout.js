import React from 'react';
import auth from './Auth';

const Logout = props => {
    console.log("logout");
    auth.logout(() => {
        props.history.push("/login");
    });
    return(
        <div>Logging out...</div>
    )
}

export default Logout;