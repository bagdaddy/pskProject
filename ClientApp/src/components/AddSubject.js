import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
const AddSubject = props => {
    let subjectName = "";

    const handleChange = (event) => {
        console.log(event.target.value);
        subjectName = event.target.value;
    };

    const handleSubmit = (event) => {
        console.log(subjectName)
        event.preventDefault();
    };

    return (
        <div className="form-left">
            <Form onSubmit={handleSubmit}>
                <FormGroup>
                    <Label for="subject_name">Subject name</Label>
                    <Input type="text" id="subject_name" name="subject_name" placeholder='e.g. "PHP"' />
                </FormGroup>
                <FormGroup>
                    <Label for="parent_subject">Parent</Label>
                    <Input type="select" name="parent" id="parent">
                        <option value="-1">-</option>
                        <option value="1">PHP</option>
                        <option value="2">C#</option>
                        <option value="3">Java</option>
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