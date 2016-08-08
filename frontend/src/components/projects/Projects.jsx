import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import config from '../api/config.js';
import ModalTemplate from '../layout/ModalTemplate';
import EditDetails from './EditDetails';
import AddProject from './AddProject';
import Item from './Item';
import addProject from '../api/controller/addProject';
import getAllProjects from '../api/controller/getAllProjects';
import deleteProject from '../api/controller/deleteProject';

export default class Projects extends Component{
    constructor () {
        super();
        this.state = {
            projects: [],
            officeId : null,
            departmentName : '',
            departmentId : null,
            showModalTemplate : null,
            modalProject : {}
        }
    }

    componentWillMount(){
        const departmentId = this.props.params.departmentId;
        callApi();
    }

    deleteProject(id){
       deleteProject(
           id,
           true,
           (data) => {
               this.fetchData();
           }
       );
    }

    fetchData(){
        this.callApi();
    }

    addProject(){
        this.setState({
            showModalTemplate : 'add'
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
        getAllProjects(
            this.props.params.departmentId,
            true,
            (data) => {
                this.setState({
                    projects : data
                });
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
                node={element}
                key={index}
                officeId={this.state.officeId}
                onDelete={this.deleteProject.bind(this, element.id)}
                onEdit={this.editProject.bind(this, element)}
            /> 
        );

        const modalTemplate = this.state.showModalTemplate !== null ? (
            <ModalTemplate
                getComponent={
                    (hideFunc) => {
                        switch(this.state.showModalTemplate) {
                            case 'edit' : 
                                return <EditDetails 
                                            project={this.state.modalProject}  
                                            hideFunc={hideFunc} 
                                            updateFunc={function(){
                                                hideFunc();
                                                this.fetchData();
                                            }.bind(this)}
                                            departmentId={this.props.params['departmentId']}
                                            projectId={this.props.params['projectId']}
                                         />;
                            case 'add' :
                                return <AddProject 
                                            hideFunc={hideFunc}
                                            saveFunc={function(){
                                                hideFunc();
                                                this.fetchData();
                                            }.bind(this)}
                                            departmentId={this.props.params['departmentId']}
                                        />;
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;

        return (
            <div>
                <h1><b>{this.state.departmentName}</b> Department Projects</h1>
                {modalTemplate}
                <button className="btn btn-md btn-info" onClick={this.addProject.bind(this)}>
                    <span className="glyphicon glyphicon-plus-sign"></span>
                     &nbsp;Add new project
                </button>

                <table className="table table-condensed" id="table">
                    <thead>
                        <tr>
                            <th className="col-md-2">Project Name</th>
                            <th className="col-md-2">Team Members</th>
                            <th className="col-md-2">Duration</th>
                            <th className="col-md-2">Status</th>
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