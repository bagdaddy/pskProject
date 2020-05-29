import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Employee from './components/Employee';
import Subjects from './components/Subjects';
import AddSubject from './components/AddSubject';
import Subject from './components/Subject';
import LearningTree from './components/LearningTree';
import Login from './components/Login';
import Profile from './components/Profile';
import Logout from './components/Logout';
import { ProtectedRoute, UnauthenticatedRoute } from './components/routes/Routes';
import EditSubject from './components/EditSubject';
import Invitation from './components/Invitation';
import Register from './components/Register';

export default class App extends Component {
  static displayName = "Mokymosi dienu kalendorius";
  
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path="/login" component={Login} />
        <Route path="/logout" component={Logout} />
        <Route path='/me' component={Profile} />
        <Route path='/employee/:id' component={Employee} />
        <Route path='/learningTree/:id?' component={LearningTree} />
        <Route path='/subjects' component={Subjects} />
        <Route path='/add-subject' component={AddSubject} />
        <Route path='/subject/:id' component={Subject} />
        <Route path='/edit-subject/:id' component={EditSubject} />
        <Route path='/register/:id' component={Register} />
        {/*Kol kas paliekam uzkomentuota, kad nereiktu prisijungti kiekviena karta kai padarom kazkokiu pakeitimu*/ }
        {/* <ProtectedRoute exact path='/' component={Home} />
        <UnauthenticatedRoute path="/login" component={Login} />
        <ProtectedRoute path="/logout" component={Logout} />
        <ProtectedRoute path='/me' component={Profile} />
        <ProtectedRoute path='/employee/:id' component={Employee} />
        <ProtectedRoute path='/learningTree/:id?' component={LearningTree} />
        <ProtectedRoute path='/subjects' component={Subjects} />
        <ProtectedRoute path='/add-subject' component={AddSubject} />
        <ProtectedRoute path='/subject/:id' component={Subject} /> */}
        <Route path='/invitation' component={Invitation} />
      </Layout>
    );
  }
}
