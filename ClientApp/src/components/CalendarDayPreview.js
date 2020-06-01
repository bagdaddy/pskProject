import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import moment from 'moment';

const CalendarDayPreview = forwardRef((props, ref) => {

    useImperativeHandle(ref, () => ({toggle, daySetup}));


  const [modal, setModal] = useState(false);
  const [date, setDate] = useState(null);
  const [days, setDays] = useState([]);
  const [subjects, setSubjets] = useState([]);
  const [employees, setEmployees] = useState([]);
 

  const toggle = () => setModal(!modal);

  const daySetup = (date, subjects, id) =>{
    setDate(date);
    setSubjets(subjects);
    } 


    async function fetchEmployees() {
        const response = await fetch('api/Employees/');
        const employeeList = await response.json();
        setEmployees(employeeList);
      }  

    function getSubjectName(id){
        var found = subjects.find(function (element) {
            return element.id === id;
          });
          return found? found.name:"NO SUBJECT";
    }  

    function getEmployee(id){
      var found = employees.find(function (element) {
          return element.id === id;
        });      
        return found? found:"NO EMPLOYEE";
  } 
    
    useEffect(() => { fetchEmployees() },[]);

    useEffect(() => {  
        var i;
        var daysToAdd = [];
        if(props.dates.length && date){
            for(i = 0; i<props.dates.length; ++i){
              var curDay = new Date(moment(props.dates[i].date).toDate().setHours(4,0,0,0));
                if(curDay.getTime() === date.getTime()){
                    daysToAdd.push(props.dates[i])
                }
            }
        }
        setDays(daysToAdd);
        
        return function cleanup() {
          };
    },[date]);


    const NestedList = () => (
      <ul>
        {days.map((day, index) => (
          <ul key={index}>
            <h4>List {getEmployee(day.employeeId)?getEmployee(day.employeeId).firstName:"ss"}</h4>
            {day.daySubjectList.map(item => (
              <li key={item}>
                <div>{getSubjectName(item.subject.id)}</div>
              </li>
            ))}
          </ul>
        ))}
      </ul>
    );
    
  return (
    <div>
      <Modal isOpen={modal} toggle={toggle}>
        <ModalHeader toggle={toggle}>{date? date.toString().substring(0, 10):""}</ModalHeader>
        <ModalBody>{NestedList()}</ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggle}>Close</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
})

export default CalendarDayPreview;