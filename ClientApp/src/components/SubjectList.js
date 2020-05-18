import React from 'react';

const SubjectList = props => {
    return (
        <div>
            <h3>Jūsų išmoktos temos:</h3>
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