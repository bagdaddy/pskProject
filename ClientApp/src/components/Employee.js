import React, { useEffect, useState } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import * as qs from 'query-string';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';

let authenticatedUserId = "5a677c6e-56e5-4cf1-9c64-157b483e8eff";

function Employee() {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);
    const [allSubjects, setAllSubjects] = useState([]);
    const parsed = qs.parse(window.location.search);

    let isAuthenticatedUser = false;

    if(Object.keys(parsed).length === 0){
        isAuthenticatedUser = true;
    }

    const fetchData = React.useCallback((id) => {
        fetch('api/Employees/' + id)
            .then(response => response.json())
            .then(data => setEmployee(data))
            .catch(error => {
                console.log(error);
            });
    });

    const fetchAllSubjets = React.useCallback(() => {
        fetch('api/GetSubjects')
            .then(response => response.json())
            .then(data => setAllSubjects(data));
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
            isAuthenticatedUser = true;
            fetchData(authenticatedUserId);
        }
        fetchAllEmployees();
        fetchAllSubjets();
    }, []);

    useEffect(() => {
        var parse = qs.parse(window.location.search);
        console.log(parse);
        if (Object.keys(parse).length > 0 && parse.id !== employee.id) {
            fetchData(parse.id);
        } else {
            if (employee.id != authenticatedUserId) {
                fetchData(authenticatedUserId);
            }
        }
    }, [window.location.search])


    if (employee) {
        return (
            <div>

                <div className="row">
                    <div className="col-lg-8 col-md-8">
                        <h2>{employee.firstName} {employee.lastName}</h2>
                        <p>Email: <a href={"mailto:" + employee.email}>{employee.email}</a></p>
                        <div className="add-goal">
                            <h3>{isAuthenticatedUser ? 'Give yourself a goal' : 'Give ' + employee.name + ' a goal'}</h3>
                            <Form>
                                <FormGroup>
                                    <Label for="subject">Choose subject</Label>
                                    <Input type="select" name="subject" id="subject">
                                        {allSubjects.map(subject => (
                                            <option value={subject.id}>{subject.name}</option>
                                        ))}
                                    </Input>
                                </FormGroup>
                                <FormGroup>
                                    <Button className="btn btn-success">Submit</Button>
                                </FormGroup>

                            </Form>
                        </div>
                    </div>
                   
                    <div className="col-lg-4 col-md-4 sidebar right">
                        {employee.hasOwnProperty("team") && employee.team.length > 0 && (
                            <div>
                                <h3>Your team: </h3>
                                <TeamList team={employee.team} />
                            </div>
                        )}
                        {employee.hasOwnProperty("subject") && employee.subjects.length > 0 && (
                            <div>
                                <h3>Subjects you've learned </h3>
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

