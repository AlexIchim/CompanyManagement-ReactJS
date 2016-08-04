import React, { Component } from 'react';

import $ from 'jquery';
import {default as apiconfig} from '../../api/config';
import ModalTemplate from '../layout/ModalTemplate';

import Item from './Item';
import ViewDetails from './ViewDetails';
import EditDetails from './EditDetails';
import AddDetails from './AddDetails';



export default class Employees extends Component {
    constructor() {
        super();
        this.state = { 
            departmentName: 'Department', 
            employeeList: [],
            showModalTemplate: null,
            modalEmployee: {}
        };
    }


    componentDidMount() {
        const departmentId = this.props.params.departmentId;

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'departments/' + this.props.params.departmentId,
            async: true,
            success: (data) => {
                this.setState({
                    departmentName: data.name,
                });
            } 
        });

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'departments/' + this.props.params.departmentId + '/employees',
            async: true,
            success: (data) => {
                this.setState({
                    employeeList: data
                });
            } 
        });
    }

    viewDetails(employee) {
        let allocations = [];

        $.ajax({
            method: 'GET',
            url: apiconfig.baseUrl + 'employees/' + employee.id + '/allocations',
            async: false,
            success: (data) => {
                allocations = data;
            }
        });


        this.setState({
            showModalTemplate: 'view',
            modalEmployee: employee,
            allocationList: allocations
        });
    }

    editDetails(employee) {
        this.setState({
            showModalTemplate: 'edit',
            modalEmployee: employee,
        });
    }

    addDetails(employee) {
        this.setState({
            showModalTemplate: 'add',
            modalEmployee: employee,
        });
    }

    hideModal() {
        this.setState({
            showModalTemplate: null,
            modalEmployee: {}
        });
    }


    render() {

        const items = this.state.employeeList.map( 
            (e) => <Item key={e.id} data={e}
                         onView={this.viewDetails.bind(this, e)}
                         onEdit={this.editDetails.bind(this, e)}
                    />
        );

        const modalTemplate = this.state.showModalTemplate !== null ? (
            <ModalTemplate
                getComponent={
                    (hideFunc) => {
                        switch(this.state.showModalTemplate) {
                            case 'view':
                                return <ViewDetails employee={this.state.modalEmployee} hideFunc={hideFunc} allocationList={this.state.allocationList} />;
                            case 'edit':
                                return <EditDetails employee={this.state.modalEmployee} hideFunc={hideFunc}/>;
                            case 'add':
                                return <AddDetails employee={this.state.modalEmployee} hideFunc={hideFunc}/>;
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;

        return (
            <div>
                <h1>{this.state.departmentName} Employees:</h1>
                {modalTemplate}
                <br/>
                <button className="btn btn-md btn-info"  onClick={this.addDetails.bind(this)}>
                        <span className="glyphicon glyphicon-plus-sign"></span> 
                        &nbsp;Add new employee 
                </button>
                <br/>
                <br/>
                <table className="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Address</th>
                            <th>Employment Date</th>
                            <th>Termination Date</th>
                            <th>Employment Hours</th>
                            <th>Position</th>
                            <th>Total Allocation</th>
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