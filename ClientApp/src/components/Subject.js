import React, { useEffect, useState } from 'react';
import NotFound from './dump-components/Error';

const Subject = props => {

    const [subject, setSubject] = useState(null);

    const fetchData = React.useCallback((id) => {
        fetch("api/GetSubjects/" + id)
            .then(response => response.json())
            .then(data => setSubject(data[0]));
    });


    useEffect(() => {
        if (props.match.params.id) {
            fetchData(props.match.params.id);
        }
    }, []);

    if(subject){
        return (
                <div className="row">
                    <div className="col-8">
                        <h3>{subject.name}</h3>
                        <p>{subject.description}</p>
                        <a href="/subjects">Back to subject list</a>
                    </div>
                    <div className="col-4">
                        {
                            subject.parentSubject &&
                            (<div>
                                <h3>Parent subject:</h3>
                                <a href={"/subject/" + subject.parentSubject.id} className="link">{subject.parentSubject.name}</a>
                            </div>
                            )
                        }
                    </div>
                </div> 
        )
    }else{
        return(
            <div></div>
        )
    }
    
};

export default Subject;