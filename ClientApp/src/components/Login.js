import React from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';

const Login = props => {

    const handleSubmit = (event) => {
        event.preventDefault();
        let email = document.getElementById("email");
        let password = document.getElementById("password");
    };

    return (
        <div className="login-form">
            <Form onSubmit={handleSubmit}>
                <FormGroup>
                    <Label for="email">Email</Label>
                    <Input type="email" name="email" id="email" placeholder="name@example.com" />
                </FormGroup>
                <FormGroup>
                    <Label for="password">Password</Label>
                    <Input type="password" name="password" id="password" />
                </FormGroup>
                <FormGroup>
                    <Button className="btn btn-success">Login</Button>
                </FormGroup>
            </Form>

        </div>
    )


};

export default Login;