import React, { useEffect, useState } from 'react';
import * as qs from 'query-string';

const Subject = props => {
    const [subject, setSubject] = useState({});
    const parsed = qs.parse(window.location.search);

    useEffect(()=>{
        fetch("http://localhost:5000/api/GetSubjects/" + parsed.id)
            .then(response => response.json())
            .then(data => setSubject(data));

            console.log(subject);
    });
    return(
        <div>
            <h3>{subject.name}</h3>
            <p>{subject.description}</p>
            <a href="/subjects">Grįžti į temų sąrašą</a>
        </div>

    )
};

export default Subject;