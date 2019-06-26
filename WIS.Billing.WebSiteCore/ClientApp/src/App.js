import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Maintenances } from './components/Maintenances';
import { FetchMaintenance } from './components/FetchMaintenance';
import { AddMaintenance } from './components/AddMaintenance';
import { FetchProject } from './components/FetchProject';
import { AddProject } from './components/AddProject';
import { FetchClient } from './components/FetchClient';
import { AddClient } from './components/AddClient';
import { AddHourRate } from './components/AddHourRate';
import { FetchHourRate } from './components/FetchHourRate';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
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
            </Layout>
        );
    }
}
