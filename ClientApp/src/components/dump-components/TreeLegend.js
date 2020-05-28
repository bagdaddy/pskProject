import React from 'react';

const TreeLegend = props => {
    console.log(props.ownTree);
    return (
        <div className="legend">
            <ul>
                <li>
                    <div className="circle green"></div> <span>{props.ownTree ? "Subjects you've learned" :  "Subjects " + props.name + " knows" }</span>
                </li>
                <li>
                    <div className="circle blue"></div> <span>{props.ownTree ? "Subjects your team has learned" : "Subjects " + props.name + " team has learned"}</span>
                </li>
                <li>
                    <div className="circle gray"></div> <span>Subjects no one has learned</span>
                </li>

            </ul>
        </div>
    )
};

export default TreeLegend;