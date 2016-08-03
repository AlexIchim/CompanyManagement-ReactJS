import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import config from '../api/config.js';

const DepartmentRow = (props) => {
    const linkEmployees = "offices/" + props.node.OfficeId +  "/departments/" + props.node.Id  + "/employees";
    const linkProjects = "offices/" + props.node.OfficeId + "/departments/" + props.node.Id  + "/projects";
    return(

        <tr>
            <td>{props.node.Name}</td>
            <td>{props.node.DepartmentManagerName}</td>
            <td>{props.node.EmployeeCount}</td>
            <td>{props.node.ProjectCount}</td>
            <td><Link to={linkEmployees}>View employees  </Link>
                <Link to={linkProjects}>View projects    </Link>
                <Link to="#"> Edit </Link></td>
        </tr>
    )
}

export default class Departments extends Component {


        constructor () {
            super();
            this.state = {
                departments: []
            }
        }

        componentWillMount() {
            console.log(this.props.params['officeId']);
            $.ajax({
                method: 'GET',
                url: config.baseUrl + 'office/getDepartmentsByOfficeId?id=' + this.props.params['officeId'],
                success: function(data) {
                    console.log(data, this);
                    this.setState({
                        departments: data
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

                        {items}


                        </tbody>
                    </table>
                </div>
            );
        }
}