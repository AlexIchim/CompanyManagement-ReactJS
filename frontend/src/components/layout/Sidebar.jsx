import * as React from 'react';
import {Link} from 'react-router';

const Sidebar = () => {
    return (
        <aside className="main-sidebar">
            <section className="sidebar sidebar-custom">
                <h1>Firm offices</h1>
                <ul className="sidebar-menu">
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