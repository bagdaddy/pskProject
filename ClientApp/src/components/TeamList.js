import React from 'react';

const TeamList = props => {
    if(props.team.length > 0){
        return (
            <div>
                <h3>Jūsų komanda:</h3>
                <ul>
                    {props.team.map(employee => (
                        <li key={employee.id}>
                            <a href={"/profile?id=" + employee.id}>{employee.name}</a>
                        </li>
                    ))}
                </ul>
            </div>
        )
    }else{

    }
    
};

export default TeamList;