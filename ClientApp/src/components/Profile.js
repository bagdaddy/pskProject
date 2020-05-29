import React, { useState, useRef, useEffect } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import { Form, FormGroup, Label, Input, Button } from 'reactstrap';
import Loader from './dump-components/Loader';
import getFlatListOfSubordinates from './dump-components/getSubordinates';


function fetchData() {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    async function fetchSubjects() {
        const response = await fetch('api/GetAllSubjects');
        const json = await response.json();
        return json;
    };

    async function fetchGoals(id) {
        const response = await fetch('api/Goals/' + id);
        if (response.ok) {
            return await response.json();
        } else {
            return [];
        }
    }

    async function fetchAllEmployees(id) {
        const response = await fetch('api/GetTeams/' + id);
        const json = await response.json();
        return json[0].subordinates;
    }

    async function getDataAsync() {
        const response = await fetch('api/Auth/self');
        const userData = await response.json();
        const eResponse = await fetch('api/Employees/' + userData.id);
        const employeeData = await eResponse.json();
        const goalsData = await fetchGoals(employeeData.id);
        const employees = await fetchAllEmployees(employeeData.id);
        let subj = await fetchSubjects();
        const subjects = subj.filter(subject => {
            return employeeData.subjects.filter(s => {
                return s.id === subject.id
            }).length === 0;
        });
        setData({ employee: employeeData, employees: employees, subjects: subjects, goals: goalsData });
        setLoading(false);
    }

    useEffect(() => {
        getDataAsync();
    }, []);

    return [data, loading];
}

function Profile(props) {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);
    const [subjects, setSubjects] = useState([]);
    const [teamSubjects, setTeamSubjects] = useState([]);
    const [selectedSubjectId, setSelectedSubjectId] = useState();
    const [data, loading] = fetchData();
    const [goals, setGoals] = useState([]);
    const [goal, setGoal] = useState(null);

    let success = useRef();
    const goalRef = useRef();

    useEffect(() => {
        let flatList = getFlatListOfSubordinates([], data.employees);
        setEmployees(flatList);
        console.log(data.employees);
        setEmployee(data.employee);
        setSubjects(data.subjects);
        setGoals(data.goals);
        let subjects = [];
        if (!loading) {
            flatList.forEach(e => {
                e.learnedSubjects.forEach(subject => {
                    if (subjects.filter(s => s.id === subject.id).length === 0) {
                        subjects.push(subject);
                    }
                });
            });
            setTeamSubjects(subjects);
        }

    }, [data, loading])

    const learnSubject = (event) => {
        event.preventDefault();
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' }
        };

        fetch('api/Employees/AddSubject/' + selectedSubjectId, requestOptions)
            .then(response => {
                if (response.ok) {
                    window.location.reload();
                }
            });
    };

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
            goalRef.current.style.display = "block";
        }
    }

    if (!loading) {
        return (
            <div>
                <div className="row">
                    <div className="col-8">
                        <h2>{employee.firstName} {employee.lastName}</h2>
                        <p>El. paštas: <a href={"mailto:" + employee.email}>{employee.email}</a></p>
                        <div className="employee-form">
                            <Form onSubmit={learnSubject}>
                                <FormGroup onSubmit={learnSubject}>
                                    <Label for="subject">Selet a subject you've learnt: </Label>
                                    <Input type="select" name="subject" id="subject" onChange={(event) => setSelectedSubjectId(event.target.value)}>
                                        <option value="-1">-</option>
                                        {subjects.map((subject) => (
                                            employee.subjects.filter(sub => sub.id === subject.id).length === 0 && <option value={subject.id}>{subject.name}</option>
                                        ))}
                                    </Input>
                                    <label ref={success} className="successMsg">Subject successfully added.</label>
                                </FormGroup>
                                <Button className="btn btn-success" disabled={!selectedSubjectId}>Submit</Button>
                            </Form>
                        </div>
                        <div className="employee-form">
                            <Form onSubmit={assignGoal}>
                                <FormGroup>
                                    <Label for="goal">Set a goal for yourself: </Label>
                                    <Input name="goal" id="goal" type="select" required onChange={(event) => setGoal(event.target.value)} >
                                        <option value="-1">-</option>
                                        {subjects.map(subject => (
                                            employee.subjects.filter(sub => sub.id !== subject.id).length === 0 && <option value={subject.id}>{subject.name}</option>
                                        ))}
                                    </Input>
                                    <label ref={goalRef} className="successMsg">Goal successfuly set.</label>
                                </FormGroup>
                                <FormGroup>
                                    <Button disabled={!goal} className="btn btn-success">Add</Button>
                                </FormGroup>
                            </Form>
                        </div>
                    </div>
                    {(employee.subjects.length > 0 || allEmployees.length > 0 || goals.length > 0) &&
                        <div className="col-lg-4 col-md-4 sidebar">
                            {goals.length > 0 && (
                                <div className="section">
                                    <h5>Current goals: </h5>
                                    <div className="goals">
                                        {goals.map(goal => (
                                            <a href={"/subject/" + goal.subject.id}>{goal.subject.name}</a>
                                        ))}
                                    </div>
                                </div>
                            )}
                            {employee.subjects.length > 0 && (
                                <div className="section">
                                    <h5>You have already learned: </h5>
                                    <SubjectList wrapperClass="subjectsList" subjects={employee.subjects} />
                                </div>
                            )}
                            {allEmployees.length > 0 && (
                                <div className="section">
                                    <h5>Your team: </h5>
                                    <TeamList wrapperClass="teamList" team={allEmployees} />
                                </div>
                            )}
                            {teamSubjects.length > 0 &&
                                <div className="section">
                                    <h5>Your team has learned these subjects: </h5>
                                    <SubjectList wrapperClass="subjectList" subjects={teamSubjects} />
                                </div>
                            }

                        </div>
                    }
                </div>
            </div>
        );
    } else {
        return (
            <Loader />
        )
    }
};



export default Profile;