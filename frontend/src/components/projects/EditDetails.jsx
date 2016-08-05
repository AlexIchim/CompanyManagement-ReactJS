import React from 'react';
import {hideFunc} from '../layout/ModalTemplate';
import Project from './Project';
import Projects from './Projects';
import config from '../../api/config';
import editProject from '../api/controller/editProject';

export default class EditDetails extends React.Component {

    constructor(){
        super();
        this.state = {
            project : null
        }
    }

    componentWillMount(){
        this.setState({
            project : this.props.project
        })
    }

    editProject(){ 
        editProject(
            this.state.project,
            true,
            this.props.updateFunc
        );
    }

    onChangeFunc(e){
      
        let project = this.state.project; 
        
        project[e.target.name] = e.target.value;
        this.setState({
            project : project
        })
    }

    render(){
        var project = this.state.project;
        const onEdit = this.props.onEdit;
       
        console.log('props',this.props);
        console.log('stateprject',project);

        return(
            <div className = "box info-box">
                <div className = "box-header with-border">
                    <h3 className = "box-title">Update {project.name}</h3>
                </div>
            <form className = "form-horizontal">
                <div className = "box-body">
                    <label>New name</label>
                    <input type="text" className="form-control"   name="name" value={project.name} onChange={this.onChangeFunc.bind(this)}></input>
                    <label>New status</label>
                    <input type="text" className="form-control" name="status" value={project.status} onChange={this.onChangeFunc.bind(this)}></input>
                    <label>New duration</label>
                    <input placeholder="variable" type="text" className="form-control" ref="duration" name="duration" value={project.duration} onChange={this.onChangeFunc.bind(this)}></input>
                </div>

                <div className = "box-footer">
                    <button className="btn btn-default" onClick={this.editProject.bind(this)}>Edit</button>
                    <button className="btn btn-default" onClick={this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        )
    }
}