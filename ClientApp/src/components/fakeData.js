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

export const data = [
    {
        name: 'PHP',
        id: '1',
        employees: [
            {
                id: "1",
                name: "Matas Kirvaitis"
            },
            {
                id: "3",
                name: "Justinas Kondroska"
            }
        ],
        subjects: [
            {
                name: 'PHP7',
                id: '2',
                employees: [],
                subjects: []
            },
            {
                name: "WordPress",
                id: '3',
                employees: [
                    {
                        name: "Justinas Kondratavicius",
                        id: "2"
                    },
                    {
                        id: "3",
                        name: "Justinas Kondroska"
                    }
                ],
                subjects: []
            },
            {
                name: "Laravel",
                id: "4",
                employees: [
                    {
                        id: "1",
                        name: "Matas Kirvaitis"
                    },
                    {
                        name: "Justinas Kondratavicius",
                        id: "2"
                    },
                ],
                subjects: []
            }
        ]
    },
    {
        name: "JavaScript",
        id: "5",
        employees:[
            {
                name: "Justinas Konroska",
                id: "3"
            }
        ],
        subjects:[
            {
                name: "React",
                id: "6",
                employees: [
                    {
                        name: "Justinas Kondroska",
                        id: "3"
                    }
                ],
                subjects:[]
            },
            {
                name: "jQuery",
                id: "7",
                employees: [
                    {
                        name:"Justinas Kondroska",
                        id: "3"
                    }
                ],
                subjects: []
            },
            {
                name: "VueJS",
                id: "8",
                employees: [],
                subjects: []
            },
            {
                name: "Angular",
                id: "9",
                subjects: [],
                employees: []
            }
        ]
    },
    {
        name: "Java",
        id: "10",
        employees: [],
        subjects:[
            {
                name: "Java8",
                id: "11",
                subjects: [],
                employees: [
                    {
                        name: "Justinas Kondroska",
                        id: "3"
                    }
                ]
            }
        ]
    },
    {
        name: ".NET",
        id: "12",
        subjects: [],
        employees: [
            {
                name: "Matas Kirvaitis",
                id: "1"
            }
        ]
    }
];



