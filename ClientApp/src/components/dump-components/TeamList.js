import React from 'react';

const TeamList = props => {
    if(props.team.length > 0){
        return (
            <div>
                <ul>
                    {props.team.map(employee => (
                        <li key={employee.id}>
                            <a href={"/profile?id=" + employee.id}>{employee.firstName + " " + employee.lastName}</a>
                        </li>
                    ))}
                </ul>
            </div>
        )
    }else{
        <div></div>
    }
    
};

export default TeamList;