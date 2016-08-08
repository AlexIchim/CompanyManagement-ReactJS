import React, { Component } from 'react';

import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

import Item from './Item';
import AssignForm from './AssignForm';
import * as Controller from '../../api/controller';
import ModalTemplate from '../layout/ModalTemplate';
import EditAllocation from './EditAllocation';

export default class ProjectMembers extends Component {
    constructor() {
        super();
        this.state = { 
            projectName: 'Project', 
            memberList: [],
            showModalTemplate: null,
            employeeAllocation: []
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

    fetchData(){
        const projectId = this.props.params.projectId;
        Controller.getEmployeesByProjectId(
            projectId,
            true,
            (data) => {
                this.setState({
                    memberList: data
                });
            }
        )
    }

    deleteAllocation(allocationId){
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


        return (
            <div>
                <h1>{this.state.projectName} Members:</h1>
                <br/>
                <button className="btn btn-md btn-info" onClick={this.assignEmployee.bind(this)}>Assign Employee</button>
                <br/>
                <br/>
                {modalTemplate}
                <table className="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Role</th>
                            <th>Allocation</th>
                            <th>Actions</th>
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