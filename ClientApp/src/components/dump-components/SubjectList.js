import React from 'react';

const SubjectList = props => {
    return (
        <div>
            <ul>
                {props.subjects.map(subject => (
                    <li key={subject.id}>
                        <a href={"/subject?id=" + subject.id}>{subject.name}</a>
                    </li>
                ))}
            </ul>
        </div>

    );
};

export default SubjectList;