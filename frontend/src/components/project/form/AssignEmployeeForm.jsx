import React from 'react';
import ModalTemplate from '../../ModalTemplate';
import config from '../../helper';
import Context from '../../../context/Context';

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
    }

    onStoreClick(){
        //to be done
    }
    render(){
        console.log('positions: ',this.state.dropdownItemsPosition )
        let itemsPosition = this.state.dropdownItemsPosition.map( (position, index) => {
            return (<option key = {position.Index} > {position.Description}  </option>)
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
                </div>


            </ModalTemplate>
        )}
}

