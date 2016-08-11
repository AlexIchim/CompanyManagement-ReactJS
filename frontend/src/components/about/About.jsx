import React, { Component } from 'react';

export default class About extends Component {
    render() {
        return (
            <div>
                <h1>HR Project - Team2</h1>
                <div>
                    <div className="col-md-6">
                        <h3>Created By</h3>
                        <ul>
                            <li>Camelia Trif</li>
                            <li>Alexandru Ichim</li>
                            <li>Robert Varadi</li>
                            <li>Arnold Beiland</li>
                        </ul>
                        <br/>
                        <span className="text-danger">Cei patru fanatici...</span>
                    </div>
                    <div className="col-md-6 pull-down">
                        <img src="assets/images/logo-evozon.png"/>
                    </div>
                </div>
            </div>
        );
    }
}