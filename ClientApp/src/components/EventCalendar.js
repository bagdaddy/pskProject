import React, { Children, useRef, useState, useEffect } from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import SubjectSelection from './SubjectSelection';
import CalendarDayPreview from './CalendarDayPreview';
import {withRouter} from 'react-router-dom';
import './react-big-calendar.css';
import auth from './Auth';

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
        if(window.location.pathname === "/"){
            return(<SubjectSelection dates={dates} employee={employee} setUpdated={setUpdated}  updated={updated} ref={childRef}/>)
        }else{
            return(<CalendarDayPreview dates={dates} ref={childRef}/>)
        }
    }

    async function fetchAllSubjects() {
        const response = await fetch('api/GetAllSubjects/');
        const json = await response.json();
        setSubjects(json);
      }  


    async function fetchDays() {
        const res = await auth.getCurrentUser();
        if (res.ok) {
            const me = await res.json();
            var apiRequest = "";
            if(window.location.pathname === "/"){
                const response = await fetch('api/Days/GetDayByEmployeeId/' + me.id);
                if (response.ok) {
                    const days = await response.json();
                    setDates(days);
                    setEmployee(me);
                    setLoading(false);
                } else {
                    return [];
                }
            }else{
                const response = await fetch('api/GetTeams/' + me.id);
                if (response.ok) {
                    const teams = await response.json();
                    var days = []
                    var arr3 = []
                    var i;
                    if(teams[0].subordinates && teams[0].subordinates.length !== 0){
                        for(i = 0; i<teams[0].subordinates.length; ++i){
                            const daysResponse = await fetch('api/Days/GetDayByEmployeeId/' + teams[0].subordinates[i].id)
                            days=arr3
                            const json = await daysResponse.json()
                            arr3 = [...days, ...json]
                        }
                    }
                    arr3!==[]?setDates(arr3):null;
                    setEmployee(me);
                    arr3!==[]?setLoading(false):null;
                } else {
                    return [];
                }
            }
        }
    }

    useEffect(() => { 
        fetchAllSubjects(); 
        fetchDays();
    },[]);  

    
    useEffect(() => {  
        fetchDays();
        setEvents();
    },[updated]);  

    useEffect(() => {  
        subjects?setSubjects(subjects.sort((a, b) => a.name.localeCompare(b.name))):null;        
    },[loading]);

    useEffect(() => {  
        if(dates.length !== 0){
            setEvents();
        }
    },[dates]);

    const setEvents = () => {
        var i;
        var j;
        var calEvents = []
        if(dates!==[])
        {for(i = 0; i<dates.length; ++i){
            for(j=0; j<dates[i].daySubjectList.length; ++j){
                if(dates[i].daySubjectList[j] != null){
                    calEvents.push({id:1, title:getSubjectName(dates[i].daySubjectList[j].subjectId), start:dates[i].date, end:dates[i].date})
                }
            }
        }}
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
        var curDay =new Date(moment(start).toDate().setHours(4,0,0,0));
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
            var curDay =new Date(moment(d.date).toDate().setHours(4,0,0,0));
            return curDay.getTime() === date.getTime();
          })
    }

    const childRef = useRef();
    console.log(dates);
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