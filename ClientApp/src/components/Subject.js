import React, { useEffect, useState } from 'react';
import Loader from './dump-components/Loader';
import {NotFound} from './dump-components/Error';

function fetchSubject(id){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);
    
    async function fetchData(id) {
        const res = await fetch("api/GetSubjects/" + id);
        const json = res.ok ? await res.json() : null;
        const user = await fetch("api/Auth/self");
        const userData = user.ok ? await user.json() : null;
        const employee = await fetch("api/Employees/" + userData.id);
        const employeeData = await employee.json();
        setLoading(false);
        setData({subject : json[0], employee: employeeData});
        console.log(json[0]);
    };

    useEffect(() => {
        fetchData(id);
    }, []);

    return [data, loading];
}

function Subject(props) {

    const [employee, setEmployee] = useState(null);
    const [subject, setSubject] = useState(null);
    const [data, loading] = fetchSubject(props.match.params.id);
    console.log(props.match.params.id);

    useEffect(() => {
        setSubject(data.subject);
        setEmployee(data.employee);
        console.log(employee);
    }, [data, loading]);

    const markAsLearnt = () => {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' }
        };

        fetch('api/Employees/AddSubject/' + subject.id, requestOptions)
            .then(response => {
                if (response.ok) {
                    window.location.reload();
                }
            });
    };

    if(!loading){
        if(subject){
            return (
                <div className="row">
                    <div className="col-8">
                        <h3>{subject.name}</h3>
                        <div className="description">
                            <p>{subject.description}</p>
                        </div>
                        <a href="/subjects">Back to subject list</a>
                    </div>
                    <div className="col-4">
                        {employee.subjects.filter(employeeSubject => employeeSubject.id === subject.id).length === 0 ? <button className="btn btn-success" onClick={markAsLearnt}>Mark subject as learnt</button> : <span>You already know this subject!</span>}
                        &nbsp;&nbsp;&nbsp;<a className="btn btn-light" href={"/edit-subject/" + subject.id}>Edit</a>
                    </div>
                </div> 
        )
        }else{
            return(
                <NotFound />
            )
        }
        
    }else{
        return(
            <Loader/>
        )
    }
    
};

export default Subject;