import React, { useEffect, useState } from 'react';
import Loader from './dump-components/Loader';
import NotFound from './dump-components/Error';

function fetchSubject(id){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);
    
    async function fetchData(id) {
        const res = await fetch("api/GetSubjects/" + id);
        const json = res.ok ? await res.json() : null;
        setLoading(false);
        setData(json ? json[0] : null);
    };

    useEffect(() => {
        fetchData(id);
    }, []);

    return [data, loading];
}

function Subject(props) {

    const [subject, setSubject] = useState(null);
    const [data, loading] = fetchSubject(props.match.params.id);

    useEffect(() => {
        setSubject(data);
    }, [data, loading])

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