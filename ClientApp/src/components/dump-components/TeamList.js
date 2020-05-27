import React from 'react';

const TeamList = props => {
    if (props.team.length > 0) {
        return (
            <div>
                <ul>
                    {props.team.map(employee => (
                        <li key={employee.id}>
                            <a href={"/employee/" + employee.id}>{employee.firstName + " " + employee.lastName}</a>
                        </li>
                    ))}
                </ul>
            </div>
        )
    } else {
        return (
            <div></div>
        )
    }

};

export default TeamList;