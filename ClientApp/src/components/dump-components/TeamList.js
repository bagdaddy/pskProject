import React from 'react';

const TeamList = props => {
    if (props.team.length > 0) {
        return (
            <div className="teamList">
                {props.team.map(employee => (
                    <a href={"/employee/" + employee.id}>{employee.firstName + " " + employee.lastName}</a>
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