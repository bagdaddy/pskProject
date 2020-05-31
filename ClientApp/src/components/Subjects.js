import React, { useState, useEffect } from 'react';
import SubjectList from './dump-components/SubjectList';
import Loader from './dump-components/Loader';

function getSubjectsAsync(){
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    async function fetchData(){
        const res = await fetch("api/GetAllSubjects");
        const d = res.ok ? await res.json() : [];
        console.log(d);
        setData(d);
        setLoading(false);
    }

    useEffect(() => {
        fetchData();
    }, []);

    return [data, loading];
}

function Subjects(props) {
    const [subjects, setSubjects] = useState([]);
    const [data, loading] = getSubjectsAsync();

    useEffect(() => {
        console.log(data);
        setSubjects(data);
    }, [data, loading]);

    if(!loading){
        return (
            <div className="subjects">
                <h1>Subjects</h1> 
                <a className="btn btn-success addSubjectBtn" href="/add-subject">Add new subject</a>
                <div className="row">
                    <div className="col-8">
                        {subjects && <SubjectList wrapperClass="subjectsList" itemClass="item" subjects={subjects} />}
                    </div>
                    <div className="col-4">
                    </div>
                </div>
            </div>
        )
    }else{
        return(
            <Loader/>
        )
    }
    

};

export default Subjects;