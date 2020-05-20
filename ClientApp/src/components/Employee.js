import React, { useEffect, useState } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import * as qs from 'query-string';

let authenticatedUserId = "5a677c6e-56e5-4cf1-9c64-157b483e8eff";

const Employee = props => {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);
    const parsed = qs.parse(window.location.search);

    const fetchData = React.useCallback((id) => {
        fetch('api/Employees/' + id)
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
        if (Object.keys(parsed).length > 0) {
            fetchData(parsed.id);
        } else {
            fetchData(authenticatedUserId);
        }
        fetchAllEmployees();
    }, []);
    console.log(allEmployees);

    if (employee) {
        return (
            <div>
                <h2>{employee.firstName} {employee.lastName}</h2>
                <p>El. paštas: <a href={"mailto:" + employee.email}>{employee.email}</a></p>
                <div className="row">
                    <div className="col-lg-6 col-md-6 sidebar right">
                        {employee.hasOwnProperty("team") && employee.team.length > 0 && (
                            <div>
                                <h3>Jūsų komanda:</h3>
                                <TeamList team={employee.team} />
                            </div>
                        )}
                        {employee.hasOwnProperty("subject") && employee.subjects.length > 0 && (
                            <div>
                                <h3>Jūsų išmoktos temos: </h3>
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

