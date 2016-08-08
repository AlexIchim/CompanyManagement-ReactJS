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
            //status : "",
            message : ""
        }
    }

    componentWillMount(){
        this.setState({
            project : this.props.project
        })
    }

    updateProject(){
        if(this.state.message === "")
        {
            updateProject(
                this.state.project,
                true,
                this.props.updateFunc
            );
        }
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
        let newProject = this.state.project; 
        newProject[e.target.name] = parseInt(e.target.value);
        this.setState({
            project : newProject
        });

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
        var project = this.state.project;
        const onEdit = this.props.onEdit;

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
                    <input placeholder="variable" type="text" className="form-control" ref="duration" name="duration" value={project.duration} onChange={this.onChangeFunction.bind(this)}></input>
                    <div>{this.state.message}</div>
                </div>

                <div className="box-footer">
                    <button className="btn btn-default" onClick={this.updateProject.bind(this)}>Edit</button>
                    <button className="btn btn-default" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        )
    }
}