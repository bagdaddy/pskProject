import React, { useState, useEffect } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';


function Profile(props) {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);

    const fetchData = React.useCallback(() => {
        fetch('api/Auth/self')
            .then(response => response.json())
            .then(data => setEmployee(data))
            .catch(error => {
                console.log(error);
            });
    });

    const fetchAllEmployees = React.useCallback(() => {
        fetch('api/Employees')
            .then(response => response.json())
            .then(data => setEmployees(data));
    });

    useEffect(() => {
        fetchData();
        fetchAllEmployees();
    }, []);


    if (employee) {
        return (
            <div>
                <div className="row">
                    <div className="col-8">
                        <h2>{employee.firstName} {employee.lastName}</h2>
                        <p>El. paštas: <a href={"mailto:" + employee.email}>{employee.email}</a></p>

                    </div>
                    <div className="col-lg-4 col-md-4 sidebar">
                        {employee.hasOwnProperty("team") && employee.team.length > 0 && (
                            <div>
                                <h3>Your team:</h3>
                                <TeamList team={employee.team} />
                            </div>
                        )}
                        {employee.hasOwnProperty("subjects") && employee.subjects.length > 0 && (
                            <div>
                                <h3>Your subjects: </h3>
                                <SubjectList subjects={employee.subjects} />
                            </div>
                        )}
                        {allEmployees.length > 0 && (
                            <div>
                                <h5>Visų darbuotojų sąrašas testavimo sumetimais: </h5>
                                <TeamList team={allEmployees} />
                            </div>
                        )}
                    </div>
                </div>
            </div>
        );
    }
};

export default Profile;