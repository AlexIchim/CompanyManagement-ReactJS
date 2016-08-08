import React from 'react';
import * as Controller from '../../api/controller';
import AvailableEmployee from './AvailableEmployee';
import Context from "../../context/Context";

export default class AssignForm extends React.Component {

    constructor() {
        super();
        this.state ={
            availableEmployees: [],
            departmentName: "",
            departments: [],
            positions: [],
            allocation: {},
            departmentIdFilter: null,
            positionIdFilter: null,
            readyToSave: false
        }
    }

    componentWillMount(){
        this.fetchData();

    }

    componentDidMount(){
        this.availableEmployees();
    }

    availableEmployees(deptId, posId){
        Controller.getAvailableEmployees(
            deptId,
            posId,
            false,
            (data) => {
                this.setState({
                    availableEmployees: data
                })
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

    fetchData(){
        this.availableEmployees();
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
        this.availableEmployees(deptId, this.state.positionIdFilter);

    }

    onChangePosition(e){
        let id = e.target.value;
        let posId =  id == -1 ? null : id;
        this.setState({
            positionIdFilter: posId
        });
        this.availableEmployees(this.state.departmentIdFilter,posId);

    }

    onChangeAllocation(data, readyToSave){
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
        console.log("@@@@", this.refs.message);
        if (this.state.readyToSave){
            Controller.addAllocation(
                this.state.allocation,
                true,
                this.props.updateFunc
            )
        }
        else
        {
            this.refs.message.innerHTML = 'Please select an employee.';
        }
    }

    hideError(){
        this.refs.message.innerHTML = ''
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
            (pos,ind) => <option key={pos.id} value={pos.id}>{pos.name}</option>
        );

        return(
            <div>
                <div className="box info-box">
                    <div className="box-header with-border">
                        <div>
                            <h3 className="box-title"><strong>Assign employee</strong></h3>
                            <br/><br/>
                            <div className="form-group">
                                 <select className="form-control " defaultValue="Department" onChange={this.onChangeDepartment.bind(this)}>
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

                       <table className="table table-hover table-bordered">
                           <thead>
                               <tr>
                                   <th> Name </th>
                                   <th> Department </th>
                                   <th> Position </th>
                                   <th> Remaining allocation </th>
                                   <th> Choose employee </th>
                               </tr>
                           </thead>

                           <tbody>

                                {items}

                           </tbody>

                       </table>


                        </div>

                        <div className="box-footer">
                            <span type="text" ref="message"/>
                            <br/>
                            <button type="button" className="btn btn-md btn-info" onClick={this.onSave.bind(this)}> Save</button>
                            <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                        </div>
                    </form>
                </div>
            </div>

        )

    }

}