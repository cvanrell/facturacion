import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

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
                                <Glyphicon glyph='user' /> Clientes
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/fetchmaintenance'} exact>
                            <NavItem>
                                <Glyphicon glyph='wrench' /> Mantenimientos
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/fetchproject'}>
                            <NavItem>
                                <Glyphicon glyph='tasks' /> Proyectos
                            </NavItem>
                        </LinkContainer>
                    </Nav>
                    <Navbar.Text>
                        FACTURACIÓN
                    </Navbar.Text>
                    <Nav>
                        <LinkContainer to={'/'} exact>
                            <NavItem>
                                <Glyphicon glyph='wrench' /> Mantenimiento
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/counter'}>
                            <NavItem>
                                <Glyphicon glyph='cog' /> Desarrollos
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/fetchdata'}>
                            <NavItem>
                                <Glyphicon glyph='tasks' /> Proyectos
                            </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
