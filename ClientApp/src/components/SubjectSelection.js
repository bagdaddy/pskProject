import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Input, Label, Form, FormGroup } from 'reactstrap';
import subjects from './testSubjects'

const SubjectSelection = forwardRef((props, ref) => {
  
    useImperativeHandle(ref, () => ({toggle, dateSetup}));

    const [modal, setModal] = useState(false);
    const [date, setDate] = useState(null);
    
    const [comment, setComment] = useState("");
    const [subjectsSelected, setSubjectsSelected] = useState([null, null, null, null]);
    const [num, setNum] = useState(null);

    const now = new Date();

    const createNewDate = React.useCallback(() => {
        fetch('api/CreateNewDate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ EmployeeId: "CHANGE INTO ID", Date: date, SubjectList: subjectsSelected })
        }).then(res => res.json()).catch(error => console.error('Error:', error));
    });

    const deleteExistingDate = React.useCallback(() => {
        fetch("api/deleteDate/${date.id}")
            .then(response => response.json())
            .catch((error) => {
                console.log(error);
            })});

    const changeExistingDate = React.useCallback(() => {
        //post user id/date/subjects/comment
    });

    const toggle = () => {
        setModal(!modal);

    };
    const dateSetup = (value) =>{
        setDate(value);
    } 

    useEffect(() => {  
        var n;
        var i;
        if(props.dates.length){
            for(i = 0; i<props.dates.length; ++i){
                if(props.dates[i].date.getTime() === date.getTime()){
                    setSubjectsSelected(props.dates[i].subjects)
                    setComment(props.dates[i].comment)
                    setNum(i)
                    n = i
                }
            }
        }
        if(!n){
            newDateOpened()
        }
        return function cleanup() {
            setNum(null);
          };
      
    },[date]);

    const newDateOpened = () => {
        setSubjectsSelected([null, null, null, null]);
        setComment("");
        setNum(null);
    }

    const onDeleteButtonClick = () => {
        if(subjectsSelected[0]){
            if(num){
                let dataArray = [...props.dates];
                dataArray.splice(num, 1)
                props.setDates(dataArray);
            }
            toggle();
        }
    }

    const onSubmitButtonClick = () => {
        if(subjectsSelected[0]){
            if(num){
                let dataArray = [...props.dates];
                dataArray[num].subjects = subjectsSelected;
                dataArray[num].comment = comment;
                props.setDates(dataArray);
            }else{
                props.setDates(props.dates.concat({id:16, date:date, subjects:subjectsSelected, comment:comment}));
            }
            toggle();
        }
    }
    
    function HasDayPassed(){
        if(date? date.getTime() > now.getTime():""){
            return false
        } else{
            return true
        }
    }

    function IsDateDeletable(){
        return !HasDayPassed() && num? 1:0
    }
    function IsDateAddableAndChangable(){
        if(num){
            return !HasDayPassed()? 1:0
        }else{
        //+to conditions IsDateLimitPer(Quarter/Year)Reached(no)
            return !HasDayPassed()? 1:0 
        }
    }

    return (
        <div>
            <Modal isOpen={modal} toggle={toggle}>
                <ModalHeader toggle={toggle}>Select subjects</ModalHeader>
                <ModalBody>
                    <Label>{date? date.toString().substring(0, 10):""}</Label>
                    <Form>
                        <FormGroup>
                            <Label for="subjectSelect">Select a subject</Label>
                            <Input 
                            defaultValue={subjectsSelected[0]?subjectsSelected[0]:'DEFAULT'} 
                            type="select" name="select" 
                            id="subjectSelect" 
                            onChange={event => setSubjectsSelected([event.target.value, subjectsSelected[1], subjectsSelected[2], subjectsSelected[3]])}>
                                <option value="DEFAULT" disabled>Choose a subject ...</option>
                                {subjects.map(subject => (
                                <option key={subject.id} value={subject.id}>{subject.title}</option>))};
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Input 
                            defaultValue={subjectsSelected[1]?subjectsSelected[1]:'DEFAULT'} 
                            type="select" name="select" 
                            id="subjectSelect" 
                            onChange={event => setSubjectsSelected([subjectsSelected[0], event.target.value, subjectsSelected[2], subjectsSelected[3]])}
                            disabled={subjectsSelected[0]?0:1}>
                                <option value="DEFAULT" disabled>Choose a subject ...</option>
                                {subjects.map(subject => (
                                <option key={subject.id} value={subject.id}>{subject.title}</option>))};
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Input 
                            defaultValue={subjectsSelected[2]?subjectsSelected[2]:'DEFAULT'} 
                            type="select" name="select" 
                            id="subjectSelect" 
                            onChange={event => setSubjectsSelected([subjectsSelected[0], subjectsSelected[1], event.target.value, subjectsSelected[3]])}
                            disabled={subjectsSelected[1]?0:1}>
                                <option value="DEFAULT" disabled>Choose a subject ...</option>
                                {subjects.map(subject => (
                                <option key={subject.id} value={subject.id}>{subject.title}</option>))};
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Input 
                            defaultValue={subjectsSelected[3]?subjectsSelected[3]:'DEFAULT'} 
                            type="select" name="select" 
                            id="subjectSelect" 
                            onChange={event => setSubjectsSelected([subjectsSelected[0], subjectsSelected[1], subjectsSelected[2], event.target.value])}
                            disabled={subjectsSelected[2]?0:1}>
                                <option value="DEFAULT" disabled>Choose a subject ...</option>
                                {subjects.map(subject => (
                                <option key={subject.id} value={subject.id}>{subject.title}</option>))};
                            </Input>
                        </FormGroup>                        
                        <FormGroup>
                            <Label for="Text">Comments</Label>
                            <Input value={comment} type="textarea" name="text" id="Text" onChange={event => setComment(event.target.value)} />
                        </FormGroup>
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={onSubmitButtonClick} disabled={!IsDateAddableAndChangable()}>Set date</Button>{' '}
                    <Button color="secondary" onClick={toggle}>Cancel</Button>
                    <Button color="danger" onClick={onDeleteButtonClick} disabled={!IsDateDeletable()}>Delete</Button>
                </ModalFooter>
            </Modal>
        </div>
    );
})

export default SubjectSelection;