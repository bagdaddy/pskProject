import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Input, Label, Form, FormGroup } from 'reactstrap';

const SubjectSelection = forwardRef((props, ref) => {
  
    useImperativeHandle(ref, () => ({toggle, daySetup}));

    const [modal, setModal] = useState(false);
    const [date, setDate] = useState(null);
    const [subjects, setSubjets] = useState([]);
    const [id, setId] = useState(null);

    
    const [comment, setComment] = useState("");
    const [subjectsSelected, setSubjectsSelected] = useState([]);
    const [num, setNum] = useState(null);

    const now = new Date();

    const toggle = () => {
        setModal(!modal);

    };
    const daySetup = (date, subjects, id) =>{
        setDate(date);
        var res = subjects.filter(item1 => 
            !props.employee.subjects.some(item2 => (item2.id === item1.id)))
        setSubjets(res);
        setId(id);
    } 
    useEffect(() => {  
        var i;
        if(props.dates.length && date){
            for(i = 0; i<props.dates.length; ++i){
                if(props.dates[i].date.getTime() === date.getTime()){
                    setSubjectsSelected(props.dates[i].subjects);
                    setComment(props.dates[i].comment);
                    setNum(i);
                }
            }
        }
        return function cleanup() {
            setSubjectsSelected([]);
            setComment("");
            setNum(null);
            setId(null)
          };
    },[date]);

    async function postDay(day) {
        const response = await fetch('api/Days/CreateNewDay/', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify({ EmployeesId: props.employee.id, Date: day.date, SubjectList: day.subjects, Comment: day.comment })
      })
    }

    async function deleteDay() {
        const response = await fetch('api/Days/DeleteDay/' + id)
        const json = await response.json();
    } 

    async function updateDay(day) {
        const response = await fetch('api/Days/UpdateDay/', {
          method: 'PUT',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify({Id: day.id, EmployeeId: props.employee.id, Date: day.date, SubjectList: day.subjects, Comment: day.comment })
      })
    }

    const onDeleteButtonClick = () => {
        if(subjectsSelected[0]){
            if(num){
                let dataArray = [...props.dates];
                dataArray.splice(num, 1)
                props.setDates(dataArray)
                deleteDay(id);
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
                props.setDates(dataArray); //auto-updates map, may be deletable after database
                updateDay({id: id, date:date, subjects:subjectsTrimmed, comment:comment})
            }else{
                var subjectsTrimmed = subjectsSelected.filter(x => x).join(', ');
                props.setDates(props.dates.concat({id:16, date:date, subjects:subjectsSelected, comment:comment})); //auto-updates map, may be deletable after database
                postDay({date:date, subjects:subjectsTrimmed, comment:comment})
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
                                <option key={subject.id} value={subject.id}>{subject.name}</option>))};
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
                                <option key={subject.id} value={subject.id}>{subject.name}</option>))};
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
                                <option key={subject.id} value={subject.id}>{subject.name}</option>))};
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
                                <option key={subject.id} value={subject.id}>{subject.name}</option>))};
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