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

    onChangeFunction(e){
        const val = parseInt(e.target.value);
        
        if(isNaN(val))
        {
            e.target.value = "";
        } else {
            if(val > 120)
            {
                this.setState({
                    message : "Error!!! A project cannot last more than 120 months."
                });
            } else {
                this.setState({
                    message : ""
                })
                e.target.value = val.toString();
            }
        }
        
    }

    render(){

        const addButton = this.state.message === "" ? (
            <button className="btn btn-info" onClick={this.addProject.bind(this)}>Add</button>
        ) : (<button className="btn btn-info" disabled>Add</button>);

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new project</h3>
                </div>
            <form className="form-horizontal">
                <div className="box-body">
                    <label>Name</label>
                    <input type="text" className="form-control" ref="name" placeholder="Project name"></input>
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
                    <input type="text" autoComplete="off" className="form-control" ref="duration" placeholder="Duration" onChange={this.onChangeFunction.bind(this)}></input>
                    <div>{this.state.message}</div>
                </div>

                <div className="box-footer">
                    {addButton}
                    <button className="btn btn-info" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        );
    }
}