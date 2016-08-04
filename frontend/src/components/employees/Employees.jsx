import React, { Component } from 'react';

import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

import Item from './Item';


export default class Employees extends Component {
    constructor() {
        super();
        this.state = { 
            departmentName: 'Department', 
            employeeList: [],
        };
    }


    componentDidMount() {
        const departmentId = this.props.params.departmentId;

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'departments/' + this.props.params.departmentId,
            async: true,
            success: (data) => {
                this.setState({
                    departmentName: data.name,
                });
            } 
        });

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'departments/' + this.props.params.departmentId + '/employees',
            async: true,
            success: (data) => {
                this.setState({
                    employeeList: data
                });
            } 
        });
    }
    
    render() {
        const items = this.state.employeeList.map( 
            (e) => <Item key={e.id} data={e} />
        );

        return (
            <div>
                <h1>{this.state.departmentName} Employees:</h1>
                <br/>
                <button className="btn btn-md btn-info" > 
                        <span className="glyphicon glyphicon-plus-sign"></span> 
                        &nbsp;Add new employee 
                </button>
                <br/>
                <br/>
                <table className="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Address</th>
                            <th>Employment Date</th>
                            <th>Termination Date</th>
                            <th>Employment Hours</th>
                            <th>Position</th>
                            <th>Total Allocation</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items}
                    </tbody>
                </table>
            </div>
        );
    }
}