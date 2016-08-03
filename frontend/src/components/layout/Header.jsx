import React, { Component } from 'react';
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
                            <a href="/#/about" className="dropdown-toggle" data-toggle="dropdown">Team2</a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Header;