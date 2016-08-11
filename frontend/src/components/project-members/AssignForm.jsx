import React from 'react';
import * as Controller from '../../api/controller';
import AvailableEmployee from './AvailableEmployee';
import PaginatedTable from '../layout/PaginatedTable';

export default class AssignForm extends React.Component {

    constructor() {
        super();
        this.state ={
            availableEmployees: [],
            totalAvailableEmployees: 0,
            departmentName: "",
            departments: [],
            positions: [],
            allocation: {},
            departmentIdFilter: null,
            positionIdFilter: null,
            readyToSave: false,
            pageSize: 5,
            pageNumber: 1
        }
    }

    componentWillMount(){
        this.fetchData();

    }

    getAvailableEmployees(deptId, posId){
        Controller.getAvailableEmployees(
            deptId,
            posId,
            this.props.projectId,
            this.state.pageSize,
            1,
            true,
            (data) => {
                this.setState({
                    availableEmployees: data,
                    pageNumber: 1
                });
            }
        )
    }

    getDepartmentName(id){
        Controller.getDepartmentName(
            id,
            true,
            (data) => {
                this.setState({
                    departmentName: data.name
                })
            }
        )
    }

    getAvailableEmployeesCount(deptId, posId){
        Controller.getAvailableEmployeesCount(
            deptId,
            posId,
            this.props.projectId,
            true,
            (data) => {
                this.setState({
                    totalAvailableEmployees: data,
                    pageNumber: 1
                });
            }
        );
    }

    fetchData(){
        this.getAvailableEmployees();
        this.getAvailableEmployeesCount();

        Controller.getDepartments(
            true,
            (data) => {
                this.setState({
                    departments: data
                })
            }
        )

        Controller.getPositions(
            true,
            (data) => {
                this.setState({
                    positions: data
                })
            }
        )
    }

    onChangeDepartment(e){
        let id = e.target.value;
        let deptId =  id == -1 ? null : id;
        this.setState({
            departmentIdFilter: deptId
        });

        this.getAvailableEmployeesCount(deptId, this.state.positionIdFilter);
        this.getAvailableEmployees(deptId, this.state.positionIdFilter);


    }

    onChangePosition(e){
        let id = e.target.value;
        let posId =  id == -1 ? null : id;
        this.setState({
            positionIdFilter: posId
        });

        this.getAvailableEmployeesCount(this.state.departmentIdFilter,posId);
        this.getAvailableEmployees(this.state.departmentIdFilter,posId);

    }

    onChangeAllocation(data, readyToSave){
        if (!readyToSave){
            this.refs.message.innerHTML = 'Invalid allocation.';
        }
        else{
            this.hideError();
        }
        let newAllocation = {
            projectId : this.props.projectId,
            employeeId: data.employeeId,
            allocationPercentage: data.allocation
        }
        this.setState({
            allocation: newAllocation,
            readyToSave: readyToSave
        })
    }

    onSave(){
        if (this.state.readyToSave){
            Controller.addAllocation(
                this.state.allocation,
                true,
                this.props.updateFunc
            )
            this.setState({
                pageNumber: 1
            })
        }
        else
        {
            this.refs.message.innerHTML = 'Please select an employee.';
        }
    }

    hideError(){
        this.refs.message.innerHTML = ''
    }

    paginationChangeHandler(pageSize, pageNumber){
        Controller.getAvailableEmployees(
            this.state.departmentIdFilter,
            this.state.positionIdFilter,
            this.props.projectId,
            pageSize,
            pageNumber,
            true,
            (data) => {
                this.setState({
                    availableEmployees: data,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                });
            }
        );

    }

    render(){
        const items = this.state.availableEmployees.map(
            (e) => <AvailableEmployee
                key={e.id}
                data={e}
                departmentName={this.state.departmentName}
                onChangeAllocation={this.onChangeAllocation.bind(this)}
                hideError={this.hideError.bind(this)}
            />
        );

        const departments = this.state.departments.map(
            (d) => <option key={d.id} value={d.id}>{d.name}</option>
        );

        const positionOptions = this.state.positions.map(
            (pos,ind) => pos.id == 1 ? null : <option key={pos.id} value={pos.id}>{pos.name}</option>
        );

        const header = (
            <thead>
            <tr>
                <th> Name </th>
                <th> Department </th>
                <th> Position </th>
                <th> Remaining allocation </th>
                <th> Choose employee </th>
            </tr>
            </thead>
        );

        return(
            <div>
                <div className="box info-box">
                    <div className="box-header with-border">
                        <div>
                            <h3 className="box-title"><strong>Assign employee</strong></h3>
                            <br/><br/>
                            <div className="form-group">
                                 <select className="form-control" defaultValue="Department" onChange={this.onChangeDepartment.bind(this)}>
                                     <option value="-1" >Department</option>
                                    {departments}
                                 </select>
                            </div>
                            <div className="form-group">
                                 <select className="form-control " defaultValue="Position" onChange={this.onChangePosition.bind(this)}>
                                     <option value="-1">Position</option>
                                     {positionOptions}
                                 </select>
                            </div>
                        </div>
                    </div>
                    <form className="form-horizontal">
                        <div className="box-body">
                        </div>
                        <div className="formBody">

                        </div>

                        <div className="box-footer">
                            <PaginatedTable
                                header={header}
                                listOfItems={items}
                                totalCount={this.state.totalAvailableEmployees}
                                pageSize={this.state.pageSize}
                                selectedPage={this.state.pageNumber}
                                changeHandler={this.paginationChangeHandler.bind(this)}
                            />
                            <br/><br/>
                            <span type="text" ref="message"/>
                            <br/><br/>
                            <button type="button" className="btn btn-md btn-info" onClick={this.onSave.bind(this)} disabled={!this.state.readyToSave}> Save</button>
                            <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                        </div>
                    </form>
                </div>
            </div>

        )

    }

}