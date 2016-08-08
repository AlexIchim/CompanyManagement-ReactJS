import React from 'react';
import {Link} from 'react-router';

export default class AvailableEmployee extends React.Component {
    constructor(){
        super();
        this.state = {
            isTrue: true
        }
    }

    onChangeInput(id, event){
        let data = {
            employeeId : id,
            allocation: event.target.value
        }
        this.props.onChangeAllocation(data, true);
    }

    onChangeRadio(id){
        this.props.hideError();
        const {employee_id} = this.refs;
        $(employee_id).closest('tbody').find('.allocationInput').attr('disabled','disabled' );
        $(employee_id).closest('tr').find('.allocationInput').removeAttr('disabled');

        this.setState({
            isTrue: $(employee_id).val() == 'on'
        })

        let data = {
            employeeId : id,
            allocation: this.refs.allocationInput.value
        }
        this.props.onChangeAllocation(data, true);



    }

    render(){
        const employee = this.props.data;
        const remainingAllocation = 100 - employee.totalAllocation;

        return(
            <tr data-id={employee.id}>
                <td>{employee.name}</td>
                <td> {employee.departmentName} </td>
                <td>{employee.positionName}</td>
                <td>
                    <input type="text" className="allocationInput" ref="allocationInput" defaultValue={remainingAllocation} disabled={this.state.isTrue} onChange={this.onChangeInput.bind(this,  employee.id)}/> / {remainingAllocation} %
                </td>
                <td className="btn-toolbar">
                    <input type="radio" ref="employee_id" name="checked" onChange={this.onChangeRadio.bind(this, employee.id)}></input>
                </td>
            </tr>
        )
    }
}