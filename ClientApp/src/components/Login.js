import React from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import auth from './Auth';
const queryString = require('query-string');

const Login = props => {
    console.log(props);
    const parsed = queryString.parse(props.location.search);
    const handleSubmit = (event) => {
        event.preventDefault();
        let userEmail = document.getElementById("email").value;
        let userPw = document.getElementById("password").value;

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                email: userEmail,
                password: userPw
            })
        };

        auth.login(requestOptions, ()=>{
            props.history.push("/me");
        })
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
                {parsed.errors && <span className="error">Invalid username or password</span>}

                </FormGroup>
                <FormGroup>
                    <Button className="btn btn-success">Login</Button>
                </FormGroup>
            </Form>

        </div>
    )


};

export default Login;