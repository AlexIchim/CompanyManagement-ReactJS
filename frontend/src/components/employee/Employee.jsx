import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'

const Tr = (props) => {
    return(
        <tr>
            <td>{props.node.Name} </td>
            <td>{props.node.Address}</td>
            <td>{props.node.EmploymentDate}</td>
            <td>{props.node.ReleaseDate}</td>
            <td>{props.node.JobType}</td>
            <td>{props.node.PositionType}</td>
            <td>{props.node.TotalAllocation}</td>
            <td><a href=""> View Details | </a>
                <a href="#"> Release | </a>
                <a href="#"> Edit </a></td>
        </tr>
        )


}

export default class Employee extends React.Component{


    constructor(){
        super();
        this.state ={
            emp: []
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/employee/getAllDepartmentEmployees?inputInfo=' + this.props.routeParams.departmentId,
            success: function(data){
                this.setState({
                    emp: data
                })

            }.bind(this)
        })
    }

render(){

    const items = this.state.emp.map( (element, index) => {
        return(
            <Tr
                node = {element}
                key = {index}

            />
        )

    });

    return(
        <div>
            <h1>{this.props.routeParams.departmentName + ' Employees'}  </h1>
            <button className="btn btn-xs btn-info" > <span className="glyphicon glyphicon-plus-sign"></span> Add new employee </button>
            <table className="table table-condensed" id="table1">
                <thead>
                <tr>
                    <th className="col-md-2">Name</th>
                    <th className="col-md-2">Address</th>
                    <th className="col-md-2">Employment Date</th>
                    <th className="col-md-2">Termination Date</th>
                    <th className="col-md-2">Job Type</th>
                    <th className="col-md-2">Position</th>
                    <th className="col-md-2">Allocation</th>
                    <th className="col-md-2">Actions</th>
                </tr>
                </thead>
                <tbody>
                    {items}
                </tbody>
            </table>
        </div>
    )
}
    
    
    
}