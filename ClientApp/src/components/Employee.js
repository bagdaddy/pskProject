﻿import React from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import * as qs from 'query-string';

//Tures pakeisti api callas
const user = {
    name: "Justas Kondroska",
    id: 0,
    team: [
        {
            name: "Matas Krivaitis",
            id: 1
        },
        {
            name: "Efkas Jonas",
            id: 2
        }
    ],
    subjects: [
        {
            parent: null,
            name: "PHP",
            id: 1
        },
        {
            parent: 1,
            name: "PHP7",
            id: 2
        },
        {
            parent: null,
            name: "WordPress",
            id: 3
        }
    ]
};

const Employee = props => {
    const parsed = qs.parse(window.location.search);
    let userProfile = user;
    
    if (userProfile) {
        return (
            <div>
                <h2>Sveiki, {userProfile.name}</h2>
                <div className="row">
                    <div className="col-lg-6 col-md-6 sidebar right">
                        {userProfile.team.length > 0 && (
                            <div>
                                <h3>Jūsų komanda:</h3>
                                <TeamList team={userProfile.team} />
                            </div>
                        )}
                        {userProfile.subjects.length > 0 && (
                            <div>
                                <h3>Jūsų išmoktos temos: </h3>
                                <SubjectList subjects={userProfile.subjects} />
                            </div>
                        )}

                    </div>
                </div>
            </div>
        );
    } else {
        return (
            <div>
                <h2>Šio profilio matyti negalite</h2>
            </div>
        )
    }

};

// ReactDOM.render(
//     <Employee/>,
//     document.getElementById("profile")
// );

export default Employee;

