const getFlatListOfSubordinates = (children, subordinates) => {
    if(!subordinates){
        return [];
    }
    subordinates.forEach(subordinate => {
        children.push(subordinate);
        if (subordinate.subordinates.length > 0) {
            getFlatListOfSubordinates(children, subordinate.subordinates);
        }
    });
    return children;
};

export default getFlatListOfSubordinates;