import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default class EditFormProject extends React.Component{
    
    constructor(){
        super();
        this.state={
            project:{
            }
        }
    }


    componentWillMount(){
     this.setState({
            project: this.props.element
        })
    }
    changeData(){

        const name = this.refs.name.value;
        const status = this.refs.status.value;
        const duration = this.refs.duration.value;
        const newO = {
            Id: this.state.project.get('Id'),
            Name:name,
            Status:status,
            Duration: duration,
            DepartmentId: this.state.project.get('DepartmentId')
        }

        this.setState({
            project: Immutable.fromJS(newO)
        })         
    }

    

    edit(cb){
        const newProject={
            Id: this.state.project.get('Id'),
            Name:this.state.project.get('Name'),
            Status:this.state.project.get('Status'),
            Duration:this.state.project.get('Duration'),
            DepartmentId:this.props.departmentId,
            
        }
        const np= this.state.project;
        $.ajax({
            method: 'PUT',
            async: false,
            url: configs.baseUrl + 'api/project/updateProject',
            data:newProject,
            success: function (data) { 
                 const index= Context.cursor.get('projects').indexOf(this.props.element)
                   Context.cursor.get('projects').update( index,  oldInstance => {
                       oldInstance=np
                       return oldInstance;
                    });              
                 
                 cb(); 
                 
            }.bind(this)
        })   

              
    }

  
    render(){
        return(

        <Modal title={'Edit project'} button={'Edit'} close={this.props.close} action={this.edit.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <input  ref="name" className="form-control" placeholder="Name" value={this.state.project.get('Name')} onChange={this.changeData.bind(this)}/>
                </div>
                <label className="col-sm-4 control-label"> Status </label>
                <div className="col-sm-6">
                    <input  ref="status" className="form-control" placeholder="Status" value={this.state.project.get('Status')} onChange={this.changeData.bind(this)}/>
                </div>
                <label className="col-sm-4 control-label"> Duration </label>
                <div className="col-sm-6">
                    <input  ref="duration" className="form-control" placeholder="Duration" value={this.state.project.get('Duration')} onChange={this.changeData.bind(this)}/>
                </div>

            </div>
       
        </Modal>
        )
    }
    
    
}