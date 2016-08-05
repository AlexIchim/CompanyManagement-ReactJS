import React from 'react';
import {hideFunc} from '../layout/ModalTemplate';
import Project from './Project';
import Projects from './Projects';
import config from '../../api/config';
import addProject from '../api/controller/addProject';

export default class AddProject extends React.Component{

    addProject(){

        let projectObject = {
            name : this.refs.name.value,
            status : this.refs.status.value,
            duration : this.refs.duration.value,
            departmentId : this.props.departmentId
        }

        addProject(
            projectObject,
            false,
            this.props.saveFunc
        )
    }

   

    render(){
     
        return (
            <div className = "box info-box">
                <div className = "box-header with-border">
                    <h3 className = "box-title">Add new project</h3>
                </div>
            <form className = "form-horizontal">
                <div className = "box-body">
                    <label>Name</label>
                    <input type = "text" className = "form-control" ref = "name" placeholder = "Project name"></input>
                    <label>Status</label>
                    <input type = "text" className = "form-control" ref = "status" placeholder = "Project status"></input>
                    <label>Duration</label>
                    <input type = "text" className = "form-control" ref = "duration" placeholder = "Project duration"></input>
                </div>

                <div className = "box-footer">
                    <button className = "btn btn-default" onClick = {this.addProject.bind(this)}>Add</button>
                    <button className = "btn btn-default" onClick = {this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        );
    }
}