import React, {useState} from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import auth from './Auth';
const queryString = require('query-string');

const Login = props => {

    const [userEmail, setUserEmail] = useState("");
    const [password, setPassword] = useState("");
    const parsed = queryString.parse(props.location.search);

    const handleSubmit = (event) => {
        event.preventDefault();
        let valid = true;
        if(userEmail === ""){
            valid = false;
            document.getElementById("email").style.border = "1px solid red";
        }
        if(password === ""){
            valid = false;
            document.getElementById("password").style.border = "1px solid red";
        }

        if(!valid){
            return false;
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                email: userEmail,
                password: password
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
                    <Input type="email" name="email" id="email" placeholder="name@example.com" onChange={(event) => setUserEmail(event.target.value)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="password">Password</Label>
                    <Input type="password" name="password" id="password" onChange={(event) => setPassword(event.target.value)}/>
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