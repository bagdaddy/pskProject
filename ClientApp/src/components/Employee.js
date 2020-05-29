﻿import React, { useEffect, useState, useRef } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import Loader from './dump-components/Loader';
import { NotFound } from './dump-components/Error';
import { Form, FormGroup, Label, Input, Button, FormFeedback } from 'reactstrap';
import getFlatListOfSubordinates from './dump-components/getSubordinates';


function fetchEmployeeData(id) {
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);

    let success = useRef();
    async function fetchEmployees(id) {
        const response = await fetch('api/GetTeams/' + id);
        if (response.ok) {
            const employees = await response.json();
            return await employees[0].subordinates;
        } else {
            return null;
        }
    }

    async function fetchGoals(id) {
        const response = await fetch('api/Goals/' + id);
        if (response.ok) {
            return await response.json();
        } else {
            return [];
        }
    }

    async function fetchMyEmployees() {
        const res = await fetch('api/Auth/self');
        if (res.ok) {
            const me = await res.json();
            const response = await fetch('api/GetTeams/' + me.id);
            if (response.ok) {
                const employees = await response.json();

                return await employees[0].subordinates;
            } else {
                return [];
            }
        }
    }

    async function fetchSubjects() {
        const response = await fetch('api/GetAllSubjects');
        const json = await response.json();
        return json;
    }

    async function fetchData(id) {
        const response = await fetch('api/Employees/' + id);
        if (response.ok) {
            const employeeData = await response.json();
            const employeesData = await fetchEmployees(id);
            const myEmployees = await fetchMyEmployees();
            const goalsData = await fetchGoals(id);
            let subj = await fetchSubjects();
            const subjects = subj.filter(subject => {
                return employeeData.subjects.filter(s => {
                    return s.id === subject.id
                }).length === 0;
            });
            setData({ employee: employeeData, employees: employeesData, myEmployees: myEmployees, subjects: subjects, goals: goalsData });
            setLoading(false);
        } else {
            setLoading(false);
            setData({ employee: null, employees: [] });
        }
    }

    useEffect(() => {
        fetchData(id);
    }, []);
    return [data, loading];
}

function Employee(props) {
    const [employee, setEmployee] = useState({});
    const [employees, setEmployees] = useState([]);
    const [myEmployees, setMyEmployees] = useState([]);
    const [subjects, setSubjects] = useState([]);
    const [data, loading] = fetchEmployeeData(props.match.params.id);
    const [selectedBossId, setBossId] = useState(null);
    const [goals, setGoals] = useState([]);
    const [goal, setGoal] = useState(null);

    const changed = useRef();
    const goalRef = useRef();

    useEffect(() => {
        setEmployees(getFlatListOfSubordinates([], data.employees));
        setMyEmployees(getFlatListOfSubordinates([], data.myEmployees));
        setGoals(data.goals);
        setEmployee(data.employee);
        setSubjects(data.subjects);
    }, [data, loading]);

    async function changeLeader(event) {
        event.preventDefault();
        const requestOptions = {
            method: "PUT",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id: employee.id,
                bossId: selectedBossId
            })
        }

        const response = await fetch("api/UpdateTeamMember", requestOptions);
        if (response.ok) {
            changed.current.style.display = "block";
        }
    }

    async function assignGoal(event) {
        event.preventDefault();

        const requestOptions = {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                employeeId: employee.id,
                subjectId: goal
            })
        };

        console.log(requestOptions);
        const response = await fetch('api/Goals', requestOptions);
        if (response.ok) {
            window.location.reload();
        }
    }

    if (!loading) {
        if (employee) {
            return (
                <div>
                    <div className="row">
                        <div className="col-8">
                            <h2>{employee.firstName} {employee.lastName}</h2>
                            <p>El. paštas: <a href={"mailto:" + employee.email}>{employee.email}</a></p>
                            <div className="employee-form">
                                <Form onSubmit={changeLeader}>
                                    <FormGroup>
                                        <Label for="newBoss">Select a new team for {employee.firstName}:</Label>
                                        <Input name="newBoss" id="newBoss" required type="select" onChange={(event) => setBossId(event.target.value)}>
                                            <option value="-1">-</option>
                                            {
                                                myEmployees.map(
                                                    (myEmployee) => (
                                                        myEmployee.id !== employee.id && <option value={myEmployee.id}>{myEmployee.firstName + " " + myEmployee.lastName}</option>
                                                    )
                                                )
                                            }
                                        </Input>
                                        <FormFeedback tooltip>Please select a value</FormFeedback>
                                        <label ref={changed} className="successMsg">Employee successfuly switched to another team.</label>
                                    </FormGroup>
                                    <FormGroup>
                                        <Button disabled={!selectedBossId} className="btn btn-success">Apply</Button>
                                    </FormGroup>
                                </Form>
                            </div>
                            <div className="employee-form">
                                <Form onSubmit={assignGoal}>
                                    <FormGroup>
                                        <Label for="goal">Select a goal: </Label>
                                        <Input name="goal" id="goal" type="select" required onChange={(event) => setGoal(event.target.value)} >
                                            <option value="-1">-</option>
                                            {subjects.map(subject => (
                                                employee.subjects.filter(sub => sub.id === subject.id).length === 0 && 
                                                goals.filter(goal => goal.subject.id === subject.id).length === 0 &&
                                                <option value={subject.id}>{subject.name}</option>
                                            ))}
                                        </Input>
                                        <label ref={goalRef} className="successMsg">Goal successfuly set for {employee.firstName}.</label>
                                        <FormFeedback tooltip>Please select a value</FormFeedback>
                                    </FormGroup>
                                    <FormGroup>
                                        <Button disabled={!goal} className="btn btn-success">Add</Button>
                                    </FormGroup>
                                </Form>
                            </div>
                        </div>
                        {(employees.length > 0 || employee.subjects.length > 0 || goals.length > 0) &&
                            <div className="col-lg-4 col-md-4 sidebar">
                                {goals.length > 0 && (
                                    <div className="section">
                                        <h5>Current goals: </h5>
                                        <div className="goals">
                                            {goals.map(goal => (
                                                <p>{goal.subject.name}</p>
                                            ))}
                                        </div>
                                    </div>
                                )}
                                {employee.subjects.length > 0 && (
                                    <div className="section">
                                        <h5>{employee.firstName} knows: </h5>
                                        <SubjectList wrapperClass="subjectList" subjects={employee.subjects} />
                                    </div>
                                )}
                                {employees.length > 0 && (
                                    <div className="section">
                                        <h5>{employee.firstName} team list:</h5>
                                        <TeamList wrapperClass="teamList" team={employees} />
                                    </div>
                                )}
                            </div>}
                    </div>
                </div>
            );
        } else {
            return (
                <NotFound />
            )
        }
    } else {
        return (
            <Loader />
        )
    }


};

export default Employee;

