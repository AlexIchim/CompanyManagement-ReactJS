import * as React from 'react';
import {Link} from 'react-router';

const Multilevel = () => {
    return (
        <li className="treeview">
            <Link to="#">
                <i className="fa fa-share"></i> <span>Offices</span>
            <span className="pull-right-container">
              <i className="fa fa-angle-left pull-right"></i>
            </span>
            </Link>
            <ul className="treeview-menu">
                <li>
                    <Link to="#"><i className="fa fa-circle-o"></i> Level One</Link></li>
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



const Sidebar = () => {
    return (
        <aside className="main-sidebar main-sidebar-custom">
            <section className="sidebar sidebar-custom">
                <h1>Firm offices</h1>
                <ul className="sidebar-menu">
                    <li className="treeview">
                        <Link to="#">
                            <i className="fa fa-dashboard"></i> <span>Dashboard</span>
                        </Link>
                    </li>
                    <Multilevel />

                </ul>
            </section>
        </aside>

    )
};

export default Sidebar;