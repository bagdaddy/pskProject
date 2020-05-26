import React from 'react';

const TreeLegend = props => {
    return (
        <div className="legend">
            <ul>
                <li>
                    <div className="circle green"></div> <span>Subjects you've learned</span>
                </li>
                <li>
                    <div className="circle blue"></div> <span>Subjects your team has learned</span>
                </li>
                <li>
                    <div className="circle gray"></div> <span>Subjects noone has learned</span>
                </li>

            </ul>
        </div>
    )
};

export default TreeLegend;