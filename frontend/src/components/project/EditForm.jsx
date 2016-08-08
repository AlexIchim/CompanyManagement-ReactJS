import React from 'react';
import ModalTemplate from '../ModalTemplate';
import config from '../helper';
import MyController from './controller/Controller.js';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';

export default class EditForm extends React.Component {
    constructor(){
        super();
    }

    componentWillMount(){
        MyController.GetStatusDescriptions();
    }

    onStoreClick(){
        let model = Context.cursor.get('model');

        if(!model){
            model = {}
        }

        let name = this.refs.inputName.value;
        let duration = this.refs.inputDuration.value;
        var select = document.getElementById('dropdown');
        var status = select.options[select.selectedIndex].index;

        model.Name = (name) ? name : model.Name;
        model.Duration = (duration) ? duration : model.Duration;
        model.Status = (status) ? status : model.Status;

        Context.cursor.set("model", model);

        this.props.FormAction();
    }

    onInputChange(){

        console.log('input was changed');
    }
    render(){

        const items = Context.cursor.get('dropdownItems').map( (status, index) => {
            return ( <option key={index} >{status}</option>
            )});

        const model = Accessors.model(Context.cursor);
        const name = model.Name;
        const duration = model.Duration;
        const status = model.Status;

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text"  ref="inputName" className="form-control"  onChange={this.OnInputChange} placeholder= {name}>
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputDuration" className="col-sm-2 control-label"> Duration</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputDuration" placeholder="Duration" value={duration}>
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputStatus" className="col-sm-2 control-label"> Status</label>
                    <select id='dropdown' className="selectpicker">
                        {items}
                    </select>

                </div>
            </ModalTemplate>
        )
    }
}