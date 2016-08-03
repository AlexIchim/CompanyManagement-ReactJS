import * as React from 'react';
import {Link} from 'react-router';

const Multilevel = () => {
    return (
        <li className="treeview active">
            <a href="#">
                <i className="fa fa-share"></i> <span>Multilevel</span>
            <span className="pull-right-container">
              <i className="fa fa-angle-left pull-right"></i>
            </span>
            </a>
            <ul className="treeview-menu">
                <li><a href="#"><i className="fa fa-circle-o"></i> Level One</a></li>
                <li>
                    <a href="#"><i className="fa fa-circle-o"></i> Level One
                <span className="pull-right-container">
                  <i className="fa fa-angle-left pull-right"></i>
                </span>
                    </a>
                    <ul className="treeview-menu">
                        <li><a href="#"><i className="fa fa-circle-o"></i> Level Two</a></li>
                        <li>
                            <a href="#"><i className="fa fa-circle-o"></i> Level Two
                    <span className="pull-right-container">
                      <i className="fa fa-angle-left pull-right"></i>
                    </span>
                            </a>
                            <ul className="treeview-menu">
                                <li><a href="#"><i className="fa fa-circle-o"></i> Level Three</a></li>
                                <li><a href="#"><i className="fa fa-circle-o"></i> Level Three</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li><a href="#"><i className="fa fa-circle-o"></i> Level One</a></li>
            </ul>
        </li>
    )
}


class Sidebar extends React.Component{
    constructor(){
        super();
    }

    render(){
        return (

            <aside className="main-sidebar">
                <section className="sidebar">
                    <ul className="sidebar-menu">
                        <li className="header">MAIN NAVIGATION</li>
                        <li className="treeview">
                            <Link to="#">
                                <i className="fa fa-dashboard"></i> <span>Dashboard</span>
                            </Link>
                        </li>
                        <Multilevel/>
                    </ul>
                </section>
            </aside>
        )
    }
}




export default Sidebar;