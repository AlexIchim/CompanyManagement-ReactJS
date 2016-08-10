import React from 'react';
import ModalTemplate from '../../ModalTemplate';
import config from '../../helper';
import MyController from '../controller/Controller.js';
import Accessors from '../../../context/Accessors';
import Context from '../../../context/Context';
import Validator from '../../validator/ProjectValidator'
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
                 dropdownItems: data,
                    NameVR: {valid: false, message:""},
                    DurationVR: {valid: false, message:""}
                })
            }.bind(this)
        });
        this.subscription=Context.subscribe(this.onContextChange.bind(this));
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }
    onStoreClick(){
        if(this.state.DurationVR.valid && this.state.NameVR){
            let model = Context.cursor.get('model');

            if(!model){
                model = {}
            }
            let name = this.refs.inputName.value;
            let duration = this.refs.inputDuration.value;
            var select = document.getElementById('dropdown');
            var status = select.options[select.selectedIndex].index;
            console.log('status selected:', status);

            model.Name = (name) ? name : model.Name;
            model.Duration = (duration) ? duration : model.Duration;
            model.Status = (status) ? status : model.Status;

            Context.cursor.set("model", model);
            this.props.FormAction();
        }

    }


    onContextChange(newGlobalCursor){
        this.setState({
            model: newGlobalCursor.get('model'),
            projectName: newGlobalCursor.get('model') && newGlobalCursor.get('model').Name || "",
            projectDuration: newGlobalCursor.get('model') && newGlobalCursor.get('model')['Duration'] || ""
        });
    }

    onNameChange(){
        var result = Validator.ValidateName(this.refs.inputName.value);
        this.updateState(result, null);
        console.log('name validation: ', result);
        if(result.valid){
            this.setState({
                model: this.state.model,
                projectName: this.refs.inputName.value,
            })
        }

    }
    onDurationChange(){
        var result = Validator.ValidateDuration(this.refs.inputDuration.value);
        this.updateState(result, null);
        console.log('duration validation: ', result);
        if(result.valid) {
            this.setState({
                model: this.state.model,
                projectDuration: this.refs.inputDuration.value
            });
        }
    }

    updateState(nameVR, durationVR){
        this.setState({
            NameVR: (nameVR) ? nameVR: this.state.NameVR,
            DurationVR: (durationVR) ? durationVR: this.state.DurationVR
        })
    }
    render(){
        let formIsValid = false;
        let nameVR = "";
        let durationVR = "";

        console.log('form name is valid?', this.state.NameVR.valid)
        console.log('form duration is valid: ', this.state.DurationVR.valid);
        if(this.state.DurationVR.valid && this.state.NameVR){
            formIsValid = true;
            console.log('is valid:', formIsValid);
        }

        if(!this.state.NameVR.valid){
            nameVR=<span>{this.state.NameVR.message}</span>;
        }
        if(!this.state.DurationVR.valid){
            durationVR=<span>{this.state.DurationVR.message}</span>
        }


        const projectName = this.state.projectName;
        const projectDuration = this.state.projectDuration;
        const items = this.state.dropdownItems.map( (status, index) => {
            return ( <option key={status.Index} >{status.Description}</option>
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
                           Model={this.props.Model}
                           formIsValid={formIsValid}
                            >


                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text"
                               ref="inputName"
                               className="form-control"
                               onChange={this.onNameChange.bind(this)}
                               value={projectName}
                        >

                        </input>
                        {nameVR}
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
                        {durationVR}
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