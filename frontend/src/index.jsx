import * as React from 'react';
import  {Router, Route,hashHistory, IndexRoute} from 'react-router';
import * as ReactDOM from 'react-dom';
import App from './components/App';
import ConcertsList from './components/concertslist/ConcertsList.jsx';
import './assets/less/index.less';
import Dashboard from './components/dashboard/Dashboard.jsx';
import Employees from './components/employee/Employees.jsx'

const Routes = () => {

    return (
        <Router history={hashHistory}>
            <Route path="/" component={App}>
                <IndexRoute component={Dashboard}/>
                <Route path="ConcertsList" component={ConcertsList}>
                </Route>

                <Route path="department/members/:departmentId" component={Employees}>
                </Route>
            </Route>
        </Router>

    )

};


ReactDOM.render(<Routes/>, document.getElementById('root'));