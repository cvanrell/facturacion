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
            <React.Fragment>
                <NavMenu />
                <main>
                    <Container className="mt-2" fluid>
                        {this.props.children}
                    </Container>
                </main>
            </React.Fragment>
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
