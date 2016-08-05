import React, { Component } from 'react';
import * as $ from 'jquery';
import apiconfig from '../../api/config';
import DepartmentRow from './DepartmentRow';
import ModalTemplate from '../layout/ModalTemplate';
import EditDetails from './EditDetails';
import * as Controller from '../api/controller';
import AddDetails from './AddDetails';


export default class Departments extends Component {


        constructor () {
            super();
            this.state = {
                officeName: 'Office',
                departmentList: [],
                showModalTemplate: null,
                modalDepartment: {},
                managerList: []
            }
        }

        componentWillMount() {
            Controller.getOfficeName(
                this.props.params.officeId,
                true,
                (data) => {
                    this.setState({
                        officeName: data.name
                    });
                });
            this.fetchData();
        }

        fetchData() {
            Controller.getDepartmentsByOffice(
                this.props.params.officeId,
                true,
                (data) => {
                    this.setState({
                        departmentList: data
                    });
                }
            );
        }

        editDetails(department) {
            let managers = [];

            Controller.getDepartmentsManagers(null,
                true,
                (data) => {
                    this.setState({
                        managerList: data
                    });
                }
            );

           this.setState({
               showModalTemplate: 'edit',
               modalDepartment: department,
               managerList: managers
           });

        }

        addDetails(department) {
            let managers = [];

            Controller.getDepartmentsManagers(null,
                true,
                (data) => {
                    this.setState({
                        managerList: data
                    });
                }
            );

            this.setState({
                showModalTemplate: 'add',
                modalDepartment: department,
                managerList: managers
            });
        }


        hideModal() {
                this.setState({
                    showModalTemplate: null,
                    modalDepartment: {}
                });
        }


        render() {
            const items = this.state.departmentList.map((element, index) => {
                return (
                    <DepartmentRow
                        key = {element.id}
                        data ={element}
                        node = {element}
                        onEdit={this.editDetails.bind(this, element)}
                    />
                )
            });

            const modalTemplate = this.state.showModalTemplate !== null ? (
                <ModalTemplate
                    getComponent={
                        (hideFunc) => {
                            switch (this.state.showModalTemplate) {
                                case 'edit':
                                    return <EditDetails department={this.state.modalDepartment} hideFunc={hideFunc} managers={this.state.managerList}/>
                                case 'add':
                                    return <AddDetails officeId={this.props.params.officeId} hideFunc={hideFunc} managers={this.state.managerList}
                                                        saveFunc={function(){hideFunc(); this.fetchData();}.bind(this)}/>

                            }
                        }
                    }
                    onHide={this.hideModal.bind(this)}
                />
            ) : null;

            return (
                <div>
                    <h1>{this.state.officeName}</h1>
                    {modalTemplate}
                    <br/>
                    <button className="btn btn-md btn-info"  onClick={this.addDetails.bind(this)}>
                        <span className="glyphicon glyphicon-plus-sign"></span>
                        &nbsp;Add new department
                    </button>
                    <br/>
                    <br/>
                    <table className="table table-hover table-bordered" id="table1">
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
                </div>
            );
        }
}
