import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Form from './Form.jsx'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';




const Tr = (props) => {
   const linkEmployees = "department/" + props.node.get('Id')  + '/' + props.node.get('Name') + "/employees";
   const linkProjects = "department/" + props.node.get('Id')  + '/' + props.node.get('Name') + "/projects";
    return(

            <tr>
            <td>{props.node.get('Name')}</td>
            <td>{props.node.get('DepartmentManager')}</td>
            <td>{props.node.get('NbrOfEmployees')}</td>
            <td>{props.node.get('NbrOfProjects')}</td>
            <td><Link to={linkEmployees}> View employees | </Link>
                <Link to={linkProjects}> View projects | </Link>
                <Link to="#"> Edit </Link></td>
        </tr>
    )
}

export default class Department extends React.Component {

    constructor() {
        super();
        this.state = {
            add: false,
            departments: Context.cursor.get("departments")
        }
    }


    
    componentWillMount(){
        
        Context.subscribe(this.onContextChange.bind(this));

        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/office/getAllDepOffice?officeId=' + this.props.routeParams.officeId+'&pageSize=10&pageNr=1',
            success: function (data) {
                Context.cursor.set("departments",Immutable.fromJS(data));
            }.bind(this)
        })
    }

     onContextChange(cursor){
        this.setState({
            departments: Context.cursor.get("departments")         
        });

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


    render() {
        
        const items = this.state.departments.map((el, x) => {
            return (
                <Tr
                node = {el}
                key= {x}

                /> 
            )
        });

        const addModal = this.state.add ? <Form officeId={this.props.routeParams.officeId} show = {this.state.add} close={this.closeAddForm.bind(this)} /> : '';

        return (
            <div>
                {addModal}

                <button className="btn btn-xs btn-info" onClick={this.showAddForm.bind(this)} > <span className="glyphicon glyphicon-plus-sign"></span> Add new department </button>
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
        )
    }


}