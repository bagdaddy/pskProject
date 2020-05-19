import React, {useState, useEffect} from 'react';
import SubjectList from './dump-components/SubjectList';

const Subjects = (props) => {
    const [subjects, setSubjects] = useState([]);
    
    useEffect(()=>{
        fetch("http://localhost:5000/api/GetSubjects")
            .then(response => response.json())
            .then(data => setSubjects(data));    
    });
    

    return (
        <div>
            <h2>Temų sąrašas:</h2>
            <div className="row">
                <div className="col-8">
                    {subjects && <SubjectList subjects={subjects}/>}
                </div>
                <div className="col-4">
                    <a className="btn btn-success" href="/add-subject">Pridėti naują temą</a>
                </div>
            </div>
        </div>
    )

};

export default Subjects;