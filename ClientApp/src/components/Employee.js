import React, { useEffect, useState } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import NotAuthorized from './dump-components/Error';

sessionStorage.setItem('userId', '5a677c6e-56e5-4cf1-9c64-157b483e8eff');

function Employee (props) {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);

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
        console.log("nice");
        if (props.match.params.id) {
            fetchData(props.match.params.id);
        } else {
            console.log("haha");
            fetchData(sessionStorage.getItem('userId'));
        }
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
                                <h3>{employee.firstName} team:</h3>
                                <TeamList team={employee.team} />
                            </div>
                        )}
                        {employee.hasOwnProperty("subjects") && employee.subjects.length > 0 && (
                            <div className="subjects">
                                <h3>{employee.firstName} has learned: </h3>
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

