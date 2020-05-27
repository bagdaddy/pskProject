import React, { Children, useRef, useState, useEffect } from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import testDays from './testDays'
import testSubjects from './testSubjects'
import SubjectSelection from './SubjectSelection'
import './react-big-calendar.css'

moment.locale('ko', {
    week: {
        dow: 1,
        doy: 1,
    },
});

const localizer = momentLocalizer(moment)
const now = new Date();

var Holidays = require('date-holidays')
var hd = new Holidays()
hd.init('LT')


const EventCalendar = (props) => {

    const [dates, setDates] = useState([]);
    const [calendarEvents, setCalendarEvents] = useState([]);
    const [subjects, setSubjects] = useState(null);


    const fetchSubjects = React.useCallback(() => {
        fetch("api/GetSubjects")
            .then(response => response.json())
            .then(data => setSubjects(data))
            .catch((error) => {
                console.log(error);
            })});

    const fetchDates = React.useCallback(() => {
        fetch("api/GetDates/${employeeId}")
            .then(response => response.json())
            .then(data => setDates(data))
            .catch((error) => {
                console.log(error);
            })});


    useEffect(() => {  
        setDates(testDays);
    },[]);

    useEffect(() => {  
        setEvents();
    },[dates]);

    const setEvents = () => {
        var i;
        var j;
        var calEvents = []
        for(i = 0; i<dates.length; ++i){
            for(j=0; j<dates[i].subjects.length; ++j){
                if(dates[i].subjects[j] != null){
                    calEvents.push({id:1, title:getSubjectName(dates[i].subjects[j]), start:dates[i].date, end:dates[i].date})
                }
            }
        }
        setCalendarEvents(calEvents)
    };


    const getSubjectName = (subjectId) =>{
        var i;
        for(i=0;i<testSubjects.length;++i){
            if(testSubjects[i].id.toString() === subjectId.toString()){
                return(testSubjects[i].title)
            }
        }        
    }

    const handleSelect = ({ start }) => {
        if(WorkDay(start)){
            childRef.current.dateSetup(start)
            childRef.current.toggle()
        }
    }

    const childRef = useRef();

    return(
        <div className="rbc-calendar">
            <Calendar
            localizer={localizer}
            events={calendarEvents}
            selectable={true}
            views={ ['month', 'agenda'] }
            startAccessor="start"
            endAccessor="end"
            onSelectSlot={handleSelect}
            onSelectEvent={handleSelect}
            components={{
            dateCellWrapper: ColoredDateCellWrapper
            }}/>
            <SubjectSelection dates={dates} setDates={setDates} ref={childRef}/>
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

export default EventCalendar