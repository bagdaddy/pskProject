import React, {useState, useEffect} from 'react';
import SubjectList from './dump-components/SubjectList';

const Subjects = (props) => {
    const [subjects, setSubjects] = useState([]);
    
    const fetchData = React.useCallback(()=>{
        fetch("api/GetSubjects")
        .then(response => response.json())
        .then(data => setSubjects(data))
        .catch((error)=>{
            console.log(error);
        });
    })
    useEffect(()=>{
            fetchData()
    }, []);
    

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