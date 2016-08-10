import * as React from 'react';
import ModalTemplate from '../ModalTemplate';
import Context from '../../context/Context';
import $ from 'jquery';
import config from '../helper';

const DropdownItem = (props) => {
    return (
        <option value={props.element.Id}>
            {props.element.Name}
        </option>
    )
}

export default class Form extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){

        this.setState({
            departmentManagers: null
        });

        this.subscription=Context.subscribe(this.onContextChange.bind(this));



        $.ajax({
            method: 'GET',
            url: config.base + 'employee/departmentManagers',
            async: false,
            success: function (data) {
                console.log("Aici!");
                this.setState({
                    departmentManagers: data
                })
            }.bind(this)
        })
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }

    onContextChange(newGlobalCursor){
        this.setState({
            model: newGlobalCursor.get('model'),
            departmentName: newGlobalCursor.get('model') && newGlobalCursor.get('model').Name || ""
        })
    }

    onModelChange(){
        this.setState({
            model: this.state.model,
            departmentName: this.refs.departmentNameInput.value
        })
    }

    onStoreClick(){
        let currentModel = this.state.model;
        let modelToStore = {};

        if (currentModel){
            modelToStore.Id = currentModel.Id;
        }

        modelToStore.Name = this.refs.departmentNameInput.value;
        var select = document.getElementById('dropdown');
        var departmentManagerId = select.options[select.selectedIndex].value;

        modelToStore.DepartmentManagerId = departmentManagerId;
        modelToStore.OfficeId = this.props.officeId;

        Context.cursor.set("model", modelToStore);
        this.props.FormAction();
    }

    render(){
        let selectedDepartmentManager = -1;
        if (this.state.model){
            selectedDepartmentManager =  this.state.model.DepartmentManagerId;
        }

        const departmentName = this.state.departmentName;

        const departmentManagers = this.state.departmentManagers.map( (item, index) => {
                return (
                    <DropdownItem
                        element = {item}
                        key={index}
                    />
                )
            }
        );

        console.log("Dept: ", departmentManagers);

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
                           Title={this.props.Title}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-4 control-label">Name</label>
                    <div className="col-sm-8">
                        <input type="text"
                               className="form-control"
                               ref="departmentNameInput"
                               onChange={this.onModelChange.bind(this)}
                               value={departmentName}
                               placeholder="Name">
                        </input>
                    </div>

                    <label htmlFor="inputName" className="col-sm-4 control-label">Department Manager</label>
                    <div className="col-sm-8">
                        <select id='dropdown' className="selectpicker" defaultValue={selectedDepartmentManager}>
                            {departmentManagers}
                        </select>
                    </div>

                </div>

            </ModalTemplate>
        )
    }
}