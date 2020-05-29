import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

const CalendarDayPreview = forwardRef((props, ref) => {

    useImperativeHandle(ref, () => ({toggle, daySetup}));


  const [modal, setModal] = useState(false);
  const [date, setDate] = useState(null);
  const [days, setDays] = useState([]);
  const [subjects, setSubjets] = useState([]);

  const toggle = () => setModal(!modal);

  const daySetup = (date, subjects, id) =>{
    setDate(date);
    setSubjets(subjects);
    } 


    async function fetchEmployee(id) {
        const response = await fetch('api/Employees/' + id);
        const employee = await response.json();
        return employee;
      }  

    function getSubjectName(id){
        var found = subjects.find(function (element) {
            return element.id === id;
          });
          return found? found.name:"NO SUBJECT NAME";
    }  
    

    useEffect(() => {  
        var i;
        var daysToAdd = [];
        if(props.dates.length && date){
            for(i = 0; i<props.dates.length; ++i){
                if(props.dates[i].date.getTime() === date.getTime()){
                    daysToAdd.push(props.dates[i])
                }
            }
        }
        setDays(daysToAdd);
        
        return function cleanup() {
          };
    },[date]);

  return (
    <div>
      <Modal isOpen={modal} toggle={toggle}>
        <ModalHeader toggle={toggle}>{date? date.toString().substring(0, 10):""}</ModalHeader>
        <ModalBody>
            <div>
            {days.map(day => (<div>{fetchEmployee(day.employeeId).name}</div>,
            day.subjects.map(subject => /*(console.log(getSubjectName(subject))*/
                <li key={subject}>{getSubjectName(subject)}</li>)))}                 
            </div>
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggle}>Cancel</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
})

export default CalendarDayPreview;