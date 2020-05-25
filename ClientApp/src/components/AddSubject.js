import React, { useState, useEffect } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
const AddSubject = props => {
    const [subjects, setSubjects] = useState([]);
    const [insertedSubject, setInsertedSubject] = useState({});

    const fetchSubjects = React.useCallback(() => {
        fetch('api/GetSubjects')
            .then(response => response.json())
            .then(data => setSubjects(data));
    });

    const insertSubject = () => {
        let parentSubjetcId = document.getElementById("parent").value !== "-1" ? document.getElementById("parent").value : null;
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name: document.getElementById("subject_name").value,
                Description: document.getElementById("description").value,
                ParentSubjectId: parentSubjetcId
            })
        };
        console.log(requestOptions);
        fetch('api/CreateSubject', requestOptions)
            .then(response => response.text())
            .then(data => setInsertedSubject(data));
    };

    useEffect(() => {
        fetchSubjects();
    }, []);

    const handleSubmit = (event) => {
        event.preventDefault();
        insertSubject();
    };

    const getChildren = (subject) => {
        return(
            <
        )
    }

    console.log(subjects);


    return (
        <div className="form-left">
            <Form onSubmit={handleSubmit}>
                <FormGroup>
                    <Label for="subject_name">Subject name</Label>
                    <Input type="text" id="subject_name" name="subject_name" placeholder='e.g. "PHP"' />
                </FormGroup>
                <FormGroup>
                    <Label for="description">Description</Label>
                    <Input type="text" id="description" name="description" placeholder="A really important subject" />
                </FormGroup>
                <FormGroup>
                    <Label for="parent_subject">Parent</Label>
                    <Input type="select" name="parent" id="parent">
                        <option value="-1">-</option>
                        {subjects.length > 0 && subjects.map(subject => (
                            <option key={subject.id} value={subject.id}>{subject.name}</option>
                        ))}
                    </Input>
                </FormGroup>
                <FormGroup>
                    <Button className="btn btn-success">Add</Button>
                </FormGroup>
            </Form>
        </div>

    );

};

export default AddSubject;