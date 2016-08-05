import * as React from 'react';
import * as ReactDOM from 'react-dom';
import  {Router, Route, IndexRoute, hashHistory} from 'react-router';

import './assets/less/index.less';

import App from './components/App';
import About from './components/about/About';
import Departments from './components/departments/Departments';
import DumbParent from './components/layout/DumbParent';
import Employees from './components/employees/Employees';
import Offices from './components/offices/Offices';
import ProjectMembers from './components/project-members/ProjectMembers';
import Projects from './components/projects/Projects';

import TestPag from './components/testpag/Testpag';

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
            <Route path="testpag" component={TestPag} />
        </Route>
    </Router>
);

ReactDOM.render(<Routes/>, document.getElementById('root'));