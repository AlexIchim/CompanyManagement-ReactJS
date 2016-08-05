import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Form from './FormProject.jsx'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as Controller from '../controller';
import ProjectItem from './ProjectItem.jsx'

export default class Project extends React.Component {

    constructor() {
        super();
        this.state = {
            add: false,
            projects: Context.cursor.get("projects"),
            pageNr:1
        }
    }

    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

    componentDidMount(){
         Controller.getAllDepProjects(this.props.routeParams.departmentId,this.state.pageNr);
    }

    componentWillUnmount () {
        this.subscription.dispose(); 
    }

     onContextChange(cursor){
        this.setState({
            projects: cursor.get("projects")         
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

            Controller.getAllDepProjects(this.props.routeParams.departmentId,whereTo);
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        Controller.getAllDepProjects(this.props.routeParams.departmentId,whereTo);

        this.setState({
            pageNr:this.state.pageNr+1
        })
    }


    render() {

        const items = this.state.projects.map((el, x) => {
            return (
                <ProjectItem
                    node = {el}
                    key= {x}
                    departmentId={this.props.routeParams.departmentId}   
                />
            )
        });
        const addModal = this.state.add ? <Form departmentId={this.props.routeParams.departmentId} show = {this.state.add} close={this.closeAddForm.bind(this)} /> : '';

        return (
            <div>
                {addModal}

                <button className="btn btn-xs btn-info" onClick={this.showAddForm.bind(this)} > <span className="glyphicon glyphicon-plus-sign"></span> Add new project </button>
                <table className="table table-condensed" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Name</th>
                        <th className="col-md-2">Team members</th>
                        <th className="col-md-2">Status</th>
                        <th className="col-md-2">Duration</th>
                        <th className="col-md-2">Actions</th>
                    </tr>
                    </thead>
                    <tbody>
                    {items}
                </tbody>
                </table>
                <div>
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-arrow-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-arrow-right fa-2x" aria-hidden="true"></i>
                    </button>              
                </div>
            </div>
        )
    }
}