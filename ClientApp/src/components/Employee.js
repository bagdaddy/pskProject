import React, { useEffect, useState, useRef } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import Loader from './dump-components/Loader';
import { NotFound } from './dump-components/Error';
import { Form, FormGroup, Label, Input, Button } from 'reactstrap';

function fetchEmployeeData(id) {
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);

    let success = useRef();
    async function fetchEmployees(id) {
        const response = await fetch('api/GetTeams/' + id);
        if (response.ok) {
            const employees = await response.json();
            console.log(employees);
            return await employees[0].subordinates;
        } else {
            return null;
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

    async function fetchData(id) {
        const response = await fetch('api/Employees/' + id);
        if (response.ok) {
            const employeeData = await response.json();
            const employeesData = await fetchEmployees(id);
            const myEmployees = await fetchMyEmployees();
            const set = { employee: employeeData, employees: employeesData, myEmployees: myEmployees };
            console.log(set);
            setData(set);
            console.log(data);
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
    const [data, loading] = fetchEmployeeData(props.match.params.id);
    const [selectedBossId, setBossId] = useState("");

    const success = useRef();

    useEffect(() => {
        setEmployees(data.employees);
        setMyEmployees(data.myEmployees);
        setEmployee(data.employee);
        console.log(employee);
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
         if(response.ok){
             success.current.style.display = "block";
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
                            <div className="row">
                                <div className="col-8">
                                    <Form onSubmit={changeLeader}>
                                        <FormGroup>
                                            <Label for="newBoss">Select a new team for {employee.firstName}:</Label>
                                            <Input name="newBoss" id="newBoss" type="select" onChange={(event) => setBossId(event.target.value)}>
                                                {
                                                    myEmployees.map(
                                                        (myEmployee) => (
                                                            myEmployee.id !== employee.id && <option value={myEmployee.id}>{myEmployee.firstName + " " + myEmployee.lastName}</option>
                                                        )
                                                    )
                                                }
                                            </Input>
                                            <label ref={success} className="successMsg">Employee successfuly switched to another team.</label>
                                        </FormGroup>
                                        <FormGroup>
                                            <Button>Apply</Button>
                                        </FormGroup>
                                    </Form>
                                </div>

                            </div>
                        </div>
                        {(employees.length > 0 || employee.subjects.length > 0) &&
                            <div className="col-lg-4 col-md-4 sidebar">
                                {employee.subjects.length > 0 && (
                                    <div>
                                        <h5>{employee.firstName} learnt subjects: </h5>
                                        <SubjectList subjects={employee.subjects} />
                                    </div>
                                )}
                                {employees.length > 0 && (
                                    <div>
                                        <h5>{employee.firstName} team list:</h5>
                                        <TeamList team={employees} />
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

// ReactDOM.render(
//     <Employee/>,
//     document.getElementById("profile")
// );

export default Employee;

