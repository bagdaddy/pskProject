import React, { useState, useEffect } from 'react';
import SubjectList from './dump-components/SubjectList';

const Subjects = (props) => {
    const [subjects, setSubjects] = useState([]);

    const fetchData = React.useCallback(() => {
        fetch("api/GetAllSubjects")
            .then(response => response.json())
            .then(data => setSubjects(data))
            .catch((error) => {
                console.log(error);
            });
    });

    useEffect(() => {
        fetchData()
    }, []);


    return (
        <div className="subjects">
            <h1>Subjects</h1> 
            <a className="btn btn-success addSubjectBtn" href="/add-subject">Add new subject</a>
            <div className="row">
                <div className="col-8">
                    {subjects && <SubjectList wrapperClass="subjectsList" subjects={subjects} />}
                </div>
                <div className="col-4">
                </div>
            </div>
        </div>
    )

};

export default Subjects;