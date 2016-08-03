import * as React from 'react';
import  {Router, Route,hashHistory, IndexRoute} from 'react-router';
import * as ReactDOM from 'react-dom';

import './assets/less/index.less';

import App from './components/App';
import Offices from './components/offices/Offices';
import Departments from './components/departments/Departments';
import DumbParent from './components/layout/DumbParent';
import Projects from './components/projects/Projects';
import ProjectMembers from './components/project-members/ProjectMembers';
import Employees from './components/employees/Employees';
import About from './components/about/About';


const Routes = () => (
    <Router history={hashHistory}>
        <Route path="/" component={App}>
            <IndexRoute component={Offices}/>
            <Route path="offices" component={DumbParent}>
                <IndexRoute component={Offices}/>
                <Route path=":officeId/departments" component={DumbParent} >
                    <IndexRoute component={Departments}/>
                    <Route path=":departmentId/projects" component={DumbParent}>
                        <IndexRoute component={Projects}/>
                        <Route path=":projectId/members" component={ProjectMembers} />                                            
                    </Route>
                    <Route path=":departmentId/employees" component={Employees} />
                </Route>
            </Route>
            <Route path="about" component={About} />
        </Route>
    </Router>
);

ReactDOM.render(<Routes/>, document.getElementById('root'));