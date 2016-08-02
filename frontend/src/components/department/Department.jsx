import React, { Component } from 'react';
import {Link} from 'react-router';

export default class Department extends React.Component {

    constructor() {
        super();
    }

    
    
    render() {

        return (

            <div>
                <h1>X Department </h1>
                <button className="btn btn-xs btn-info" > <span className="glyphicon glyphicon-plus-sign"></span> Add new department </button>
                <table className="table table-condensed" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Department</th>
                        <th className="col-md-2">Department manager</th>
                        <th className="col-md-2">Employees</th>
                        <th className="col-md-2">Projects</th>
                        <th className="col-md-2">Actions</th>
                    </tr>
                    </thead>
                    <tbody>

                  

                    </tbody>
                </table>
            </div>
        )
    }


}