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
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'}>WIS SISTEMA DE FACTURACIÓN</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Navbar.Text>
                        ADMINISTRACIÓN
                    </Navbar.Text>
                    <Nav>
                        <LinkContainer to={'/fetchclient'} exact>
                            <NavItem>
                                <FontAwesomeIcon icon='user' /> Clientes
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/fetchmaintenance'} exact>
                            <NavItem>
                                <FontAwesomeIcon icon='wrench' /> Mantenimientos
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/fetchproject'}>
                            <NavItem>
                                <FontAwesomeIcon icon='tasks' /> Proyectos
                            </NavItem>
                        </LinkContainer>
                    </Nav>
                    <Navbar.Text>
                        FACTURACIÓN
                    </Navbar.Text>
                    <Nav>
                        <LinkContainer to={'/'} exact>
                            <NavItem>
                                <FontAwesomeIcon icon='wrench' /> Mantenimiento
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/counter'}>
                            <NavItem>
                                <FontAwesomeIcon icon='cog' /> Desarrollos
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/fetchdata'}>
                            <NavItem>
                                <FontAwesomeIcon icon='tasks' /> Proyectos
                            </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
