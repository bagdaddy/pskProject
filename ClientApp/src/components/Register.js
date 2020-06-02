import React, { useEffect, useState } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';

import Loader from './dump-components/Loader';
import auth from './Auth';

function fetchData (id){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);

    async function getDataAsync(id){
        const res = await fetch("api/GetInvite/" + id);
        console.log(res);
        if(res.status === 200){
            const json = await res.json();
            console.log(json);
            setData(json);
        }else if(res.status === 204){
            setData({error: "The invite ID is invalid."});
        }else{
            setData({error: "There was an unexpected error."});
        }
        setLoading(false);
    }

    useEffect(() => {
        getDataAsync(id);
    }, []);

    return [data,loading];
}

const Register = props => {
    console.log(props);
    const [data, loading] = fetchData(props.match.params.id);
    const [email, setEmail] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [password, setPassword] = useState("");
    const [invitingEmployee, setInvitingEmployee] = useState({});

    useEffect(() => {
        if(data.hasOwnProperty("error")){
            
        }else{
            setEmail(data.email);
            setInvitingEmployee(data.employee);
        }
    }, [data, loading]);

    
    const handleSubmit = (event) => {
        event.preventDefault();
        let valid = true;
        if(email === ""){
            valid = false;
            document.getElementById("email").style.border = "1px solid red";
        }

        if(firstname === ""){
            valid = false;
            document.getElementById("firstname").style.border = "1px solid red";
        }

        if(lastname === ""){
            valid = false;
            document.getElementById("lastname").style.border = "1px solid red";
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
                email: email,
                firstName: firstname,
                lastName: lastname,
                password: password,
                inviteId: data.id
            })
        };

        fetch("api/Auth/register", requestOptions)
            .then(response => {
                if(response.ok)
                    window.location.href = "/login";
                    else{
                        window.location.href = '/login?errors=true';
                    }
                })
    };

    if(!loading){
        if(data.hasOwnProperty("error")){
            return (
                <div>{data.error}</div>
            )
        }
        return (
            <div className="login-form">
                <Form onSubmit={handleSubmit}>
                    <FormGroup>
                        <Label for="email">Email</Label>
                        <Input type="email" name="email" id="email" placeholder="name@example.com" value={email} onChange={(e) => setEmail(e.target.value)}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="firstname">First name:</Label>
                        <Input type="text" name="firstname" id="firstname" placeholder="John" value={firstname} onChange={(e) => setFirstname(e.target.value)}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="lastname">Last name:</Label>
                        <Input type="text" name="lastname" id="lastname" placeholder="Doe" value={lastname} onChange={(e) => setLastname(e.target.value)}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input type="password" name="password" id="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
                    </FormGroup>
                    <FormGroup>
                        <Button className="btn btn-success">Register</Button>
                    </FormGroup>
                </Form>
    
            </div>
        )
    }else{
        return(
            <Loader/>
        )
    }
    


};

export default Register;