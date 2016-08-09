import React from 'react';
import ModalTemplate from '../../ModalTemplate';
import config from '../../helper';
import Context from '../../../context/Context';


const EmployeeItem = (props) => {
    return (
        <tr>
            <td>{props.element['Name']}</td>
            <td>{props.element['Department']}</td>
            <td>{props.element['Position']}</td>
            <td>{props.element['RemainingAllocation']}</td>
        </tr>
    )

}
export default class AssignEmployeeForm extends React.Component{
    constructor(){
        super();
    }

    componentWillMount() {
        $.ajax({
            method:'GET',
            url: config.base + "/employee/getPositions",
            async: false,
            success: function(data){
                this.setState({
                    dropdownItemsPosition: data
                })
            }.bind(this)
        });


        $.ajax({
            method: 'GET',
            url: config.base + "/department/GetAll",
            async: false,
            success: function(data){
                this.setState({
                    dropdownItemsDepartments: data
                })
            }.bind(this)
        });

        $.ajax({
            method: 'GET',

            url: config.base + "office/availableEmployees/1/5/1",
            async: false,
            success: function(data){
                this.setState({
                    availableEmployees: data
                })
            }.bind(this)
        });
    }

    onStoreClick(){
        //to be done
    }

    onPositionTypeChange(){
        console.log('changed');
    }
    render(){
        console.log('positions: ',this.state.dropdownItemsPosition );

        const availableEmployees = this.state.availableEmployees.map( (employee, index) =>{
            return (
                <EmployeeItem
                    element = {employee}
                    key = {index}
                />
            )
        })

        let itemsPosition = this.state.dropdownItemsPosition.map( (position, index) => {
            return (<option key = {position.Index} onChange={this.onPositionTypeChange.bind(this)} > {position.Description}  </option>)
        });

        let itemsDepartment = this.state.dropdownItemsDepartments.map ( (department, index) => {
            return (<option key = {department.Id} > {department.Name} </option>)
        });

        return(
            <ModalTemplate
                onCancelClick={this.props.onCancelClick}
                onStoreClick={this.onStoreClick.bind(this)}
            >
                <h3> Assign Employee </h3>
                <div className="form-group">
                    <label htmlFor="inputStatus" className="col-sm-2 control-label"> Filter by</label>

                    <select id='dropdown' className="selectpicker">
                        {itemsPosition}
                    </select>

                    <select id='dropdown' className="selectpicker">
                        {itemsDepartment}
                    </select>

                    <div>
                    <table className="table table-stripped">

                    <tr>
                        <td>Name </td>
                        <td> Department </td>
                        <td> Position </td>
                        <td> Remaining Allocation</td>
                    </tr>
                        {availableEmployees}
                        </table>
                        </div>
                </div>

            </ModalTemplate>
        )}
}

