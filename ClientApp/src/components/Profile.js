import React, { useState, useRef, useEffect } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import { Form, FormGroup, Label, Input, Button } from 'reactstrap';


function Profile(props) {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);
    const [subjects, setSubjects] = useState([]);
    const [selectedSubjectId, setSelectedSubjectId] = useState("");
    
    let success = useRef();

    const fetchData = React.useCallback(() => {
        fetch('api/Auth/self')
            .then(response => response.json())
            .then(employeeData => {
                setEmployee(employeeData);
                fetch('api/GetAllSubjects')
                    .then(response => response.json())
                    .then(data => {
                        let subjects = [];
                        if (employeeData.subjects.length == 0) {
                            subjects = data;
                        } else {
                            data.forEach(subject => {
                                if (employeeData.subjects.filter(s => s.id === subject.id).length > 0) {
                                    subjects.push(subject);
                                }
                            });
                        }
                        setSubjects(subjects);
                    });
            })
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
        // fetchSubjects();
    }, []);

    const learnSubject = (event) => {
        event.preventDefault();
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                subjectId: selectedSubjectId
            })
        };

        fetch('api/Employee/learnSubject', requestOptions)
            .then(response => {
                if (response.ok) {
                    success.current.style.display = "block";
                }
            });
    };


    if (employee) {
        return (
            <div>
                <div className="row">
                    <div className="col-8">
                        <h2>{employee.firstName} {employee.lastName}</h2>
                        <p>El. paštas: <a href={"mailto:" + employee.email}>{employee.email}</a></p>
                        <div className="row">
                            <div className="col-8">
                                <Form onSubmit={learnSubject}>
                                    <FormGroup onSubmit={learnSubject}>
                                        <Label for="subject">Selet a subject you've learnt: </Label>
                                        <Input type="select" name="subject" id="subject" onChange={(event) => setSelectedSubjectId(event.target.value)}>
                                            {subjects.map((subject) => (
                                                <option value={subject.id}>{subject.name}</option>
                                            ))}
                                        </Input>
                                        <label ref={success} className="successMsg">Subject successfully added.</label>
                                    </FormGroup>
                                    <Button>Submit</Button>
                                </Form>
                            </div>
                        </div>
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