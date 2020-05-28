import React, { useEffect, useState } from 'react';
import Loader from './dump-components/Loader';
import {NotFound} from './dump-components/Error';

function fetchSubject(id){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);
    
    async function fetchData(id) {
        const res = await fetch("api/GetSubjects/" + id);
        const json = res.ok ? await res.json() : null;
        const emp = await fetch("api/Auth/self");
        const empData = emp.ok ? await emp.json() : null;
        setLoading(false);
        setData({subject : json[0], employee: empData});
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

    useEffect(() => {
        setSubject(data.subject);
        setEmployee(data.employee);
        console.log(employee);
    }, [data, loading]);

    const markAsLearnt = () => {

    };

    if(!loading){
        if(subject){
            return (
                <div className="row">
                    <div className="col-8">
                        <h3>{subject.name}</h3>
                        <p>{subject.description}</p>
                        <a href="/subjects">Back to subject list</a>
                    </div>
                    <div className="col-4">
                        {employee.subjects.filter(employeeSubject => employeeSubject.id === subject.id).length === 0 ? <button class="btn btn-success" onClick={markAsLearnt}>Mark subject as learnt</button> : <span>You already know this subject!</span>}
                        
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