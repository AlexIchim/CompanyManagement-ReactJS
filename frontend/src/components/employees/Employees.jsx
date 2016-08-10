import React, { Component } from 'react';

import $ from 'jquery';
import {default as apiconfig} from '../../api/config';
import ModalTemplate from '../layout/ModalTemplate';
import PaginatedTable from '../layout/PaginatedTable'

import Item from './Item';
import ViewDetailsModal from './ViewDetailsModal';
import EditModal from './EditModal';
import AddModal from './AddModal';

import * as Controller from '../../api/controller';

import * as Command from '../../context/commands';


export default class Employees extends Component {
    constructor() {
        super();
        this.state = { 
            departmentName: 'Department', 
            employeeList: [],
            pageSize: 5,
            pageNumber: 1,
            totalEmployeeCount: 0,
            showModalTemplate: null,
            modalEmployee: {},
            searchText: '',
            posFilter: null,
            positionList: [],
            selectedPosition: ''
        };
    }

    componentWillMount(){
        this.init();
    }
    componentWillReceiveProps(newProps){
        this.props = newProps;
        this.init();
    }


    init(){
        Command.setCurrentOffice(this.props.params.officeId);
        Command.setCurrentDepartment(this.props.params.departmentId);
        Controller.getDepartmentById(
            this.props.params.departmentId,
            true,
            (data) => {
                this.setState({
                    departmentName: data.name
                });
            } 
        );
        Controller.getPositions(
            true,
            (data) => {
                this.setState({
                    positionList: data
                });
            }
        );
        this.fetchData();
    }

    fetchData(newSearchText, newPosFilter) {
        let stext = (newSearchText === null || newSearchText === undefined) ? this.state.searchText : newSearchText;
        let pfilter = (newPosFilter === null || newPosFilter === undefined) ? this.state.selectedPosition : newPosFilter;

        if(pfilter === ''){ 
            pfilter = null;
        }

        Controller.getEmployeeCountByDepartmentId(
            this.props.params.departmentId,
            stext,
            pfilter,
            true,
            (data) => {
                this.setState({
                    totalEmployeeCount: data
                });
            }
        );


        Controller.getEmployeesByDepartmentId(
            this.props.params.departmentId,
            this.state.pageSize,
            this.state.pageNumber,
            stext,
            pfilter,
            true,
            (data) => {
                this.setState({
                    employeeList: data
                });
            }
        );
    }

    viewDetails(employee) {
        Controller.getEmployeeAllocations(
            employee.id,
            true,
            (data) => {
                this.setState({
                    allocationList: data
                });
            }
        );

        this.setState({
            showModalTemplate: 'view',
            modalEmployee: employee,
            allocationList: []
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

    deleteItem(id){
        Controller.releaseEmployee(
            id, 
            true, 
            this.fetchData.bind(this)
        ); 
    }

    paginationChangeHandler(pageSize, pageNumber){
        Controller.getEmployeesByDepartmentId(
            this.props.params.departmentId,
            pageSize,
            pageNumber,
            this.state.searchText,
            this.state.selectedPosition,
            true,
            (data) => {
                this.setState({
                    employeeList: data,
                    pageSize: pageSize,
                    pageNumber: pageNumber,
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

    onPositionFilterChange(e){
         this.setState({
            selectedPosition: e.target.value
        });
        this.fetchData(null, e.target.value);
    }


    render() {

        const items = this.state.employeeList.map( 
            (e) => <Item key={e.id} data={e}
                         onView={this.viewDetails.bind(this, e)}
                         onEdit={this.editDetails.bind(this, e)}
                         onDelete={this.deleteItem.bind(this,e.id)}
                    />
        );


        const header = (
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
            </thead>);

        const modalTemplate = this.state.showModalTemplate !== null ? (
            <ModalTemplate
                getComponent={
                    (hideFunc) => {
                        switch(this.state.showModalTemplate) {
                            case 'view':
                                return <ViewDetailsModal 
                                            employee={this.state.modalEmployee} 
                                            hideFunc={hideFunc} 
                                            allocationList={this.state.allocationList} />;
                            case 'edit':
                                return <EditModal 
                                            employee={this.state.modalEmployee} 
                                            departmentId={this.props.params.departmentId}
                                            saveFunc={function(){ hideFunc(); this.fetchData(); }.bind(this)}
                                            hideFunc={hideFunc}
                                        />;
                            case 'add':
                                return <AddModal 
                                            departmentId={this.props.params.departmentId}
                                            saveFunc={function(){ hideFunc(); this.fetchData(); }.bind(this)}
                                            hideFunc={hideFunc} />;
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;

        const positionOptions = this.state.positionList.map(
            e => <option value={e.id} key={e.id}>{e.name}</option>
        );

        return (
            <div>
                <h1>{this.state.departmentName} Employees:</h1>
                {modalTemplate}
                <br/>
                <button className="btn btn-md btn-info"  onClick={this.addDetails.bind(this)}>
                        <span className="glyphicon glyphicon-plus-sign"></span> 
                        &nbsp;Add new employee 
                </button>
                <br/><br/>
                <div>
                    <div className="col-md-6">
                        <label>Search:&nbsp;&nbsp;</label>
                        <input type="text" value={this.state.searchText} onChange={this.onSearchTextChange.bind(this)} />
                    </div>
                    <div className="col-md-6">
                        <select className="pull-right" value={this.state.selectedPosition} onChange={this.onPositionFilterChange.bind(this)}>
                            <option value="" key={''}>All Positions</option>
                            {positionOptions}
                        </select>
                    </div>
                </div>
                
                <br/>
                <PaginatedTable 
                    header={header} 
                    listOfItems={items}
                    totalCount={this.state.totalEmployeeCount}
                    pageSize={this.state.pageSize}
                    selectedPage={this.state.pageNumber}
                    changeHandler={this.paginationChangeHandler.bind(this)}
                />
            </div>
        );
    }
}