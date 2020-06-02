import React, {connect, mapStateToProps, matchDispatchToProps} from 'react';
import { withRouter } from 'react-router';

const SubjectList = props => {
    return (
        <div className={props.wrapperClass}>
            {props.subjects.map(subject => (
                <div key={subject.id} className={props.itemClass ? props.itemClass : ""}>
                    <a onClick={() => props.history.push("/subject/" + subject.id)}>{subject.name}</a>
                </div>
            ))}
        </div>

    );
};

export default SubjectList;