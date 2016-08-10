import * as React from 'react';
import {Link} from 'react-router';

import * as Access from '../../context/accessors';
import * as Commands from '../../context/commands';
import * as Controller from '../../api/controller';
import Context from '../../context/Context'

class Multilevel extends React.Component {
    constructor() {
        super();

        this.state = {
            offices: [],
            currentOfficeId: null,
            currentDepartments: [],
            currentDepartmentId: null
        };

    }

    componentWillMount(){
        Commands.fetchOffices();

        Context.subscribe((newCursor) => {
            this.setState({
                offices: 
                    Access.offices(Context.cursor).map(
                        (el) => { 
                            return {
                                id: el.get('id'), 
                                name: el.get('name')
                            };
                        }
                    ).toJS(),
                currentOfficeId: Access.currentOfficeId(Context.cursor),
                currentDepartments: 
                    Access.currentDepartments(Context.cursor).map(
                        (el) => {
                            return {
                                id: el.get('id'),
                                name: el.get('name')
                            }
                        } 
                    ).toJS(),
                currendDepartmentId: Access.currentDepartmentId(Context.cursor)
            });
        });
    }

    render() {
        const offices = this.state.offices.map((o) => (
            <li className="active" key={o.id}>
                <Link activeClassName="active" to={'/offices/' + o.id + '/departments'}>
                    <i className="fa fa-building-o"/> {o.name}
                </Link>
                { this.state.currentOfficeId == o.id ? 
                    <ul className="unstyled" >{
                        this.state.currentDepartments.map(d => 
                            <li key={d.id}><Link activeClassName="active" to={
                                '/offices/' + o.id + '/departments/' + d.id
                            }>{d.name} </Link>
                            { this.state.currendDepartmentId == d.id ? 
                                <ul className="unstyled" >
                                    <li><Link activeClassName="active" to={
                                        '/offices/' + o.id + '/departments/' + d.id + '/projects'
                                    }><i className=" fa fa-folder"/> Projects</Link></li>
                                    <li><Link activeClassName="active"  to={
                                        '/offices/' + o.id + '/departments/' + d.id + '/employees'
                                    }><i className="fa fa-users"/> Employees</Link></li>
                                </ul>
                            : null }
                            </li>
                        )
                    }</ul>
                : null }
            </li>
        ));


        return (
            <li className="treeview active">
                <Link to="/">
                    <i className="fa fa-share"></i> <span>Firm Offices</span>
                </Link>

                <ul className="treeview-menu">
                    {offices}
                </ul>
            </li>
        );
    }
}


const Sidebar = () => (
    <aside className="main-sidebar">
        <section className="sidebar">
            <ul className="sidebar-menu">
                <li className="header">MAIN NAVIGATION</li>
                <Multilevel/>
            </ul>
        </section>
    </aside>
);

export default Sidebar;