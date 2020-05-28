import React, { useState, useRef, useEffect } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import { Form, FormGroup, Label, Input, Button } from 'reactstrap';
import Loader from './dump-components/Loader';

function fetchData() {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    async function fetchSubjects(){
        const response = await fetch('api/GetAllSubjects');
        const json = await response.json();
        return json;
    };

    async function fetchAllEmployees() {
        const response = await fetch('api/Employees');
        const json = await response.json();
        return json;
    }

    async function getDataAsync(){
        const response = await fetch('api/Auth/self');
        const employee = await response.json();
        console.log(employee);
        const employees = await fetchAllEmployees();
        let subj = await fetchSubjects();
        const subjects = subj.filter(subject => {
            return employee.subjects.filter(s => {
                return s.id === subject.id
            }).length === 0;
        });
        setData({employee: employee, employees: employees, subjects: subjects});
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
    const [selectedSubjectId, setSelectedSubjectId] = useState("");
    const [data, loading] = fetchData();
    
    let success = useRef();

    useEffect(() => {
        setEmployees(data.employees);
        setEmployee(data.employee);
        setSubjects(data.subjects);
    }, [data, loading])

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


    if (!loading) {
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
    }else{
        return(
            <Loader/>
        )
    }
};



export default Profile;