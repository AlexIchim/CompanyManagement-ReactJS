import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import Form from './Form.jsx'

const Tr = (props) => {
    return(
        <tr>
            <td>{props.node.get('Name')} </td>
            <td>{props.node.get('Address')}</td>
            <td>{props.node.get('EmploymentDate')}</td>
            <td>{props.node.get('ReleaseDate')}</td>
            <td>{props.node.get('JobType')}</td>
            <td>{props.node.get('PositionType')}</td>
            <td>{props.node.get('TotalAllocation')}</td>
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
            add: false,
            employees: Context.cursor.get("employees")
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/employee/getAllDepartmentEmployees?departmentId=' + this.props.routeParams.departmentId+'&pageSize=10&pageNr=1',
            success: function(data){
                    Context.cursor.set("employees", Immutable.fromJS(data))
            }.bind(this)
        })
    }

    onContextChange(cursor){
        this.setState({
            employees: Context.cursor.get('employees')
        })
    }

    showAddForm(){
        this.setState({
            add:true
        });
    }

    closeAddForm(){
        this.setState({
            add: !this.state.add
        })
    }

render(){
    const items = this.state.employees.map( (element, index) => {
        return(
            <Tr
                node = {element}
                key = {index}

            />
        )

    });
   const addModal = this.state.add ? <Form departmentId={this.props.routeParams} show={this.state.add} close ={this.closeAddForm.bind(this)} /> : ""
   console.log(addModal)
    return(
        <div>
            {addModal}
            <h1>{this.props.routeParams.departmentName + ' Employees'}  </h1>
            <button className="btn btn-xs btn-info" onClick={this.showAddForm.bind(this)}> <span className="glyphicon glyphicon-plus-sign"></span> Add new employee </button>
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