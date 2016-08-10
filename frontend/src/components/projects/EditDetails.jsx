import React from 'react';
import {hideFunc} from '../layout/ModalTemplate';
import Project from './Project';
import Projects from './Projects';
import config from '../../api/config';
import updateProject from '../../api/controller/updateProject';

export default class EditDetails extends React.Component {

    constructor(){
        super();
        this.state = {
            project : {},
            message : "",
        }
    }

    componentWillMount(){
        const project = this.props.project;
        let copy = {}
        for(let key in project){
            if(project.hasOwnProperty(key)){
                copy[key] = project[key];
            }
        }

        this.setState({
            project : copy
        });
    }

    updateProject(){
            updateProject(
                this.state.project,
                true,
                this.props.updateFunc
            );
    }

    changeOption(e){      
        let newProject = this.state.project; 
        newProject['status'] = e.target.value;
        this.setState({
            project: newProject
        });
    }

    onInputChange(){
        var newProjectName = this.refs.name.value;
        var newProjectDuration = parseInt(this.refs.duration.value);
        const newProject = this.state.project;

        if(newProjectName === ""){
            this.setState({
                message : "Error!!! Project name cannot be empty."
            });
        } else if(newProjectName.length > 100) {
            this.setState({
                message : "Error!!! Project name cannot contain more than 100 characters."
            });
        } else if(isNaN(newProjectDuration)) {
            newProjectDuration= 0;
        } else if(newProjectDuration > 120) {
            this.setState({
                message : "Error!!! A project cannot last more than 120 months."
            });
        } else {
            this.setState({
                message : ""
            });
        }
        console.log(newProjectName, ' ', newProjectDuration);
        this.refs.name.value = newProjectName;
        newProject.name = this.refs.name.value;
        newProject.duration = newProjectDuration;
        
        this.setState({
            project : newProject
        });
    }

    render(){
        let project = this.state.project;
        const onEdit = this.props.onEdit;
        const projectName = this.state.project.name;

        const saveButton = this.state.message === "" ? (
            <button type="button" className="btn btn-info" onClick={this.updateProject.bind(this)}>Save</button>
        ) : (<button type="button" className="btn btn-info" disabled>Save</button>);

        return(
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Update {projectName}</h3>
                </div>
            <form className="form-horizontal">
                <div className="box-body">
                    <div className="form-group">
                        <div className="col-md-3">
                            <label>New name</label>
                        </div>
                        <div className="col-md-9">
                            <input type="text" className="form-control" ref="name" value={project.name} onChange={this.onInputChange.bind(this)} autoComplete="off"></input>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-md-3">
                            <label>New status</label>
                         </div>
                        <div className="col-md-9">
                            <select className="form-control" id="choice" onChange={this.changeOption.bind(this)}>
                                <option value="Not started">Not started</option>
                                <option value="In progress">In progress</option>
                                <option value="On hold">On hold</option>
                                <option value="Done">Done</option>
                            </select>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-md-3">
                            <label>New duration</label>
                        </div>
                        <div className="col-md-9">
                            <input placeholder="variable" type="text" className="form-control" ref="duration" value={project.duration} onChange={this.onInputChange.bind(this)}  autoComplete="off"></input>
                        </div>
                    </div>
                    <div><font color="red"><b>{this.state.message}</b></font></div>
                </div>

                <div className="box-footer">
                    {saveButton}
                    <button type="button" className="btn btn-info" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        );
    }
}