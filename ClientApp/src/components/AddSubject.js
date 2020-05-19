import React, {useState} from 'react';

const AddSubject = props => {
    let subjectName = "";

    const handleChange = (event) => {
        console.log(event.target.value);
        subjectName = event.target.value;
    };

    const handleSubmit = (event) => {
        console.log(subjectName)
        event.preventDefault();
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>
                        Temos pavadinimas:
                    </label>
                    <input type="text" className="text-field" onChange={handleChange} name="subject_name" />
                </div>
                <div className="form-group">
                    <label>
                        Tėvinė tema:
                    </label>
                    <select name="parent">
                        <option value="-1">-</option>
                        <option value="1">PHP</option>
                        <option value="2">C#</option>
                        <option value="3">Wordpress</option>
                    </select>
                </div>
                <input type="submit" value="Submit" className="btn btn-success" />
            </form>

        </div>
    );

};

export default AddSubject;