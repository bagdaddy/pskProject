import React from 'react';

const Subjects = props => {
    //gauti visus subjectus per ajax

    let subjects = [
        {
            parent: null,
            name: "PHP",
            id: 1
        },
        {
            parent: 1,
            name: "PHP7",
            id: 2
        },
        {
            parent: null,
            name: "WordPress",
            id: 3
        },
        {
            parent: 1,
            name: "Laravel",
            id: 4
        }
    ];

    return (
        <div>
            <h2>Galimų temų sąrašas:</h2>
            <div className="row">
                <div className="col-8">
                    <ul>
                        {subjects.map(subject => (
                            <li key={subject.id}>
                                <a href={"subject?id=" + subject.id}>{subject.name}</a>
                            </li>
                        ))}
                    </ul>
                </div>
                <div className="col-4">
                    <a className="btn btn-success" href="/add-subject">Pridėti naują temą</a>
                </div>
            </div>
        </div>
    )

};

export default Subjects;