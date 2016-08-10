import React from 'react';
import ModalTemplate from '../ModalTemplate';
import config from '../helper';
import MyController from './Controller/Controller.js';
import Context from '../../context/Context';

export default class Form extends React.Component {
    constructor() {
        super();
    }

    componentWillMount() {
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
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

        {/*var select = document.getElementById('dropdown');
         var departmentManagerId = select.options[select.selectedIndex].value;

         modelToStore.DepartmentManagerId = departmentManagerId;
         modelToStore.OfficeId = this.props.officeId;*/}

        Context.cursor.set("model", modelToStore);
        this.props.FormAction();
    }

        render()
        {
            const employeeName = this.state.employeeName;
            const employeeAddress = this.state.employeeAddress;
            const employeeEmploymentDate = this.state.employeeEmploymentDate;
            const employeeJobType = this.state.employeeJobType;
            const employeePosition = this.state.employeePosition;

            return (

                <ModalTemplate onCancelClick={this.props.onCancelClick}
                               onStoreClick={this.onStoreClick.bind(this)}
                               Title={this.props.Title}
                               Model={this.props.Model}>

                    <div className="form-group">
                        <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputName" className="form-control"
                                   onChange={this.onModelChange.bind(this)} value={employeeName} placeholder="Name">
                            </input>
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputAddress" className="form-control"
                                       onChange={this.onModelChange.bind(this)} value={employeeAddress}
                                       placeholder="Address">
                            </input>
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputEmploymentDate" className="col-sm-2 control-label"> Employment Date</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputEmploymentDate" className="form-control"
                                           onChange={this.onModelChange.bind(this)} value={employeeEmploymentDate}
                                           placeholder="EmploymentDate">
                            </input>
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputJobType" className="col-sm-2 control-label"> Job Type</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputJobType" className="form-control"
                                               onChange={this.onModelChange.bind(this)} value={employeeJobType}
                                               placeholder="JobType">
                            </input>
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputPosition" className="col-sm-2 control-label"> Position</label>
                        <div className="col-sm-10">
                            <input type="text" ref="inputPosition" className="form-control"
                                                   onChange={this.onModelChange.bind(this)} value={employeePosition}
                                                   placeholder="Position">
                            </input>
                        </div>
                    </div>

                </ModalTemplate>
            )
        }
}


