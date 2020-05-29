import React from 'react';

export const NotFound = props => {
    return(
        <div className="notfound">
            <h1>Oops!</h1>
            <p>The page you were looking for doesn't exist...</p>
        </div>
    )
};

export const NotAuthorized = props => {
    return(
        <div className="notfound">
            <h1>Sorry...</h1>
            <p>You are not authorized to access this page.</p>
        </div>
    )
}