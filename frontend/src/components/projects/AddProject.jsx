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
            message : ""
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
        
        this.setState({
            status: e.target.value
        });

    }

    onInputChange(){
        const newProjectName = this.refs.name.value;
        const newProjectDuration = parseInt(this.refs.duration.value);
        
        if(newProjectName === "")
        {
            this.setState({
                message : "Error!!! Project name cannot be empty."
            });
        } else if(newProjectName.length > 100) {
            this.setState({
                message : "Error!!! Project name cannot be longer than 100 characters."
            });
        } else if(isNaN(newProjectDuration)) {
            this.refs.duration.value = "0";
        } else if(newProjectDuration > 120) {
            this.setState({
                message : "Error!!! A project cannot last more than 120 months."
            });
        } else {
            this.setState({
                message : ""
            });
            this.refs.duration.value = newProjectDuration;
        }

    }

    render(){

        const addButton = this.state.message === "" ? (
            <button type="button" className="btn btn-info" onClick={this.addProject.bind(this)}>Add</button>
        ) : (<button type="button" className="btn btn-info" disabled>Add</button>);

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new project</h3>
                </div>
            <form className="form-horizontal">
                <div className="box-body">
                    <label>Name</label>
                    <input type="text" className="form-control" ref="name" placeholder="Project name" autoComplete="off" onChange={this.onInputChange.bind(this)}></input>
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
                    <input type="text" autoComplete="off" className="form-control" ref="duration" placeholder="Duration" onChange={this.onInputChange.bind(this)}></input>
                    <div><font color="red"><b>{this.state.message}</b></font></div>
                </div>

                <div className="box-footer">
                    {addButton}
                    <button type="button" className="btn btn-info" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        );
    }
}