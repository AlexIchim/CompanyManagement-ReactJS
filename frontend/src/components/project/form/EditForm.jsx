import React from 'react';
import ModalTemplate from '../../ModalTemplate';
import config from '../../helper';
import MyController from '../controller/Controller.js';
import Accessors from '../../../context/Accessors';
import Context from '../../../context/Context';

export default class EditForm extends React.Component {
    constructor(){
        super();
    }
    componentWillMount(){
        //MyController.GetStatusDescriptions();
        $.ajax({
            method:'GET',
            url: config.base + "/project/statusDescriptions",
            async: false,
            success: function(data){
                this.setState({
                 dropdownItems: data
                })
            }.bind(this)
        });
        this.subscription=Context.subscribe(this.onContextChange.bind(this));
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

    componentWillUnmount(){
        this.subscription.dispose();
    }
    onContextChange(newGlobalCursor){
        this.setState({
            model: newGlobalCursor.get('model'),
            projectName: newGlobalCursor.get('model') && newGlobalCursor.get('model').Name || "",
            projectDuration: newGlobalCursor.get('model') && newGlobalCursor.get('model')['Duration'] || ""
        });
    }

    onNameChange(){
        this.setState({
            model: this.state.model,
            projectName: this.refs.inputName.value,
        })
    }
    onDurationChange(){
        this.setState({
            model: this.state.model,
            projectDuration: this.refs.inputDuration.value
        })

    }
    render(){
        const projectName = this.state.projectName;
        const projectDuration = this.state.projectDuration;
        const items = this.state.dropdownItems.map( (status, index) => {
            return ( <option key={index} >{status}</option>
            )});

        const model = Accessors.model(Context.cursor);
        const name = model.Name;
        console.log('duratiooon', model.Duration);
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
                        <input type="text"
                               ref="inputName"
                               className="form-control"
                               onChange={this.onNameChange.bind(this)}
                               value={projectName}>
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputDuration" className="col-sm-2 control-label"> Duration</label>
                    <div className="col-sm-10">
                        <input type="text"
                               className="form-control"
                               ref="inputDuration"
                               onChange={this.onDurationChange.bind(this)}
                               value={projectDuration}
                        >
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