import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import auth from './Auth';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">TP</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">

                {auth.isAuthenticated() &&
                  <React.Fragment>
                    <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/" onClick={() => document.body.classList.remove("learning-tree")}>Calendar</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/team-calendar">Team Calendar</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/me" onClick={() => document.body.classList.remove("learning-tree")}>Profile</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/subjects" onClick={() => document.body.classList.remove("learning-tree")}>Subjects</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/learningTree">Your learning</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-red" to="/logout" onClick={() => document.body.classList.remove("learning-tree")}>Log out</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/invitation" onClick={() => document.body.classList.remove("learning-tree")}>Invite</NavLink>
                    </NavItem>
                  </React.Fragment>
                }
                {
                  !auth.isAuthenticated() &&
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/login" onClick={() => document.body.classList.remove("learning-tree")}>Login</NavLink>
                  </NavItem>
                }
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
