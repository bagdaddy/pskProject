import React from 'react';

const SubjectList = props => {
    return (
        <div className={props.wrapperClass}>
            {props.subjects.map(subject => (
                <div className={props.itemClass ? props.itemClass : ""}>
                    <a href={"/subject/" + subject.id}>{subject.name}</a>
                </div>
            ))}
        </div>

    );
};

export default SubjectList;