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
                        <li className="dropdown user user-menu">
                            <a href="#" className="dropdown-toggle" data-toggle="dropdown">
                                    <span className="hidden-xs">
                                        { Context.cursor.get('user').get('name') }
                                    </span>
                            </a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Header;