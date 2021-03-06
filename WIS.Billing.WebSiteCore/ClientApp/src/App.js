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



//import { FetchMaintenance } from './pages/Maintenances/FetchMaintenance';
//import { AddMaintenance } from './pages/Maintenances/AddMaintenance';
import { FetchProject } from './pages/Projects/FetchProject';
import { AddProject } from './pages/Projects/AddProject';

import { FetchClient } from './pages/Clients/FetchClient';
import { AddClient } from './pages/Clients/AddClient';
import { AddHourRate } from './pages/HourRates/AddHourRate';
import { FetchHourRate } from './pages/HourRates/FetchHourRate';
import { CLI010 } from './pages/Clients/CLI010';
import { CLI020 } from './pages/Clients/CLI020';
import { CLI030 } from './pages/Clients/CLI030';
import { CLI040 } from './pages/Clients/CLI040';
import { CLI050 } from './pages/Clients/CLI050';
import { FEE010 } from './pages/Fees/FEE010';
//import { PRO010 } from './pages/Projects/PRO010';


const PRO010 = React.lazy(() => import('./pages/Projects/PRO010'));
const ADJ010 = React.lazy(() => import('./pages/Adjustments/ADJ010'));
const MAN010 = React.lazy(() => import('./pages/Maintenance/MAN010'));
const BIL010 = React.lazy(() => import('./pages/Billing/BIL010'));
const BIL020 = React.lazy(() => import('./pages/Billing/BIL020'));

//const Clients = React.lazy(() => import('./pages/Clients/Clients'));


export class App extends Component {
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
                        
                        
                        <Route path='/fetchproject' component={FetchProject} />
                        <Route path='/addproject' component={AddProject} />
                        <Route path='/project/edit/:projectId' component={AddProject} />
                        <Route path='/fetchclient' component={FetchClient} />
                        <Route path='/addclient' component={AddClient} />
                        <Route path='/client/edit/:clientId' component={AddClient} />
                        <Route path='/addHourRate/' component={AddHourRate} />
                        <Route path='/fetchHourRate' component={FetchHourRate} />

                        <Route path='/Clients/CLI010' component={CLI010} />
                        <Route path='/Clients/CLI020' component={CLI020} />
                        <Route path='/Clients/CLI030' component={CLI030} />
                        <Route path='/Clients/CLI040' component={CLI040} />
                        <Route path='/Clients/CLI050' component={CLI050} />
                        <Route path='/Fees/FEE010' component={FEE010} />

                        <Route path='/Projects/PRO010' render={(props) => <PRO010 {...props} />} />
                        <Route path='/Adjustments/ADJ010' render={(props) => <ADJ010 {...props} />} />
                        <Route path='/Maintenance/MAN010' render={(props) => <MAN010 {...props} />} />
                        <Route path='/Billing/BIL010' render={(props) => <BIL010 {...props} />} />
                        <Route path='/Billing/BIL020' render={(props) => <BIL020 {...props} />} />


                    </Suspense>
                </ScrollContextProvider>
            </Layout>
        );
    }
}
export default DragDropContext(HTML5Backend)(App); //Si se necesita touch, hay que usar el backend de touch