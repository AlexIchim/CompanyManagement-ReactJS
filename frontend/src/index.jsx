import * as React from 'react';
import  {Router, Route,hashHistory, IndexRoute} from 'react-router';
import * as ReactDOM from 'react-dom';
import App from './components/App';
import Department from './components/department/Department';
import Employee from './components/employee/Employee';
import Project from './components/project/Project';
import Member from './components/project/Member';
import './assets/less/index.less';
import Dashboard from './components/dashboard/Dashboard.jsx';

const Routes = () => {
    return (
        <Router history={hashHistory}>
            <Route path="/" component={App}>
                <IndexRoute component={Dashboard}/>
                <Route path="office/:officeId/:officeName/departments" component={Department}/>
                <Route path="department/:departmentId/:departmentName/employees" component={Employee}/>
                <Route path="department/:departmentId/:departmentName/projects" component={Project}/>
                <Route path="project/:projectId/:projectName/members" component={Member}/>
            </Route>
        </Router>
    )

};


ReactDOM.render(<Routes/>, document.getElementById('root'));