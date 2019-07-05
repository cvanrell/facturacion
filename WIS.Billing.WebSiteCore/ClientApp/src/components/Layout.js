import React, { Component } from 'react';
import { Col, Grid, Row, Container } from 'react-bootstrap';
import { NavMenu } from './NavMenu';

//import React, { useState } from 'react';
//import { LayoutHeader } from './LayoutComponents/LayoutHeader';
//import { LayoutMain } from './LayoutComponents/LayoutMain';
//import { LayoutFooter } from './LayoutComponents/LayoutFooter';
//import { LayoutMenu } from './LayoutComponents/LayoutMenu';

export class Layout extends Component {
    displayName = Layout.name

    render() {
        return (
            <Container fluid>
                <Row>
                    <Col sm={3}>
                        <NavMenu />
                    </Col>
                    <Col sm={9}>
                        {this.props.children}
                    </Col>
                </Row>
            </Container>
        );
    }
}


//export function Layout(props) {
//    const [isMenuOpening, setMenuOpening] = useState(false);

//    return (
//        <React.Fragment>
//            <LayoutHeader />
//            <LayoutMenu setMenuOpening={setMenuOpening} />
//            <LayoutMain isMenuOpening={isMenuOpening}>
//                {props.children}
//            </LayoutMain>
//            <LayoutFooter />
//        </React.Fragment>
//    );
//}
