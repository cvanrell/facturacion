﻿import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';
  import { faUser, faWrench, faTasks, faCog } from '@fortawesome/free-solid-svg-icons';

export class NavMenu extends Component {
    displayName = NavMenu.name

    render() {
        return (
            <Navbar bg="dark" variant="dark" expand="lg">                
                <Navbar.Brand>
                    <Link to={'/'}>WIS SISTEMA DE FACTURACIÓN</Link>
                </Navbar.Brand>                    
                <Navbar.Toggle aria-controls="menuNav" />
                <Navbar.Collapse id="menuNav">
                    <Nav>
                        <Navbar.Text>
                            ADMINISTRACIÓN
                        </Navbar.Text>                        
                        <NavItem>
                            <Link to={'/Clients/CLI010'} className="nav-link" >
                                <i className='fas fa-user' /> Clientes
                            </Link>                        
                        </NavItem>
                        <NavItem>
                            <Link to={'/Clients/CLI050'} className="nav-link" >
                                <i className='fas fa-clipboard-list' /> Tarifas totales
                            </Link>
                        </NavItem>
                        <NavItem>
                            <Link to={'/Maintenance/MAN010'} className="nav-link" >
                                <i className='fas fa-wrench' /> Mantenimientos
                            </Link>
                        </NavItem>
                        <NavItem>
                            <Link to={'/Projects/PRO010'} className="nav-link">
                                <i className='fas fa-tasks' /> Proyectos
                            </Link>
                        </NavItem>                        
                        <Navbar.Text>
                            FACTURACIÓN
                        </Navbar.Text>
                        <NavItem>
                            <Link to={'/Adjustments/ADJ010'} className="nav-link" >
                                <i className='fas fa-wrench' /> Ajustes de IPC
                            </Link>
                        </NavItem>

                        <NavItem>
                            <Link to={'/Billing/BIL010'} className="nav-link" >
                                <i className='fas fa-wrench' /> Mantenimientos a facturar
                            </Link>
                        </NavItem>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
