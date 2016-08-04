import React, { Component } from 'react';

import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

import Item from './Item';
import AssignForm from './AssignForm';

export default class ProjectMembers extends Component {
    constructor() {
        super();
        this.state = { 
            projectName: 'Project', 
            memberList: [],
            showAssignForm: false 
        };
    }


    componentDidMount() {
        const projectId = this.props.params.projectId;

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'projects/' + this.props.params.projectId,
            async: true,
            success: (data) => {
                this.setState({
                    projectName: data.name,
                });
            } 
        });

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'projects/' + this.props.params.projectId + '/employees',
            async: true,
            success: (data) => {
                this.setState({
                    memberList: data
                });
            } 
        });
    }
    
    render() {
        const items = this.state.memberList.map( 
            (e) => <Item key={e.employee.id} data={e} />
        );

        const assignForm = this.state.showAssignForm ? (<AssignForm />) : null;

        return (
            <div>
                <h1>{this.state.projectName} Members:</h1>
                <br/>
                <button className="btn btn-md btn-info" 
                    onClick={
                        (e) => this.setState({ showAssignForm: true })
                    }
                > 
                        <span className="glyphicon glyphicon-plus-sign"></span> 
                        &nbsp;Assign Employee 
                </button>
                <br/>
                <br/>
                <table className="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Role</th>
                            <th>Allocation</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items}
                    </tbody>
                </table>

                {assignForm}
            </div>
        );
    }
}