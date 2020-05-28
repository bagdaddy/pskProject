import React from 'react';

const TreeLegend = props => {
    console.log(props.ownTree);
    return (
        <div className="legend">
            <ul>
                <li>
                    <div className="circle green"></div> <span>{props.ownTree ? "Subjects you've learned" :  "Subjects " + props.name + " knows" }</span>
                </li>
                {props.ownsTeam && <li>
                    <div className="circle blue"></div> <span>{props.ownTree ? "Subjects your team has learned" : "Subjects " + props.name + " team has learned"}</span>
                </li>}
                <li>
                    <div className="circle gray"></div> <span>{props.ownsTeam ? "Subjects no one has learned" : "Subjects " + props.name + " doesn't know"}</span>
                </li>

            </ul>
        </div>
    )
};

export default TreeLegend;