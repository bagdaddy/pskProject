import React, { useEffect, useState, useRef } from 'react';
import TeamList from './dump-components/TeamList';
import SubjectList from './dump-components/SubjectList';
import Loader from './dump-components/Loader';
import { NotFound } from './dump-components/Error';
import { Form, FormGroup, Label, Input, Button, FormFeedback, FormText, CardBody } from 'reactstrap';
import getFlatListOfSubordinates from './dump-components/getSubordinates';
import auth from './Auth';
import { Card, CardTitle, CardText, CardLink, Row, Col, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

function fetchTeamData(){
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(true);
    async function fetchRoles(userId){
        const response = await fetch('api/UserRole/' + userId + '/getAll');
        if(response.ok){
            return await response.json();
        }else{
            return [];
        }
    }

    async function fetchTeam(userId){
        const response = await fetch('api/GetTeams/' + userId);
        if(response.ok){
            return await response.json();
        }else{
            return {};
        }
    }

    async function fetchData(){
        const response = await auth.getCurrentUser();
        var currUser = null;
        if(response.ok){
            currUser = await response.json();
        }
        const roles = await fetchRoles(currUser.id);
        const team = await fetchTeam(currUser.id);
        setData({ roles: roles, currUser : currUser, team: team });
        setLoading(false);
    }

    useEffect(() => {
        fetchData();
    }, []);
    return [data, loading];
}

function Team(props) {
    const [data, loading] = fetchTeamData();
    const [roles, setRoles] = useState([]);
    const [currentUser, setCurrentUser] = useState({});
    const [team, setTeam] = useState({});
    let newTeam = [];
    const [roleName, setRoleName] = useState("");
    const [editingRole, setEditingRole] = useState({});
    const [deletingRole, setDeletingRole] = useState({});
    const [modal, setModal] = useState(false);
    const [uSureModal, setUSureModal] = useState(false);
    const [newName, setNewName] = useState("");

    const success = useRef();
    const cantAdd = useRef();
    const error = useRef();

    const toggleUSureModal = () => setUSureModal(!uSureModal);
    const toggle = () => setModal(!modal);

    const removeEmployeeFromTeam = (employee) => {
        const newList = newTeam.filter((em) => em.id !== employee.id);
        newTeam = newList;
    }

   
    const filterTeamList = () => {
        roles.map(role => {
            role.employeeList.map(employee => {
                removeEmployeeFromTeam(employee);
            });
        });
    }
    useEffect(() => {
        setRoles(data.roles);
        var employeeList = getFlatListOfSubordinates([], data.team);
        setTeam(employeeList);
        setCurrentUser(data.currUser);
        newTeam = team;
        console.log(newTeam);
    }, [data, loading]);

    // if(team){
    //     getFilteredEmployeeList();
    // }
    
    async function insertRole() {
        let valid = true;
        if(roleName === ""){
            valid = false;
            document.getElementById("role_name").style.broder = "1px solid red";
        }

        if(!valid){
            return false;
        }
        const requestOptions = {
            method: "POST",
            headers: {'Content-Type' : 'application/json'},
            body: JSON.stringify({
                name: roleName
            })
        };
        const res = await fetch("api/UserRole/create", requestOptions);
        if(res.ok){
            window.location.reload();
        }else{
            cantAdd.current.style.display = "block";
        }
    }
    
    const handleSubmit = (event) => {
        event.preventDefault();
        insertRole();
    }

    const editUserRole = (role) => {
        setEditingRole(role);
        setNewName(editingRole.name);
        setModal(true);
    }

    const handleEditSubmit = (event) => {
        event.preventDefault();
        var requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name: newName
            })
        };

        fetch('api/UserRole/' + editingRole.id + '/edit', requestOptions)
                        .then(response => {
                            if(response.ok){
                                window.location.reload();
                            }else{
                                toggle();
                                error.current.style.display = "block";
                            }
                        });
        
    }

    const deleteUserRole = (role) => {
        setDeletingRole(role);
        toggleUSureModal();
    }

    const handleDeleteSubmit = () => {
        var requestOptions = {
            method: "DELETE"
        };

        fetch('api/UserRole/' + deletingRole.id + '/delete', requestOptions)
            .then(response => {
                if(response.ok){
                    window.location.reload();
                }else{
                    alert("Couldn't delete this user role. Maybe there are employees that belong to this group?");
                    toggleUSureModal();
                }
            })
    }
    if(team.length && roles.length){
        newTeam = team;
        filterTeamList();
        console.log(newTeam);
        console.log(team);
    }
    if(!loading){
        if(roles){
            return (
                <div className="container">
                    <div className="row">
                        <div className="col-6">
                            <div className="form-left">
                            <label ref={error} className="error" style={{display : "none"}}>Couldn't edit role</label>
                                <Form onSubmit={handleSubmit}>
                                    <FormGroup>
                                        <Label for="role_name">Add new role:</Label>
                                        <Input type="text" id="role_name" name="role_name" placeholder="e.g. Junior Developer" value={roleName} onChange={(event) => {setRoleName(event.target.value)}} />
                                        <label ref={success} className="successMsg">Role successfully added.</label>
                                        <label ref={cantAdd} className="error" style={{display : "none"}}>Couldn't add role. Maybe this name is already in use?</label>
                                    </FormGroup>
                                    <FormGroup>
                                        <Button className="btn btn-success">Add</Button>
                                    </FormGroup>
                                </Form>
                            </div>
                        </div>
                        {roles.length > 0 && 
                        
                        <div className="col-6">
                            <div className="sidebar">
                                <ul>
        
                                </ul>
                                {roles.map(
                                    (role) => (
                                        role.id && 
                                    <li key={role.id} style={{listStyleType: "none"}}>
                                        <div className="row">
                                            <div className="col-6">
                                                {role.name}
                                            </div>
                                            <div className="col-6">
                                                <a data-id={role.id} onClick={(event) => {event.preventDefault(); editUserRole(role);}} >edit</a>
                                                <a style={{marginLeft : "10px"}} data-id={role.id} onClick={(event) => {event.preventDefault(); deleteUserRole(role)}}><span style={{color : "red"}}>delete</span></a>
                                            </div>
                                        </div>
                                    </li>
                                    )
                                )}
                            </div>
                        </div>
                        }
                        
                    </div>
                    <br/>
                    <br/>
                    {roles.length > 0 && 
                    <div className="row">
                        <div className="col-12">
                            <div className="section-header">
                                <h3>Your team</h3>
                            </div>
                        </div>
                        
                        {roles.map(
                            (role) => (
                                role.id && role.employeeList.length > 0 &&
                                <div key={role.id} className="col-4" style={{marginBottom : "20px"}}>
                                    <Card>
                                        <CardBody>
                                            <CardTitle>
                                                {role.name}
                                            </CardTitle>
                                            <div className="employeeList">
                                                <TeamList history={props.history} wrapperClass="teamList" team={role.employeeList} currentUser={currentUser.id}/>                                    
                                            </div>
                                        </CardBody>
                                    </Card>
                                </div>
                            )
                        )}
                        {newTeam.length > 0 &&
                            <div className="col-4" style={{marginBottom : "20px"}}>
                                <Card>
                                    <CardBody>
                                        <CardTitle>
                                            Other Employees
                                        </CardTitle>
                                        <div className="employeeList">
                                            <TeamList history={props.history} wrapperClass="teamList" team={newTeam} currUser={currentUser.id}/>
                                        </div>
                                    </CardBody>
                                </Card>
                            </div>
                            }
                    </div>
                    }
                    <Modal isOpen={modal} toggle={toggle}>
                        <ModalHeader toggle={toggle}>Modal title</ModalHeader>
                        <ModalBody>
                            <Form onSubmit={handleEditSubmit}>
                                <FormGroup>
                                    <Label for="newName">Select a new name for this role</Label>
                                    <Input type="text" name="newName" id="newName" defaultValue={editingRole.name} onChange={(event) => setNewName(event.target.value)}/>
                                </FormGroup>
                                <FormGroup>
                                    <Input type="submit" name="submit" id="submit" value="Submit"/>
                                </FormGroup>
                            </Form>
                        </ModalBody>
                    </Modal>
                    <Modal isOpen={uSureModal} toggle={toggleUSureModal}>
                        <ModalHeader toggle={toggleUSureModal}>Are you sure you want to delete {deletingRole.name}?</ModalHeader>
                        <ModalBody>
                            <Button className="btn btn-danger" onClick={handleDeleteSubmit}>
                                Delete
                            </Button>
                            <Button style={{marginLeft : "10px"}} className="btn" onClick={() => toggleUSureModal()}>
                                Cancel
                            </Button>
                        </ModalBody>
                    </Modal>
                </div>
            )

        }else{
            return (
                <NotFound />
            )
        }
    }else{
        return(
            <Loader/>
        )
    }
    
        

}

export default Team;