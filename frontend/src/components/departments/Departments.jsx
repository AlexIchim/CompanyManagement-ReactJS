import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import config from '../../api/config';
import DepartmentRow from './DepartmentRow';
import * as Immutable from 'immutable';

export default class Departments extends Component {


        constructor () {
            super();
            this.state = {
                departments: [],
                officeName: 'Office'
            }
        }

        componentWillMount() {
            $.ajax({
                method: 'GET',
                url: config.baseUrl + 'offices/' + this.props.params['officeId'] + '/departments',
                success: function(data) {
                    this.setState({
                        departments: data,
                    })
                }.bind(this)
            })

            $.ajax({
                method: 'GET',
                url: config.baseUrl + 'offices/' + this.props.params['officeId'],
                success: function(data) {
                    this.setState({
                        officeName: data.name
                    })
                }.bind(this)
            })
        }


        render() {
            const items = this.state.departments.map((element, index) => {
                return (
                    <DepartmentRow
                        node = {element}
                        key= {index}
                    />
                )
            });

            return (
                <div>
                    <h1> {this.state.officeName}</h1>
                    <button className="btn btn-md btn-info" > <span className="glyphicon glyphicon-plus-sign"></span> Add new department </button>
                    <table className="table" id="table1">
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
                        {items}
                        </tbody>
                    </table>
                </div>
            );
        }
}
