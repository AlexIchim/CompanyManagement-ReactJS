import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Form from './Form.jsx'
import EditForm from './EditForm.jsx'
import DepartmentItem from './DepartmentItem.jsx'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as Controller from '../controller';


export default class Department extends React.Component {

    constructor() {
        super();
        this.state = {
            add: false,
            departments: Context.cursor.get("departments"),
            pageNr:1
        }
    }

    
    componentWillMount(){ 
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

    componentDidMount(){
         Controller.getAllDepOffice(this.props.routeParams.officeId,this.state.pageNr);
    }

    componentWillUnmount () {
        this.subscription.dispose(); 
    }

     onContextChange(cursor){
        this.setState({
            departments: cursor.get("departments")         
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

    back(){

        if (this.state.pageNr!=1){
            const whereTo=this.state.pageNr-1
            console.log(2)
            Controller.getAllDepOffice(this.props.routeParams.officeId,whereTo);
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1
        cosole.log(3)

        Controller.getAllDepOffice(this.props.routeParams.officeId,whereTo);

        this.setState({
            pageNr:this.state.pageNr+1
        })
    }

    render() {
        const items = this.state.departments.map((el, x) => {
            return (
                <DepartmentItem
                node = {el}
                key= {x}
                officeId={this.props.routeParams.officeId}      
                /> 
            )
        });

        const addModal = this.state.add ? <Form officeId={this.props.routeParams.officeId} show = {this.state.add} close={this.closeAddForm.bind(this)} /> : '';
    
        return (
            <div>
                {addModal}

                <button className="btn btn-xs btn-info" onClick={this.showAddForm.bind(this)} > <span className="glyphicon glyphicon-plus-sign"></span> Add new department </button>
                <table className="table table-striped" id="table1">
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