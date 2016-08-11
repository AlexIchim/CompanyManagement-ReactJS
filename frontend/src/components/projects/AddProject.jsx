import React from 'react';
import {hideFunc} from '../layout/ModalTemplate';
import Project from './Project';
import Projects from './Projects';
import config from '../../api/config';
import addProject from '../../api/controller/addProject';
import * as $ from 'jquery';

export default class AddProject extends React.Component{

    constructor(){
        super();
        this.state = {
            status : "Not started",
            nameMessage : "Error!!! Project name cannot be empty",
            durationMessage : "",
            numberOfErrors : 1
        }
    }

    addProject(){
            let projectObject = {
                name : this.refs.name.value,
                status : this.state.status,
                duration : this.refs.duration.value,
                departmentId : this.props.departmentId
            };

            addProject(
                projectObject,
                false,
                this.props.saveFunc
            );
    }

    changeOption(e){      
        let newProject = this.state.project; 
        newProject['status'] = e.target.value;
        this.setState({
            project: newProject
        });
    }

    getNumberOfErrors(){
        let numberOfErrors = 0;
        if(this.state.nameMessage !== ""){
            numberOfErrors++;
        }

        if(this.state.durationMessage !== ""){
            numberOfErrors++;
        }

        return numberOfErrors;
    }

    onInputChange(){
        let newProjectName = this.refs.name.value;
        let newProjectDuration = parseInt(this.refs.duration.value);
        
        if(newProjectName === "")
        {
            this.setState({
                nameMessage : "Error!!! Project name cannot be empty."
            });
        } else if(newProjectName.length > 100) {
            this.setState({
                nameMessage : "Error!!! Project name cannot be longer than 100 characters."
            });
        } else { 
            this.setState({
                nameMessage : ""
            })
        }
        
        
        if(isNaN(newProjectDuration)) {
            newProjectDuration = 0
        } else if(newProjectDuration > 120) {
            this.setState({
                durationMessage : "Error!!! A project cannot last more than 120 months."
            });
            newProjectDuration = parseInt(newProjectDuration / 10);  
        } else {
            this.setState({
                durationMessage : ""
            });
        }

        this.refs.duration.value = newProjectDuration.toString();
        this.refs.name.value = newProjectName.substr(0, 99);
        this.setState({
            numberOfErrors : this.getNumberOfErrors()
        })
    }

    render(){

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new project</h3>
                </div>
            <form className="form-horizontal">
                <div className="box-body">
                    <label>Project name:</label>
                    <input type="text" ref="name" className="form-control" value={this.state.name} autoComplete="off" onChange={this.onInputChange.bind(this)}></input>
                    <label>Status</label>
                    <div>
                        <select className="form-control" id="choice" onChange={this.changeOption.bind(this)}>
                            <option value="Not started">Not started</option>
                            <option value="In progress">In progress</option>
                            <option value="On hold">On hold</option>
                            <option value="Done">Done</option>
                        </select>
                    </div>
                    <label>Duration</label>
                    <input type="text" autoComplete="off" ref="duration" className="form-control" value={this.state.duration} onChange={this.onInputChange.bind(this)}></input>
                    <div>
                        <font color="red">
                            <b>{this.state.nameMessage}<br/>
                                {this.state.duratioMessage}
                            </b>
                        </font>
                    </div>
                </div>

                <div className="box-footer">
                    <button type="button" className="btn btn-info" onClick={this.addProject.bind(this)} disabled={this.getNumberOfErrors() > 0}>Add</button>
                    <button type="button" className="btn btn-info" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        );
    }
}