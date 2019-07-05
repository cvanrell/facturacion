import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faWrench, faTasks, faCog } from '@fortawesome/free-solid-svg-icons';

export class NavMenu extends Component {
    displayName = NavMenu.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                
                    <Navbar.Brand>
                        <Link to={'/'}>WIS SISTEMA DE FACTURACIÓN</Link>
                    </Navbar.Brand>
                    
                
                <Navbar.Collapse>
                    <Navbar.Text>
                        ADMINISTRACIÓN
                    </Navbar.Text>
                    <Nav>
                        <Link to={'/fetchclient'} exact>
                            <NavItem>
                                <FontAwesomeIcon icon='user' /> Clientes
                            </NavItem>
                        </Link>
                        <Link to={'/fetchmaintenance'} exact>
                            <NavItem>
                                <FontAwesomeIcon icon='wrench' /> Mantenimientos
                            </NavItem>
                        </Link>
                        <Link to={'/fetchproject'}>
                            <NavItem>
                                <FontAwesomeIcon icon='tasks' /> Proyectos
                            </NavItem>
                        </Link>
                    </Nav>
                    <Navbar.Text>
                        FACTURACIÓN
                    </Navbar.Text>
                    <Nav>
                        <Link to={'/'} exact>
                            <NavItem>
                                <FontAwesomeIcon icon='wrench' /> Mantenimiento
                            </NavItem>
                        </Link>
                        <Link to={'/counter'}>
                            <NavItem>
                                <FontAwesomeIcon icon='cog' /> Desarrollos
                            </NavItem>
                        </Link>
                        <Link to={'/fetchdata'}>
                            <NavItem>
                                <FontAwesomeIcon icon='tasks' /> Proyectos
                            </NavItem>
                        </Link>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
