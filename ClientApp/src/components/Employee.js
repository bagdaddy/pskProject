import React, { useEffect, useState } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import Loader from './dump-components/Loader';
import NotAuthorized from './dump-components/Error';

function fetchEmployeeData(id){
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    async function fetchEmployees(){
        const response = await fetch('api/Employees');
        if(response.ok){
            return await response.json();
        }
    }

    async function fetchData(id){
        const response = await fetch('api/Employees/' + id);
        if(response.ok){
            const employeeData = await response.json();
            const employeesData = await fetchEmployees();
            setData({employee: employeeData, employees: employeesData ? employeesData : []});
            setLoading(false); 
        }
    }

    useEffect(() => {
        fetchData(id);
    }, []);
    console.log(data);
    return [data, loading];
}

function Employee (props) {
    const [employee, setEmployee] = useState({});
    const [allEmployees, setEmployees] = useState([]);
    const [data, loading] = fetchEmployeeData(props.match.params.id);

    useEffect(() => {
        setEmployees(data.employees);
        setEmployee(data.employee);
    }, [data, loading]);

    if(!loading){
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
                <NotFound/>
            )
        }
    }else{
        return (
            <Loader/>
        )
    }
    

};

// ReactDOM.render(
//     <Employee/>,
//     document.getElementById("profile")
// );

export default Employee;

