import React from 'react';

const SubjectList = props => {
    return (
        <div className={props.wrapperClass}>
            {props.subjects.map(subject => (
                <a href={"/subject/" + subject.id}>{subject.name}</a>
            ))}
        </div>

    );
};

export default SubjectList;