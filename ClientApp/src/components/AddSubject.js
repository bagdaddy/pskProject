import React, { useState, useEffect, useRef } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
const AddSubject = props => {
    const [subjects, setSubjects] = useState([]);
    const [subjectName, setSubjectName] = useState("");
    const [description, setDescription] = useState("");
    const [parent, setParent] = useState(-1);

    const success = useRef();
    async function fetchSubjects() {
        const response = await fetch('api/GetAllSubjects');
        if(response.ok){
            const data = await response.json();
            setSubjects(data);
        }
    }

    useEffect(() => {
        fetchSubjects();
    }, []);
    

    async function insertSubject() {
        let valid = true;
        if(subjectName === ""){
            valid = false;
            document.getElementById("subject_name").style.border = "1px solid red";
        }

        if(!valid){
            return false;
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name: document.getElementById("subject_name").value,
                Description: document.getElementById("description").value,
                ParentSubjectId: parent == "-1" ? null : parent
            })
        };
        const res = await fetch('api/CreateSubject', requestOptions);
        if(res.ok){
            success.current.style.display = "block";
        }else{
            //TODO: prideti error handlinima
        }
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        insertSubject();
    };


    return (
        <div className="form-left">
            <Form onSubmit={handleSubmit}>
                <FormGroup>
                    <Label for="subject_name">Subject name</Label>
                    <Input type="text" id="subject_name" name="subject_name" placeholder='e.g. "PHP"' value={subjectName} onChange={(event) => setSubjectName(event.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="description">Description</Label>
                    <Input type="textarea" id="description" name="description" placeholder="A really important subject" value={description} onChange={(event) => setDescription(event.target.value) } />
                </FormGroup>
                <FormGroup>
                    <Label for="parent_subject">Parent</Label>
                    <Input type="select" name="parent" id="parent" value={parent} onChange={(event) => setParent(event.target.value)}>
                        <option value="-1">-</option>
                        {subjects.length > 0 && subjects.map(subject => (
                            <option key={subject.id} value={subject.id}>{subject.name}</option>
                        ))}
                    </Input>
                    <label ref={success} className="successMsg">Subject successfully added.</label>
                </FormGroup>
                <FormGroup>
                    <Button className="btn btn-success">Add</Button>
                </FormGroup>
            </Form>
        </div>

    );

};

export default AddSubject;