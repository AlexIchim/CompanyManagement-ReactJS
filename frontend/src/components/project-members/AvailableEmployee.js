import React from 'react';
import {Link} from 'react-router';

export default class AvailableEmployee extends React.Component {
    constructor(){
        super();
        this.state = {
            isTrue: true
        }
    }

    onChangeInput(id){
        let data = {
            employeeId : id,
            allocation: this.refs.allocationInput.value
        }

        this.props.onChangeAllocation(data);
    }

    onChangeRadio(){
        const {employee_id} = this.refs;
        $(employee_id).closest('tbody').find('.allocationInput').attr('disabled','disabled' );
        $(employee_id).closest('tr').find('.allocationInput').removeAttr('disabled');

        this.setState({
            isTrue: $(employee_id).val() == 'on'
        })

    }

    render(){
        const employee = this.props.data;
        const departmentName = this.props.departmentName;
        const remainingAllocation = 100 - employee.totalAllocation;

        return(
            <tr data-id={employee.id}>
                <td>{employee.name}</td>
                <td> {departmentName} </td>
                <td>{employee.positionName}</td>
                <td>
                    <input type="text" className="allocationInput" ref="allocationInput" disabled={this.state.isTrue} onChange={this.onChangeInput.bind(this, employee.id)}/> / {remainingAllocation} %
                </td>
                <td className="btn-toolbar">
                    <input type="radio" ref="employee_id" name="checked" onChange={this.onChangeRadio.bind(this)}></input>
                </td>
            </tr>
        )
    }
}