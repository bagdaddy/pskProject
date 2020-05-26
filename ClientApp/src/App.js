import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import Employee from './components/Employee';
import Subjects from './components/Subjects';
import AddSubject from './components/AddSubject';
import Subject from './components/Subject';
import LearningTree from './components/LearningTree';
import Login from './components/Login';
import Profile from './components/Profile';

export default class App extends Component {
  static displayName = App.name;
  
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/me' component={Profile} />
        <Route path='/employee/:id' component={Employee} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/learningTree/:id?' component={LearningTree} />
        <Route path='/subjects' component={Subjects} />
        <Route path='/add-subject' component={AddSubject} />
        <Route path='/login' component={Login} />
        <Route path='/subject' component={Subject} />
      </Layout>
    );
  }
}
