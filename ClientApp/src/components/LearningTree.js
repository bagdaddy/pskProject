import React, { useEffect, useState } from 'react';
import Tree from 'react-d3-tree';

const styles = {
    links: {

    },
    nodes: {
        node: {
            circle: {},
            name: {
                border: 1,
            },
            attributes: {},
        },
        leafNode: {
            circle: {},
            name: {},
            attributes: {},
        },
    },
}
const rect = {
    shape: 'rect',
    shapeProps: {
        width: 20,
        height: 20,
        x: -10,
        y: -10,
        fill: 'red',
    }
};
const circle = {
    shape: 'circle',
    shapeProps: {
        r: 8,
        fill: 'grey',
    }
}

class NodeLabel extends React.PureComponent {
    render() {
        const { className, nodeData } = this.props
        return (
            <div className={className}>
                <div className="numOfEmployees red">1</div>
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

    const fetchData = React.useCallback(() => {
        fetch("api/GetSubjects")
            .then(response => response.json())
            .then(data => setSubjects(data))
            .catch((error) => {
                console.log(error);
            });
    });

    useEffect(() => {
        fetchData()
    }, []);
    const myTreeData = [
        {
            name: 'Top lvl',
            nodeSvgShape: circle,
            children: [
                {
                    name: 'Java',
                    attributes: {
                        subjectId: 1,
                    },
                    nodeSvgShape: circle,
                    children: [
                        {
                            name: 'Java8',
                            attributes: {
                                subjectId: "3",

                            },
                        }
                    ]
                },
                {
                    name: '.NET',
                },
            ],
        },
    ];

    const handleClick = (event, node) => {
        console.log('handle click ', event);
        console.log('handle click node', node);
    }

    return (
        <div style={{ width: "100%", height: "1000px" }}>
            <Tree data={myTreeData} collapsible={false} onClick={handleClick} allowForeignObjects styles={styles} nodeLabelComponent={{
                render: <NodeLabel className='myLabelComponentInSvg' />,
                foreignObjectWrapper: {
                    y: 0
                }
            }} />
        </div>
    );
}

export default LearningTree;