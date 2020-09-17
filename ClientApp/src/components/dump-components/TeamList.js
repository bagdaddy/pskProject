import React from 'react';

const TeamList = props => {
    if (props.team.length > 0) {
        return (
            <div className={props.wrapperClass}>
                {props.team.map(employee => (
                    props.currentUser == employee.id ? 
                    <a key={employee.id} onClick={() => props.history.push("/me")}>{employee.firstName + " " + employee.lastName}</a>
                    : <a key={employee.id} onClick={() => props.history.push("/employee/" + employee.id)}>{employee.firstName + " " + employee.lastName}</a>
                ))}
            </div>
        )
    } else {
        return (
            <div></div>
        )
    }

};

export default TeamList;