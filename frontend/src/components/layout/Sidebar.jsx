import * as React from 'react';
import {Link} from 'react-router';

const Sidebar = () => {
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
                </ul>
            </section>
        </aside>

    )
};

export default Sidebar;