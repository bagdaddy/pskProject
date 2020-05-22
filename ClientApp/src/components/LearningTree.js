import React, { useEffect, useState } from 'react';
import Tree from 'react-d3-tree';
import { data } from './fakeData.js';

const circle = {
    shape: 'circle',
    shapeProps: {
        r: 10,
        fill: 'grey',
    }
};

const circleLearntByYou = {
    shape: 'circle',
    shapeProps: {
        r: 10,
        fill: '#3CB043',
    }
};

const circleLearntByTeam = {
    shape: 'circle',
    shapeProps: {
        r: 10,
        fill: '#2832C2',
    }
}

const authenticatedUserId = "1";

const SubjectInfo = props => {
    let subjectData = props.data;
    if (Object.keys(subjectData).length > 0) {
        if (parseInt(subjectData.attributes.subjectId) === -1) {
            return (
                <div></div>
            )
        }
    } else {
        return (
            <div></div>
        )
    }

    if (Object.keys(props.data).length > 0 && subjectData.attributes.employees.length > 0) {
        return (
            <div className="subjectInfo">
                <p className="title">{subjectData.name}</p>
                <ul className="employeeList">
                    {subjectData.attributes.employees.map(employee => (
                        <li><a href={"/profile?id=" + employee.id}>{employee.name}</a></li>
                    ))}
                </ul>
            </div>
        )
    } else {
        return (
            <div className="subjectInfo">
                <p className="title">{subjectData.name}</p>
                <div className="info">
                    <p>Your team has yet to learn this subject.</p>
                </div>
            </div>
        )
    }


};

class NodeLabel extends React.PureComponent {
    render() {
        const { className, nodeData } = this.props
        return (
            <div className={className}>
                <div className="subjectName">
                    <p>{nodeData.name}</p>
                </div>

            </div>
        )
    }
}

const textProps = { x: -20, y: 20 };
const LearningTree = props => {
    const [subjects, setSubjects] = useState([]);
    const [treeData, setTreeData] = useState([]);
    const [displayedNode, setDisplayedNode] = useState({});

    document.body.classList.add("learning-tree");

    const fetchData = React.useCallback(() => {
        fetch("api/GetSubjects")
            .then(response => response.json())
            .then(data => setSubjects(data))
            .catch((error) => {
                console.log(error);
            });
    });

    useEffect(() => {
        fetchData();
        let myFakeTreeData = [
            {
                name: "Top level",
                nodeSvgShape: circle,
                attributes: {
                    subjectId: "-1"
                },
                children: []
            }
        ];

        function formSubjectObj(subject) {
            let selectedNodeSvgShape = circle;
            if (subject.employees.length > 0) {
                if (subject.employees.filter(e => e.id === authenticatedUserId).length > 0) {
                    selectedNodeSvgShape = circleLearntByYou;
                } else {
                    selectedNodeSvgShape = circleLearntByTeam;
                }
            }

            let subjectToPush = {
                name: subject.name,
                nodeSvgShape: selectedNodeSvgShape,
                attributes: {
                    subjectId: subject.id,
                    employees: subject.employees
                },
                children: getChildren(subject)
            };
            return subjectToPush;
        }

        function getChildren(subjectData) {
            let children = [];
            subjectData.subjects.forEach(subject => {
                let subjectToPush = formSubjectObj(subject);
                children.push(subjectToPush);
            });
            return children;
        }

        data.forEach(subject => {
            let subjectToPush = formSubjectObj(subject);
            myFakeTreeData[0].children.push(subjectToPush);
        });
        setTreeData(myFakeTreeData);
    }, []);

    console.log(treeData);


    const handleClick = React.useCallback((event, node) => {
        console.log('handle click ', event);
        console.log('handle click node', node);
        setDisplayedNode(event);
    });

    return (
        <div className="treeWrapper" style={{ width: "100%", height: "1000px" }}>
            <SubjectInfo data={displayedNode} />
            {treeData.length > 0 && <Tree data={treeData} collapsible={false} onClick={handleClick} allowForeignObjects transitionDuration={0} nodeLabelComponent={{
                render: <NodeLabel className='myLabelComponentInSvg' />,
                foreignObjectWrapper: {
                    y: 0,
                    x: 0
                }
            }}
                nodeSize={{ x: 350, y: 70 }} />
            }
        </div>
    );
}

export default LearningTree;