import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Input, Label, Form, FormGroup } from 'reactstrap';
import moment from 'moment';

const SubjectSelection = forwardRef((props, ref) => {
  
    useImperativeHandle(ref, () => ({toggle, daySetup}));

    const [modal, setModal] = useState(false);
    const [date, setDate] = useState(null);
    const [subjects, setSubjets] = useState([]);
    const [id, setId] = useState(null);
    const [daysThisQuarter, setDaysThisQuarter] = useState(0);
    const [quarterRestriction, setQuarterRestriction] = useState(0);

    
    const [comment, setComment] = useState("");
    const [subjectsSelected, setSubjectsSelected] = useState([]);
    const [num, setNum] = useState(null);
    const [employeeId, setEmployeeId] = useState(null); 

    const now = new Date();

    const toggle = () => {
        setModal(!modal);
    };

    const daySetup = (date, subjects, id) =>{
        setDate(date);
        var res = subjects.filter(item1 => 
            !props.employee.subjects.some(item2 => (item2.name === item1.name)))
        setSubjets(res);
        setId(id);
    } 

    useEffect(() => {
        getQuarterRestrictions();
    },[employeeId])

    useEffect(() => {  
        var i;

        if(props.dates.length && date){
            for(i = 0; i<props.dates.length; ++i){
                var curDay =new Date(moment(props.dates[i].date).toDate().setHours(4,0,0,0));
                if(curDay.getTime() === date.getTime()){
                    let result = props.dates[i].daySubjectList.map(a => a.subjectId);
                    setSubjectsSelected(result);
                    setComment(props.dates[i].comment);
                    setNum(i);
                }
            }
        }
        if(props.employee){
            setEmployeeId(props.employee.id);
            getDatesThisQuarter();
        }

        return function cleanup() {
            setSubjectsSelected([]);
            setComment("");
            setNum(null);
            setId(null);
            setDaysThisQuarter(0);
          };
    },[date]);

    async function getDatesThisQuarter() {
        var year = getQuarter(date)[0];
        var quarter = getQuarter(date)[1];
        const response = await fetch('api/Days/GetDaysInQuarter/' + employeeId + "/" + year + "/" + quarter);
        const days = await response.json();
        setDaysThisQuarter(days);
      }  

    async function getQuarterRestrictions() {
        const response = await fetch('/api/GetRestriction/' + employeeId);
        const restriction = await response.json();
        setQuarterRestriction(restriction);
      }

    async function postDay(day) {
        delete Array.prototype.toJSON;
        const response = await fetch('api/Days/CreateDay/', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify({ employeeId: props.employee.id, date: day.date, subjectList: day.subjects, comment: day.comment })
        }).catch(function() {
            console.log("error");
        });
        props.setUpdated(!props.updated); 
    }

    async function deleteDay() {
        const response = await fetch('api/Days/DeleteDay/' + id)
        const json = await response.json().catch(function() {
            console.log("error");
        });;
        props.setUpdated(!props.updated)
    } 

    async function updateDay(day) {
        const response = await fetch('api/Days/ChangeDay/' + id, {
          method: 'PUT',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify({Id: day.id, EmployeeId: props.employee.id, Date: day.date, SubjectList: day.subjects, Comment: day.comment })
        }).catch(function() {
            console.log("error");
        });
        props.setUpdated(!props.updated); 
    }

    const onDeleteButtonClick = () => {
        if(subjectsSelected[0]){
            if(num != null){
                deleteDay(id);
            }
            toggle();
        }
    }

    const onSubmitButtonClick = () => {
        if(subjectsSelected[0]){
            var filtered = subjectsSelected.filter(function (el) {
                return el != null;
              });
            if(num != null){
                updateDay({id: id, date:date, subjects:filtered, comment:comment})
            }else{                
                postDay({date:date, subjects:filtered, comment:comment})
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
        return !HasDayPassed() && num != null? 1:0
    }

    function IsDateAddableAndChangable(){
        if(num != null){
            return !HasDayPassed()? 1:0
        }else{
            return !HasDayPassed() && !DoesDayHaveAdjascentDays() && !IsMaxThisQuarterReacher() 
        }
    }

    function DoesDayHaveAdjascentDays(){
        var minus = new Date(date.valueOf() - 86400000);
        var plus = new Date(date.valueOf() + 86400000);
        if(DoesDateExist(minus) || DoesDateExist(plus)){
            return true;
        }else{
            return false;
        }
    }

    function DoesDateExist(date){
        var i;
        if(props.dates.length && date){
            for(i = 0; i<props.dates.length; ++i){
                var curDay =new Date(moment(props.dates[i].date).toDate().setHours(4,0,0,0));
                if(curDay.getTime() === date.getTime()){
                    return true;
                }
            }
            return false;
        }
    }

    function getQuarter(d) {
        d = d || new Date();
        var m = Math.floor(d.getMonth() / 3)+1;
        m -= m > 4 ? 4 : 0;
        var y = d.getFullYear();
        return [y,m];
    }

    function IsMaxThisQuarterReacher() {
        if(daysThisQuarter >= quarterRestriction){
            return true;
        }else{
            return false;
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
                            defaultValue={subjectsSelected[0]?subjectsSelected[0].id:'DEFAULT'} 
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