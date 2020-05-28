import React, { useEffect, useState } from 'react';
import Tree from 'react-d3-tree';
import Loader from './dump-components/Loader';
import TreeLegend from './dump-components/TreeLegend.js';
import { NotAuthorized } from './dump-components/Error';

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
                        <li><a href={"/profile/" + employee.id}>{employee.firstName} {employee.lastName}</a></li>
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

function getDataAsync(employeeId) {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    async function fetchData() {
        const employeeRes = employeeId ? await fetch("api/Employees/" + employeeId) : await fetch("api/Auth/self");
        const employee = employeeRes.ok ? await employeeRes.json() : {};
        const sRes = await fetch("api/GetSubjects");
        const s = sRes.ok ? await sRes.json() : [];
        const eRes = await fetch('api/Employees');
        const e = eRes.ok ? await eRes.json() : [];
        setData({ subjects: s, employees: e, employee: employee });
        setLoading(false);
    }

    useEffect(() => {
        fetchData();
    }, []);
    return [data, loading];
}

const LearningTree = props => {
    const [subjects, setSubjects] = useState([]);
    const [employees, setEmployees] = useState([]);
    const [employee, setEmployee] = useState({});
    const [treeData, setTreeData] = useState([]);
    const [displayedNode, setDisplayedNode] = useState({});
    const [data, loading] = getDataAsync(props.match.params.id);

    document.body.classList.add("learning-tree");

    useEffect(() => {
        setEmployee(data.employee);
        setSubjects(data.subjects);
        setEmployees(data.employees);
        let myTreeData = [
            {
                name: "Top level",
                nodeSvgShape: circle,
                attributes: {
                    subjectId: "-1"
                },
                children: []
            }
        ];
        if (!loading) {
            console.log(subjects);
            console.log(employees);
            data.subjects.forEach(subject => {
                let subjectToPush = formSubjectObj(subject);
                myTreeData[0].children.push(subjectToPush);
            });
            setTreeData(myTreeData);
        }
    }, [data, loading]);


    const handleClick = React.useCallback((event, node) => {
        setDisplayedNode(event);
    });

    const getEmployeesForSubject = (subject) => {
        let employeeArr = [];
        employees.forEach(employee => {
            if (employee.subjects.filter(function (e) { return e.id === subject.id; }).length > 0) {
                employeeArr.push(employee);
            }
        });
        return employeeArr;
    };

    function formSubjectObj(subject) {
        let selectedNodeSvgShape = circle;
        subject.employees = getEmployeesForSubject(subject);
        if (subject.employees.length > 0) {
            if (subject.employees.filter(e => e.id === employee.id).length > 0) {
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
        subjectData.childSubjects.forEach(subject => {
            let subjectToPush = formSubjectObj(subject);
            children.push(subjectToPush);
        });
        return children;
    }
    if (!loading) {
        if(!employee){
            return (
                <div className="treeWrapper" style={{ width: "100%", height: "1000px" }}>
                    <TreeLegend name={employee.name} ownTree={props.match.params.id ? false : true} />
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
        }else{
            return(
                <NotAuthorized/>
            )
        }
        
    } else {
        return (
            <Loader />
        )
    }

}

export default LearningTree;