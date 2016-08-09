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
            project : null,
            message : ""
        }
    }

    componentWillMount(){
        this.setState({
            project : this.props.project
        })
    }

    updateProject(){
        
            updateProject(
                this.state.project,
                true,
                this.props.updateFunc
            );
        
    }

    onChangeFunc(e){        
        let newProject = this.state.project; 
        newProject[e.target.name] = e.target.value;
        this.setState({
            project : newProject
        })
    }

    changeOption(e){      
        
        let newProject = this.state.project; 
        newProject['status'] = e.target.value;
        this.setState({
            project: newProject
        });

    }

    onChangeFunction(e){
        const val = parseInt(e.target.value);
        const newProject = this.props.project;
        var newValue;

        if(isNaN(val)){
            newValue = "0";
        } else {
            newValue = val.toString();
        }

        if(val > 120)
        {
            this.setState({
                message : "Error!!! A project cannot last more than 120 months."
            })
        } else {
            this.setState({
                message : ""
            })
        }

        e.target.value = newValue;
        newProject['duration'] = parseInt(newValue);
        
        this.setState({
            project: newProject
        });
    }

    render(){
        var project = this.state.project;
        const onEdit = this.props.onEdit;

        const saveButton = this.state.message === "" ? (
            <button type="button" className="btn btn-info" onClick={this.updateProject.bind(this)}>Save</button>
        ) : (<button className="btn btn-info" disabled>Save</button>);

        return(
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Update {project.name}</h3>
                </div>
            <form className="form-horizontal">
                <div className="box-body">
                    <label>New name</label>
                    <input type="text" className="form-control" name="name" value={project.name} onChange={this.onChangeFunc.bind(this)}></input>
                    <label>New status</label>
                    <div>
                        <select className="form-control" id="choice" onChange={this.changeOption.bind(this)}>
                            <option value="Not started">Not started</option>
                            <option value="In progress">In progress</option>
                            <option value="On hold">On hold</option>
                            <option value="Done">Done</option>
                        </select>
                    </div>
                    <label>New duration</label>
                    <input placeholder="variable" type="text" className="form-control" ref="duration" name="duration" value={project.duration} onChange={this.onChangeFunction.bind(this)}  autoComplete="off"></input>
                    <div><b>{this.state.message}</b></div>
                </div>

                <div className="box-footer">
                    {saveButton}
                    <button className="btn btn-info" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        )
    }
}