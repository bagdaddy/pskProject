import React, {useState} from 'react';
import { Button, Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import auth from './Auth';

const Invitation = (props) => {

    const [email, setEmail] = useState("");
    const [sent, setSent] = useState(false);
    const [sending, setSending] = useState(false);

    async function sendEmail() {
        const res = await auth.getCurrentUser();
        if (true) {
            const me = await res.json();
            const response = await fetch('api/Emails/'+me.id, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email:email })
            })
            if (response.ok) {
                setEmail("");
                setSent(true);
                setSending(false);
            } else {
                return null;
            }
        }
      }  

    const onSubmit = (e) => {
        e.preventDefault()
        if(email){
            setSending(true);
            setSent(false);
            sendEmail();
        }
    }
    return (
        <div>
        <Alert color="primary" isOpen={sent}>
                The invitation was sent!
        </Alert>
        <Alert color="secondary" isOpen={sending}>
            Sending...
        </Alert>
        <Form inline onSubmit={e => onSubmit(e)}>
            <Label>Invite an employee </Label>
            <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                <Input type="email" name="email" id="Email" placeholder="something@idk.cool" onChange={e => setEmail(e.target.value)}/>
            </FormGroup>
            <Button disabled={sending}>Submit</Button>
        </Form>
        </div>
    );
}

export default Invitation;
