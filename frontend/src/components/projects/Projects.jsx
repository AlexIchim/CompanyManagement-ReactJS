import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import config from '../api/config.js';

const ProjectRow = (props) => {
   
    const linkMembers = 'offices/' + props.officeId + '/departments/' + props.node.departmentId + '/employees';

    return (
        <tr>
            <td>{props.node.Name}</td>
            <td>{props.node.MemberCount}</td>
            <td>{props.node.Duration}</td>
            <td>{props.node.Status}</td>
            <td>
                <Link to = "#">edit     </Link>
                <Link to = {linkMembers}>view members     </Link>
                <Link to = "#">remove</Link>
            </td>
        </tr>
    )
}

export default class Projects extends Component{
    constructor () {
            super();
            this.state = {
                projects: [],
                officeId : null,
                departmentName : ''
            }
        }

    componentWillMount(){
        console.log(this.props);
        $.ajax({
                method: 'GET',
                url: config.baseUrl + 'department/getProjectsByDepartmentId?id=' + this.props.params['departmentId'],
                success: function(data) {
                    console.log(data, this);
                    this.setState({
                        projects : data
                    })
                }.bind(this)
            });
         $.ajax({
                method: 'GET',
                url: config.baseUrl + 'office/getById?id=' + this.props.params['officeId'],
                success: function(data) {
                    console.log(data, this);
                    this.setState({
                        officeId : data
                    })
                }.bind(this)
            });
            console.log(this.state.departmentName, 'in cwm');
            $.ajax({
                method: 'GET',
                url: config.baseUrl + 'department/getById?id=' + this.props.params['departmentId'],
                success: function(data) {
                    console.log(data, this);
                    this.setState({
                        departmentName : data.Name
                    })
                }.bind(this)
            });
            console.log(this.state.departmentName, 'in cwm');
    }

    render() {
        const items = this.state.projects.map((element, index) => {
            return (
                <ProjectRow
                    node = {element}
                    key = {index}
                />
            )
        });

        console.log(this.state.departmentName.value);

        return (
            <div>
                <h1>{this.state.departmentName} Department Projects</h1>

                <button className = "btn btn-md btn-info">
                    <span className="glyphicon glyphicon-plus-sign"></span>
                     Add new project
                </button>

                <table className = "table table-condensed" id = "table">
                    <thead>
                        <tr>
                            <th className = "col-md-2">Project Name</th>
                            <th className = "col-md-2">Team Members</th>
                            <th className = "col-md-2">Duration</th>
                            <th className = "col-md-2">Status</th>
                            <th className = "col-md-2">Actions</th>
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