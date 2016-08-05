import * as React from 'react';
import ModalTemplate from '../ModalTemplate';
import Context from '../../context/Context';
import $ from 'jquery';
import config from '../helper';
import Accessors from '../../context/Accessors';

const DropdownItem = (props) => {
    return (
        <option>
            {props.element}
        </option>
    )
}

export default class Form extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){

        $.ajax({
            method: 'GET',
            url: config.base + 'employee/getAll',
            async: false,
            success: function (data) {
                //Context.cursor.set('dropdownItems', data);
            }.bind(this)
        })

    }

    onStoreClick(){
        let model=Context.cursor.get("model");

        if(!model){
            model={};
        }

        let name=this.refs.inputName.value;
        //let departmentManagerId = 1;

        model.Name = (name)? name : model.Name;
        model.DepartmentManagerId = 1;
        model.OfficeId = 1;

        Context.cursor.set("model", model);

        this.props.FormAction();
    }

    render(){

        const model=Context.cursor.get('model');
        const name=(model)? model.Name : "Name";

        /*const departmentManagers = Accessors.dropdownItems(Context.cursor).map( (item, index) => {
                return (
                    <DropdownItem
                        element = {item.Name}
                        key={index}
                    />
                )
            }
        );*/

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
                           Title={this.props.Title}>

                <div className="form-group">

                    <label htmlFor="inputName" className="col-sm-4 control-label">Name</label>
                    <div className="col-sm-8">
                        <input type="text"
                               className="form-control"
                               ref="inputName"
                               placeholder={name}>
                        </input>
                    </div>

                    {/*<label htmlFor="inputName" className="col-sm-4 control-label">Department Manager</label>
                    <div className="col-sm-8">
                        <select id='dropdown' className="selectpicker">
                            {departmentManagers}
                        </select>
                    </div>*/}

                </div>

            </ModalTemplate>
        )
    }
}