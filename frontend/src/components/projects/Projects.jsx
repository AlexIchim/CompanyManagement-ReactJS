import React from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import config from '../../api/config.js';

import * as Command from '../../context/commands';
import ModalTemplate from '../layout/ModalTemplate';
import EditDetails from './EditDetails';
import AddProject from './AddProject';
import Item from './Item';
import addProject from '../../api/controller/addProject';

import PaginatedTable from '../layout/PaginatedTable';

import * as Controller from '../../api/controller';

export default class Projects extends React.Component{
    constructor () {
        super();
        this.state = {
            projects: [],
            officeId : null,
            departmentName : '',
            showModalTemplate : null,
            modalProject : {},

            projectCount: 0,
            pageSize: 5,
            pageNumber: 1
        }
    }

    componentWillMount(){
        const departmentId = this.props.params.departmentId;
        callApi();
    }

    deleteProject(id){
       Controller.deleteProject(
           id,
           true,
           (data) => {
               this.callApi();
           }
       );
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
        Command.setCurrentOffice(this.props.params.officeId);
        Command.setCurrentDepartment(this.props.params.departmentId);

        Controller.getProjectsByDepartmentId(
            this.props.params.departmentId,
            this.state.pageSize,
            this.state.pageNumber,
            true,
            (data) => {
                this.setState({
                    projects : data
                });
        });

        Controller.getProjectCountByDepartmentId(
            this.props.params.departmentId,
            true,
            (data) => {
                this.setState({
                    projectCount : data
                });
        });

        Controller.getDepartmentById(
            this.props.params.departmentId,
            true,
            (data) => {
                this.setState({
                    departmentName: data.name
                });
            }
        );
    }

    
    paginationChangeHandler(pageSize, pageNumber){
        Controller.getProjectsByDepartmentId(
            this.props.params.departmentId,
            pageSize,
            pageNumber,
            true,
            (data) => {
                this.setState({
                    projects: data,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                });
            }
        );
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
                                                this.callApi();
                                            }.bind(this)}
                                            departmentId={this.props.params['departmentId']}
                                            projectId={this.props.params['projectId']}
                                         />;
                            case 'add' :
                                return <AddProject 
                                            hideFunc={hideFunc}
                                            saveFunc={function(){
                                                hideFunc();
                                                this.callApi();
                                            }.bind(this)}
                                            departmentId={this.props.params['departmentId']}
                                        />;
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;

        const header = (
            <thead>
                <tr>
                    <th className="col-md-2">Project Name</th>
                    <th className="col-md-2">Team Members</th>
                    <th className="col-md-2">Duration</th>
                    <th className="col-md-2">Status</th>
                    <th className="col-md-2">Actions</th>
                </tr>
            </thead>
        );

        return (
            <div>
                <div>
                    <h1>{this.state.departmentName} Department Projects</h1>
                    {modalTemplate}
                    <button className="btn btn-md btn-info" onClick={this.addProject.bind(this)}>
                        <span className="glyphicon glyphicon-plus-sign"></span>
                        &nbsp;Add new project
                    </button>

                    <PaginatedTable 
                        header={header} 
                        listOfItems={items}
                        totalCount={this.state.projectCount}
                        pageSize={this.state.pageSize}
                        selectedPage={this.state.pageNumber}
                        changeHandler={this.paginationChangeHandler.bind(this)}
                    />

                </div>
            </div>
        );
    }
           
}