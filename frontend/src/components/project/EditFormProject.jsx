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
            },
            statusDescriptions:[]
        }
    }


    componentWillMount(){
     $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/project/getProjectStatusDescriptions',
            success: function (data) {
                this.setState({
                    statusDescriptions: data
                })
            }.bind(this)
        })     
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
            EmployeesNumber: this.props.element.get("EmployeesNumber")
        }

        this.setState({
            project: Immutable.fromJS(newO)
        })         
    }

    

    edit(cb){
        const status=this.refs.status.options[this.refs.status.selectedIndex].id;

        const newProject={
            Id: this.state.project.get('Id'),
            Name:this.state.project.get('Name'),
            Status:status,
            Duration:this.state.project.get('Duration'),
            DepartmentId:this.props.departmentId,
            EmployeesNumber: this.state.project.get("EmployeesNumber")
            
        }
        const np= this.state.project.set("Status",this.refs.status.options[this.refs.status.selectedIndex].value);
       
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
        const statusDescriptions=this.state.statusDescriptions.map((el, x) => {
            return (
                <option value={el.Description} key={x} id={el.Id} >{el.Description}</option>                         
            )
        });
        
        return(

        <Modal title={'Edit project'} button={'Edit'} close={this.props.close} action={this.edit.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <input  ref="name" className="form-control" placeholder="Name" value={this.state.project.get('Name')} onChange={this.changeData.bind(this)}/>
                </div>
            </div>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Duration </label>
                <div className="col-sm-6">
                    <input  ref="duration" className="form-control" placeholder="Duration" value={this.state.project.get('Duration')} onChange={this.changeData.bind(this)}/>
                </div>
            </div>
            <div className="form-group">
                <label className="col-sm-4 control-label">Status </label>
                <div className="col-sm-6">
                    <select className="selectpicker form-control" ref="status" >
                        {statusDescriptions}
                    </select>
                </div>
            </div>
       
        </Modal>
        )
    }
    
    
}