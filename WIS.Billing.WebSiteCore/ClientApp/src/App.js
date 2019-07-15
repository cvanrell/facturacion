import React, { Component, Suspense } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Loading } from './components/Loading';
import { ScrollContextProvider } from './components/GridComponents/ScrollContextProvider';
import HTML5Backend from 'react-dnd-html5-backend';
import { DragDropContext } from 'react-dnd';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import { Maintenances } from './components/Maintenances';

import { FetchMaintenance } from './pages/Maintenances/FetchMaintenance';
import { AddMaintenance } from './pages/Maintenances/AddMaintenance';
import { FetchProject } from './pages/Projects/FetchProject';
import { AddProject } from './pages/Projects/AddProject';

import { FetchClient } from './pages/Clients/FetchClient';
import { AddClient } from './pages/Clients/AddClient';
import { AddHourRate } from './pages/HourRates/AddHourRate';
import { FetchHourRate } from './pages/HourRates/FetchHourRate';
import { CLIENTS } from './pages/Clients/CLIENTS';

//const Clients = React.lazy(() => import('./pages/Clients/Clients'));

//const Home = React.lazy(() => import('./components/Home'));

export default class App extends Component {
    displayName = App.name


    constructor(props) {
        super(props);

        this.state = {
            isMenuOpening: false
        };
    }

    setMenuOpening(value) {
        this.setState({
            isMenuOpening: value
        });
    }

    render() {
        return (
            <Layout setMenuOpening={this.setMenuOpening}>
                <ScrollContextProvider>
                    <Suspense fallback={<Loading />}>
                        <Route exact path='/' component={Home} />
                        <Route path='/counter' component={Counter} />
                        <Route path='/fetchdata' component={FetchData} />
                        <Route path='/fetchmaintenance' component={FetchMaintenance} />
                        <Route path='/addMaintenance' component={AddMaintenance} />
                        <Route path='/maintenance/edit/:maintenanceId' component={AddMaintenance} />
                        <Route path='/fetchproject' component={FetchProject} />
                        <Route path='/addproject' component={AddProject} />
                        <Route path='/project/edit/:projectId' component={AddProject} />
                        <Route path='/fetchclient' component={FetchClient} />
                        <Route path='/addclient' component={AddClient} />
                        <Route path='/client/edit/:clientId' component={AddClient} />
                        <Route path='/addHourRate/' component={AddHourRate} />
                        <Route path='/fetchHourRate' component={FetchHourRate} />

                        <Route path='/Clients/CLIENTS' component={CLIENTS} />

                    </Suspense>
                </ScrollContextProvider>
            </Layout>
        );
    }
}
//export default DragDropContext(HTML5Backend)(App); //Si se necesita touch, hay que usar el backend de touch