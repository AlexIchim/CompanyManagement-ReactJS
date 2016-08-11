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
            pageNumber: 1,
            selectedStatus: '',
            searchText: ''
        }
    }

    componentWillMount(){
        const departmentId = this.props.params.departmentId;
        fetchData();
    }

    deleteProject(id){
        this.setState({
            pageNumber: 1
        })
       Controller.deleteProject(
           id,
           true,
           (data) => {
               this.fetchData();
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
        this.fetchData();
    }

    componentWillReceiveProps(newProps){
        this.props = newProps;
        this.fetchData();
    }

    fetchData(newSearchText, newStatusFilter){
        let stext = (newSearchText === null || newSearchText === undefined) ? this.state.searchText : newSearchText;
        let stfilter = (newStatusFilter === null || newStatusFilter === undefined) ? this.state.selectedStatus : newStatusFilter;

        if(!stfilter){ 
            stfilter = '';
        }

        Command.setCurrentOffice(this.props.params.officeId);
        Command.setCurrentDepartment(this.props.params.departmentId);

        Controller.getProjectsByDepartmentId(
            this.props.params.departmentId,
            this.state.pageSize,
            this.state.pageNumber,
            stext,
            stfilter,
            true,
            (data) => {
                this.setState({
                    projects : data
                });
        });

        Controller.getProjectCountByDepartmentId(
            this.props.params.departmentId,
            stext,
            stfilter,
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
            this.state.searchText,
            this.state.selectedStatus,
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

    onSearchTextChange(e){
        this.setState({
            searchText: e.target.value
        });
        this.fetchData(e.target.value,null);
    }

    onStatusFilterChange(e){
         this.setState({
            selectedStatus: e.target.value
        });
        this.fetchData(null, e.target.value);
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
                    <br/>
                    <button className="btn btn-md btn-info" onClick={this.addProject.bind(this)}>
                        <span className="glyphicon glyphicon-plus-sign"></span>
                        &nbsp;Add new project
                    </button>
                    <br/><br/>

                    <div>
                        <div className="col-md-2 input-group pull-left">
                            <input type="text" value={this.state.searchText} placeholder="Search by name" className="form-control" onChange={this.onSearchTextChange.bind(this)} />
                            <span className="input-group-addon"><i className="fa fa-search"></i></span>
                        </div>
                        <div className="col-md-2 pull-right">
                            <select className="pull-right form-control" value={this.state.selectedStatus} onChange={this.onStatusFilterChange.bind(this)}>
                                <option value="" key={''}>Any Status</option>
                                <option value="Not Started">Not started</option>
                                <option value="In progress">In progress</option>
                                <option value="On hold">On hold</option>
                                <option value="Done">Done</option>
                            </select>
                        </div>
                    </div>

                    <br/><br/><br/>
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