import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import config from '../api/config.js';
import ModalTemplate from '../layout/ModalTemplate';
import EditDetails from './EditDetails';
import AddProject from './AddProject';
import Item from './Item';

export default class Projects extends Component{
    constructor () {
        super();
        this.state = {
            projects: [],
            officeId : null,
            departmentName : '',
            showModalTemplate : null,
            modalProject : {}
        }
    }

    componentWillMount(){
        const departmentId = this.props.params.departmentId;

        $.ajax({
            method : 'GET',
            url : config.baseUrl + 'departments/' + this.props.params['departmentId'],
            async : true,
            success : (data) => {
                this.setState({
                    departmentName : data.name,
                });
            }
        });

        $.ajax({
            method : 'GET',
            url : config.baseUrl + 'departments/' + this.props.params['departmentId'] + '/projects',
            async : true,
            success : (data) => {
                this.setState({
                    projects : data
                });
            }
        });
    }

    addProject(project){
        this.setState({
            showModalTemplate : 'add',
            modalProject : project
        })
    }

    editProject(project){
        this.setState({
            showModalTemplate : 'edit',
            modalProject : project
        })
    }

    hideModal(){
        this.setState({
            showModalTemplate : null,
            modalProject : {}
        })
    }

    componentWillMount(){
        this.callApi();
    }
    
    componentWillReceiveProps(newProps){
        this.props = newProps;
        this.callApi();
    }

    callApi(){
        $.ajax({
            method: 'GET',
            url: config.baseUrl + 'departments/' + this.props.params['departmentId'] + '/projects',
            success: function(data) {
                this.setState({
                    projects : data
                })
            }.bind(this)
        });

        $.ajax({
            method: 'GET',
            url: config.baseUrl + 'offices/' + this.props.params['officeId'],
            success: function(data) {
                this.setState({
                    officeId : data.id
                })
            }.bind(this)
        });

        $.ajax({
            method: 'GET',
            url: config.baseUrl + 'departments/' + this.props.params['departmentId'],
            success: function(data) {
                
                this.setState({
                    departmentName : data.name
                })
            }.bind(this)
        });
    }

 

    render() {
    
        const items = this.state.projects.map((element, index) => 
           
                <Item
                    node = {element}
                    key = {index}
                    officeId = {this.state.officeId}
                />
            
        );

        const modalTemplate = this.state.showModalTemplate !== null ? (
            <ModalTemplate
                getComponent = {
                    (hideFunc) => {
                        switch(this.state.showModalTemplate) {
                            case 'edit' : 
                                return <EditDetails project = {this.state.modalProject} hideFunc = {hideFunc}/>
                            case 'add' :
                                return <AddProject project = {this.state.modalProject} hideFunc = {hideFunc}/>;
                        }
                    }
                }
                onHide = {this.hideModal.bind(this)}
            />
        ) : null;

        return (
            <div>
                <h1><b>{this.state.departmentName}</b> Department Projects</h1>
                {modalTemplate}
                <button className = "btn btn-md btn-info" onClick = {this.addProject.bind(this)}>
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