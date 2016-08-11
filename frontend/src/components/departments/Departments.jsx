import React, { Component } from 'react';
import * as $ from 'jquery';
import apiconfig from '../../api/config';
import DepartmentRow from './DepartmentRow';
import ModalTemplate from '../layout/ModalTemplate';
import PaginatedTable from '../layout/PaginatedTable'
import EditDetails from './EditDetails';
import * as Controller from '../../api/controller';
import AddDetails from './AddDetails';

import Context from '../../context/Context'
import * as Access from '../../context/accessors'
import * as Command from '../../context/commands';


export default class Departments extends Component {
    constructor () {
        super();
        this.state = {
            officeName: 'Office',
            departmentList: [],
            pageSize: 5,
            pageNumber: 1,
            totalDepartmentCount: 0,
            showModalTemplate: null,
            modalDepartment: {},
            managerList: []
        }
    }

    componentWillReceiveProps(newProps){
        this.reinit(newProps);
    }

    componentWillMount() {
        this.reinit(this.props)
    }

    reinit(props){
        Command.setCurrentOffice(props.params.officeId, this.fetchData.bind(this,props));
        Command.setCurrentDepartment(null);

        Controller.getOfficeName(
            props.params.officeId,
            true,
            (data) => {
                this.setState({
                    officeName: data.name
                });
            });
    }

    fetchData(props) {
        if(!props) props = this.props;

        this.setState({
            totalDepartmentCount: Access.currentDepartments(Context.cursor).size,
            pageSize: 5,
            pageNumber: 1
        });

        this.setState({
            departmentList: Access.currentDepartments(Context.cursor).skip(
                                (this.state.pageNumber - 1) * this.state.pageSize
                            ).take(this.state.pageSize).toJS()
        });
    }

    editDetails(department) {
        let managers = [];

        Controller.getDepartmentsManagers(
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


    paginationChangeHandler(pageSize, pageNumber){
        this.setState({
            pageSize: pageSize,
            pageNumber: pageNumber,
            departmentList: Access.currentDepartments(Context.cursor).skip(
                (pageNumber - 1) * pageSize
            ).take(this.state.pageSize).toJS()
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
                                return <EditDetails department={this.state.modalDepartment}
                                                    officeId={this.props.params.officeId}
                                                    hideFunc={hideFunc}
                                                    managers={this.state.managerList}
                                                    saveFunc={function(){
                                                        hideFunc();
                                                        Command.fetchOfficeDepartmentList(this.fetchData.bind(this));
                                                    }.bind(this)}
                                />
                            case 'add':
                                return <AddDetails officeId={this.props.params.officeId}
                                                    hideFunc={hideFunc}
                                                    managers={this.state.managerList}
                                                    saveFunc={function(){
                                                        hideFunc();
                                                        Command.fetchOfficeDepartmentList(this.fetchData.bind(this));
                                                    }.bind(this)}/>
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;

        const header = (
           <thead>
            <tr>
                <th className="col-md-2">Department</th>
                <th className="col-md-2">Department manager</th>
                <th className="col-md-2">Employees</th>
                <th className="col-md-2">Projects</th>
                <th className="col-md-2">Actions</th>
            </tr>
            </thead> 
        );


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
                <PaginatedTable 
                    header={header} 
                    listOfItems={items}
                    totalCount={this.state.totalDepartmentCount}
                    pageSize={this.state.pageSize}
                    selectedPage={this.state.pageNumber}
                    changeHandler={this.paginationChangeHandler.bind(this)}
                />
            </div>
        );
    }
}