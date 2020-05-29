import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import moment from 'moment';

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


    function getList(){
      if(days){
        return( <div>
          {days.map(day => (<li key={day.employeeId}>{fetchEmployee(day.employeeId).firstName}</li>,
          day.daySubjectList.map(subject =>
              <li key={subject}>{getSubjectName(subject)}</li>)))}                 
          </div>)
      }else{
        return(<div>Loading...</div>)
      }
    }
  return (
    <div>
      <Modal isOpen={modal} toggle={toggle}>
        <ModalHeader toggle={toggle}>{date? date.toString().substring(0, 10):""}</ModalHeader>
        <ModalBody>{getList()}</ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggle}>Cancel</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
})

export default CalendarDayPreview;