import React, { Component } from 'react';
import Header from './layout/Header.jsx';
import Sidebar from './layout/Sidebar.jsx';
import Context from '../context/Context';


class App extends Component {
    render() {
        const margin = {
            marginLeft: "250px"
        };
        return (
            <div  className="wrapper">
                <Header/>
                <Sidebar/>
                <section className="content" style={margin}>
                    {this.props.children}
                </section>
            </div>
        );
    }
}

export default App;
