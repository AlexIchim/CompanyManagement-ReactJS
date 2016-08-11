import React from 'react';
import ModalTemplate from '../ModalTemplate';
import config from '../helper';
import MyController from './Controller/Controller.js';
import Context from '../../context/Context';
import Validator from '../validator/EmployeeValidator';
import '../../assets/less/index.less';

export default class Form extends React.Component {
    constructor() {
        super();
    }

    componentWillMount() {
        this.subscription = Context.subscribe(this.onContextChange.bind(this));

        this.setState({
            NameValidationResult:{valid: false, message: ""},
            AddressValidationResult:{valid: false, message: ""},
        })

    }

    componentDidMount(){

        $('#datepicker1').datepicker({});

    }

    onContextChange(newGlobalCursor) {
        this.setState({
            model: newGlobalCursor.get('model'),
            employeeName: newGlobalCursor.get('model') && newGlobalCursor.get('model').Name || '',
            employeeAddress: newGlobalCursor.get('model') && newGlobalCursor.get('model').Address || '',
            employeeEmploymentDate: newGlobalCursor.get('model') && newGlobalCursor.get('model').EmploymentDate || '',
            employeeJobType: newGlobalCursor.get('model') && newGlobalCursor.get('model').JobType || '',
            employeePosition: newGlobalCursor.get('model') && newGlobalCursor.get('model').Position || ''
        })
    }

    componentWillUnmount() {
        this.subscription.dispose();
    }

    onModelChange() {
        this.setState({
            model: this.state.model,
            employeeName: this.refs.inputName.value,
            employeeAddress: this.refs.inputAddress.value,
            employeeEmploymentDate: this.refs.inputEmploymentDate.value,
            employeeJobType: this.refs.inputJobType.value,
            employeePosition: this.refs.inputPosition.value
        })
    }

    onStoreClick() {

        if(     this.state.NameValidationResult.valid
            &&  this.state.AddressValidationResult.valid) {

            let currentModel = this.state.model;
            let modelToStore = {};

            if (currentModel) {
                modelToStore.Id = currentModel.Id;
            }

            modelToStore.Name = this.refs.inputName.value;
            modelToStore.Address = this.refs.inputAddress.value;
            modelToStore.EmploymentDate = this.refs.inputEmploymentDate.value;
            modelToStore.JobType = this.refs.inputJobType.value;
            modelToStore.Position = this.refs.inputPosition.value;
            modelToStore.DepartmentId = 1;

            Context.cursor.set('model', modelToStore);
            this.props.FormAction();
        }
    }

    onChangeName(){
        var result=Validator.ValidateName(this.refs.inputName.value);
        this.updateState(result, null);
    }
    onChangeAddress(){
        var result=Validator.ValidateAddress(this.refs.inputAddress.value);
        this.updateState(null, result);
    }

    updateState(nameVR, addrVR) {
        this.setState({
            NameValidationResult: (nameVR) ? nameVR : this.state.NameValidationResult,
            AddressValidationResult: (addrVR) ? addrVR : this.state.AddressValidationResult
        });
    }

        render(){

            let nameValidationResult="";
            let addressValidationResult="";

            if(!this.state.NameValidationResult.valid){
                nameValidationResult=<span>{this.state.NameValidationResult.message}</span>;
            }

            if(!this.state.AddressValidationResult.valid){
                addressValidationResult=<span>{this.state.AddressValidationResult.message}</span>;
            }


            var formIsValid=false;
            if(    this.state.NameValidationResult.valid
                && this.state.AddressValidationResult.valid)
                {
                    formIsValid = true;
                }
            const employeeName = this.state.employeeName;
            const employeeAddress = this.state.employeeAddress;
            const employeeEmploymentDate = this.state.employeeEmploymentDate;
            const employeeJobType = this.state.employeeJobType;
            const employeePosition = this.state.employeePosition;

            return (

                <ModalTemplate onCancelClick={this.props.onCancelClick}
                               onStoreClick={this.onStoreClick.bind(this)}
                               Title={this.props.Title}
                               Model={this.props.Model}
                               formIsValid={formIsValid} >

                    <div className="btn-group">
                        <button type="button" className="btn btn-info">Job Type</button>
                        <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span className="caret"></span>
                            <span className="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul className="dropdown-menu" role="menu">
                            <li><a href="#">Part Time 4</a></li>
                            <li><a href="#">Part Time 6</a></li>
                            <li><a href="#">Full Time</a></li>
                        </ul>

                        <button type="button" className="btn btn-info">Position</button>
                        <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span className="caret"></span>
                            <span className="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul className="dropdown-menu" role="menu">
                            <li><a href="#">Developer</a></li>
                            <li><a href="#">Project Manager</a></li>
                            <li><a href="#">QA</a></li>
                            <li><a href="#">Department Manager</a></li>
                        </ul>
                    </div>
                    <p></p>
                    <div className="form-group">
                        <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputName" className="form-control"
                                   onChange={this.onModelChange.bind(this)} value={employeeName} placeholder="Name"
                                   onKeyUp={this.onChangeName.bind(this)}>
                            </input>
                            {nameValidationResult}
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputAddress" className="form-control"
                                       onChange={this.onModelChange.bind(this)} value={employeeAddress}
                                       onKeyUp={this.onChangeAddress.bind(this)}
                                       placeholder="Address">
                            </input>
                            {addressValidationResult}
                        </div>
                    </div>

                    <div className="form-group">
                        <label className="col-sm-2 control-label">Employment Date</label>
                        <div className="input-group date">
                            <div className="input-group-addon">
                                <i className="fa fa-calendar"></i>
                            </div>
                            <input type="text" className="form-control pull-right" id="datepicker1"></input>
                        </div>
                    </div>


                </ModalTemplate>
            )
    }
}


