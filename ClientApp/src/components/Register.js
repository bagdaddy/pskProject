import React, { useEffect, useState } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import auth from './Auth';

function fetchData (id){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);

    async function getDataAsync(id){
        const res = await fetch("api/GetInvite/" + id);
        if(res.ok){
            const data = await res.json();
            setData(data);
        }else{
            setData({error: "The invite is invalid."});
        }
        setLoading(false);
    }

    useEffect(() => {
        getDataAsync(id);
    }, []);

    return [data,loading];
}

const Register = props => {

    const [data, loading] = fetchData(props.match.params.id);
    const [email, setEmail] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [password, setPassword] = useState("");

    useEffect(() => {
        if(data.hasOwnProperty("error")){
            return (
                <div>Your invite is invalid.</div>
            )
        }else{
            setEmail(data.email);
        }
    }, [data, loading]);
    const handleSubmit = (event) => {
        event.preventDefault();
        // let userEmail = document.getElementById("email").value;
        // let firstname = document.getElementById("firstname").value;
        // let lastname = document.getElementById("lastname").value;
        // let userPw = document.getElementById("password").value;

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                email: email,
                firstName: firstname,
                lastName: lastname,
                password: password
            })
        };

        fetch("api/Auth/register", requestOptions)
            .then(response => {
                window.location.href = "/login";
            })
        // auth.login(requestOptions, ()=>{
        //     props.history.push("/me");
        // })
    };

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


};

export default Register;