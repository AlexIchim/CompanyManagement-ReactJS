import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import Form from './Form';
import * as Controller from '../controller';
import EmployeeItem from './EmployeeItem.jsx'

export default class Employee extends React.Component{


    constructor(){
        super();
        this.state ={
            add: false,
            employees: Context.cursor.get("employees"),
            pageNr: 1
        }
    }

    componentWillMount(){
       this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

     componentDidMount(){
         Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,this.state.pageNr);
    }

    componentWillUnmount () {
        this.subscription.dispose(); 
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

    back(){

        if (this.state.pageNr!=1){
            const whereTo=this.state.pageNr-1

            Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,whereTo)
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,whereTo)

        this.setState({
            pageNr:this.state.pageNr+1
        })
    }

render(){
    
    const items = this.state.employees.map( (element, index) => {
        return(
            <EmployeeItem
                node = {element}
                key = {index}
                departmentId = {this.props.routeParams.departmentId}

            />
        )

    });
   const addModal = this.state.add ? <Form departmentId={this.props.routeParams.departmentId} show={this.state.add} close ={this.closeAddForm.bind(this)} /> : ""
   
    return(
        <div>
            {addModal}
            
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
            <div className="btn-wrapper">
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-arrow-left fa-1x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-arrow-right fa-1x" aria-hidden="true"></i>
                    </button>              
                </div>
        </div>
    )
}
    
    
    
}