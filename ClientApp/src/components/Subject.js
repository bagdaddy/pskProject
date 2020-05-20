import React, { useEffect, useState } from 'react';
import * as qs from 'query-string';

const Subject = props => {
    const [subject, setSubject] = useState({});
    const parsed = qs.parse(window.location.search);

    const fetchData = React.useCallback(() => {
        fetch("http://localhost:5000/api/GetSubjects/" + parsed.id)
            .then(response => response.json())
            .then(data => setSubject(data));
    });
    useEffect(() => {
        fetchData();

    }, []);

    console.log(subject.parentSubject);
    return (
        <div className="row">
            <div className="col-8">
                <h3>{subject.name}</h3>
                <p>{subject.description}</p>
                <a href="/subjects">Grįžti į temų sąrašą</a>
            </div>
            <div className="col-4">
                {
                    subject.parentSubject &&
                    (<div>
                        <h3>Parent:</h3>
                        <a href={"/subject?id=" + subject.parentSubject.id} className="link">{subject.parentSubject.name}</a>
                    </div>
                    )
                }
            </div>

        </div>

    )
};

export default Subject;