import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Loading } from './components/Loading';
import { ScrollContextProvider } from './components/GridComponents/ScrollContextProvider';
import HTML5Backend from 'react-dnd-html5-backend';
import { DragDropContext } from 'react-dnd';
//import { Home } from './components/Home';
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

const Home = React.lazy(() => import('./components/Home'));

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
                    </Suspense>
                </ScrollContextProvider>
            </Layout>
        );
    }
}
export default DragDropContext(HTML5Backend)(App); //Si se necesita touch, hay que usar el backend de touch