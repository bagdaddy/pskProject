import React, { useState, useEffect, useRef } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import Loader from './dump-components/Loader';

function fetchSubject(id){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);
    
    async function fetchData(id) {
        const res = await fetch("api/GetSubjects/" + id);
        const json = res.ok ? await res.json() : null;
        // const emp = await fetch("api/Auth/self");
        // const empData = emp.ok ? await emp.json() : null;
        setLoading(false);
        setData(json[0]);
        console.log(json[0]);
    };

    useEffect(() => {
        fetchData(id);
    }, []);

    return [data, loading];
}

const AddSubject = props => {
    const [subject, setSubject] = useState([]);
    const [data, loading] = fetchSubject(props.match.params.id);
    const [subjectName, setSubjectName] = useState("");
    const [description, setDescription] = useState("");

    const success = useRef();

    useEffect(() => {
        setSubject(data);
        setSubjectName(data.name);
        setDescription(data.description);
    }, [data, loading]);
    

    async function updateSubject() {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name: subjectName,
                Description: description,
                id: subject.id
            })
        };
        console.log(requestOptions);
        const res = await fetch('api/UpdateSubject', requestOptions);
        if(res.ok){
            success.current.style.display = "block";
        }else{
            //TODO: prideti error handlinima
        }
    }


    const handleSubmit = (event) => {
        event.preventDefault();
        updateSubject();
    };

    if(!loading){
        return (
            <div className="form-left">
                <Form onSubmit={handleSubmit}>
                    <FormGroup>
                        <Label for="subject_name">Subject name</Label>
                        <Input type="text" id="subject_name" name="subject_name" value={subjectName} onChange={(event) => setSubjectName(event.target.value)} placeholder='e.g. "PHP"' />
                    </FormGroup>
                    <FormGroup>
                        <Label for="description">Description</Label>
                        <Input type="textarea" id="description" name="description" placeholder="A really important subject" value={description} onChange={(event) => setDescription(event.target.value)}/>
                        <label ref={success} className="successMsg">Subject successfully updated.</label>
                    </FormGroup>
                    <FormGroup>
                        <Button className="btn btn-success">Add</Button>
                    </FormGroup>
                </Form>
            </div>
    
        );
    
    }else{
        return(
            <Loader/>
        )
    }
    
};

export default AddSubject;