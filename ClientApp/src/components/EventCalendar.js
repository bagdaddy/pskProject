import React, { Children, useRef, useState, useEffect } from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import SubjectSelection from './SubjectSelection';
import CalendarDayPreview from './CalendarDayPreview';
import {withRouter} from 'react-router-dom';
import './react-big-calendar.css';

moment.locale('ko', {
    week: {
        dow: 1,
        doy: 1,
    },
});

const localizer = momentLocalizer(moment);

var Holidays = require('date-holidays')
var hd = new Holidays()
hd.init('LT')


const EventCalendar = (props) => {

    const [dates, setDates] = useState([]);
    const [calendarEvents, setCalendarEvents] = useState([]);
    const [subjects, setSubjects] = useState(null);
    const [employee, setEmployee] = useState(null);
    const [loading, setLoading] = useState(true);
    const [updated, setUpdated] = useState(true);

    function isPersonalCalendar(){
        if(window.location.pathname === "/calendar"){
            return(<SubjectSelection dates={dates} employee={employee} setUpdated={setUpdated}  updated={updated} ref={childRef}/>)
        }else{
            return(<CalendarDayPreview dates={dates} ref={childRef}/>)
        }
    }

    async function fetchAllSubjects() {
        const response = await fetch('api/GetAllSubjects/');
        const json = await response.json();
        setSubjects(json);
        setLoading(false);
      }  

    async function fetchDays() {
      const response = await fetch('api/Days/GetDayByEmployeeId/' + employee.id);
      const days = await response.json();
      setDates(days);
      setLoading(false);
    }  

    async function fetchEmployee() {
        const response = await fetch('api/Employees/' + '5a677c6e-56e5-4cf1-9c64-157b483e8eff');
        const employee = await response.json();
        setEmployee(employee);
    }  


    useEffect(() => {  
        employee?fetchDays():null;
    },[updated]);  

    useEffect(() => {  
        fetchAllSubjects().then(result => fetchEmployee());
    },[loading]);   
    
    useEffect(() => {  
        employee?fetchDays():null;
        setEvents();
    },[employee]);  

    useEffect(() => {  
        subjects?setSubjects(subjects.sort((a, b) => a.name.localeCompare(b.name))):null;        
    },[loading]);

    useEffect(() => {  
        if(dates){
        setEvents();
        }
    },[dates]);

    const setEvents = () => {
        var i;
        var j;
        var calEvents = []
        for(i = 0; i<dates.length; ++i){
            for(j=0; j<dates[i].daySubjectList.length; ++j){
                if(dates[i].daySubjectList[j] != null){
                    calEvents.push({id:1, title:getSubjectName(dates[i].daySubjectList[j].subjectId), start:dates[i].date, end:dates[i].date})
                }
            }
        }
        setCalendarEvents(calEvents)
    };

    const getSubjectName = (subjectId) =>{
        if(subjects){
            var i;
            for(i=0;i<subjects.length;++i){
                if(subjects[i].id.toString() === subjectId.toString()){
                    return(subjects[i].name)
                }
            }  
        }      
    }

    const handleSelect = ({ start }) => {
        var curDay =new Date(moment(start).toDate().setHours(0,0,0,0));
        console.log(curDay)
        var id;
        var day = DayExists(curDay);
        day? id=day.id : id=null;

        if(WorkDay(curDay)){
            childRef.current.daySetup(curDay, subjects, id);
            childRef.current.toggle();
        }
    }

    function DayExists(date){
        return dates.find((d) => {
            var curDay =new Date(moment(d.date).toDate().setHours(0,0,0,0));
            return curDay.getTime() === date.getTime();
          })
    }

    const childRef = useRef();

    return(
        loading?
        <div>Loading...</div>:
        <div className="rbc-calendar">
            <Calendar
            localizer={localizer}
            events={calendarEvents}
            selectable={true}
            views={ ['month'] }
            startAccessor="start"
            endAccessor="end"
            onSelectSlot={handleSelect}
            onSelectEvent={handleSelect}
            components={{
            dateCellWrapper: ColoredDateCellWrapper
            }}/>
            {isPersonalCalendar()}
        </div>
    )
  }


const ColoredDateCellWrapper = (props) =>
    React.cloneElement(Children.only(props.children), {
        style: {
            ...props.children.style,
            backgroundColor: WorkDay(props.value) ?  "" : "lightgrey",
        },
        selectable : WorkDay(props.value) ?  1 : 0
    });

function WorkDay(date){
    if((hd.isHoliday(date) && hd.isHoliday(date).type === "public") || date.getDay() === 6 || date.getDay() === 0){
        return false
    } else{
        return true
    }
}

export default withRouter(EventCalendar)