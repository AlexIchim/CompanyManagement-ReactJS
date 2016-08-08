import React from 'react';
import {Link} from 'react-router';
import Context from '../../context/Context';

const Header = () => {
    return (
        <header className="main-header">
            <a href="/" className="logo">
                <span className="logo-lg"><b>HR</b>Project</span>
            </a>
            <nav className="navbar navbar-static-top">
                <div className="navbar-custom-menu">
                    <ul className="nav navbar-nav">
                        <li>
                            <Link to="/about" className="dropdown-toggle">Team2</Link>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Header;

