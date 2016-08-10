import React, { Component } from 'react';

import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

import Item from './Item';
import AssignForm from './AssignForm';
import * as Controller from '../../api/controller';
import ModalTemplate from '../layout/ModalTemplate';
import EditAllocation from './EditAllocation';
import PaginatedTable from '../layout/PaginatedTable';

export default class ProjectMembers extends Component {
    constructor() {
        super();
        this.state = { 
            projectName: 'Project', 
            memberList: [],
            showModalTemplate: null,
            employeeAllocation: [],
            totalProjectMembers: 0,
            pageSize: 5,
            pageNumber: 1
        };
    }

    componentWillMount(){
        const projectId = this.props.params.projectId;
        Controller.getProjectName(
            projectId,
            true,
            (data) => {
                this.setState({
                    projectName: data.name,
                });
            }
        );
        this.fetchData();
    }

    getProjectMembersCount(){
        const projectId = this.props.params.projectId;
        Controller.getProjectMembersCount(
            projectId,
            true,
            (data) => {
                this.setState({
                    totalProjectMembers: data
                })
            }
        )
    }

    getEmployeesByProjectId(projectId, pageSize, pageNumber){
        Controller.getEmployeesByProjectId(
            projectId,
            pageSize,
            pageNumber,
            true,
            (data) => {
                this.setState({
                    memberList: data
                });
            }
        )
    }

    fetchData(){
        const projectId = this.props.params.projectId;
        this.getEmployeesByProjectId(projectId, this.state.pageSize, this.state.pageNumber);
        this.getProjectMembersCount();
    }

    deleteAllocation(allocationId){
        this.setState({
            pageNumber: 1
        })
        Controller.deleteAllocation(
            allocationId,
            true,
            this.fetchData.bind(this)
        )
    }

    editAllocation(employeeAllocation){
        this.setState({
            showModalTemplate : 'edit',
            employeeAllocation: employeeAllocation
        });

    }

    assignEmployee(){
        this.setState({
            showModalTemplate : 'assign'
        });
    }

    hideModal() {
        this.setState({
            showModalTemplate: null,
            modalAllocation: {}
        });
    }

    paginationChangeHandler(pageSize, pageNumber){
        Controller.getEmployeesByProjectId(
            this.props.params.projectId,
            pageSize,
            pageNumber,
            true,
            (data) => {
                this.setState({
                    memberList: data,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                });
            }
        );

    }

    render() {
        const items = this.state.memberList.map(
            (e) => <Item
                        key={e.employee.id}
                        data={e}
                        onDelete={this.deleteAllocation.bind(this, e.allocationId)}
                        onEdit={this.editAllocation.bind(this, e)}
                    />
        );

        const modalTemplate = this.state.showModalTemplate !== null ? (
            <ModalTemplate
                getComponent={
                    (hideFunc) => {
                        switch(this.state.showModalTemplate) {
                            case 'edit':
                                return <EditAllocation employeeAllocation={this.state.employeeAllocation} updateFunc={function(){
                                    hideFunc();
                                    this.fetchData();
                                }.bind(this)} hideFunc={hideFunc}/>;
                            case 'assign':
                                return <AssignForm
                                    departmentId = {this.props.params['departmentId']}
                                    projectId = {this.props.params.projectId}
                                    updateFunc={function(){
                                    hideFunc();
                                    this.fetchData();
                                }.bind(this)} hideFunc={hideFunc}/>;
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;


        const header = (
            <thead>
            <tr>
                <th>Name</th>
                <th>Department</th>
                <th>Role</th>
                <th>Allocation</th>
                <th>Actions</th>
            </tr>
            </thead>
        )

        return (
            <div>
                <h1>{this.state.projectName} Members:</h1>
                <br/>
                <button className="btn btn-md btn-info" onClick={this.assignEmployee.bind(this)}>Assign Employee</button>
                <br/>
                <br/>
                {modalTemplate}
                <PaginatedTable
                    header={header}
                    listOfItems={items}
                    totalCount={this.state.totalProjectMembers}
                    pageSize={this.state.pageSize}
                    selectedPage={this.state.pageNumber}
                    changeHandler={this.paginationChangeHandler.bind(this)}
                />

            </div>
        );

    }
}