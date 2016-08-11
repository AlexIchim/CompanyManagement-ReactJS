import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import ValidateProject from '../validators/ValidateProject.js';

export default class EditFormProject extends React.Component{
    
    constructor(){
        super();
        this.state={
            project:{
            },
            statusDescriptions:[],
            errors:{
                NameErrors:[],
                DurationErrors:[]
            }
        }
    }


    componentWillMount(){
     $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/project/getProjectStatusDescriptions',
            success: function (data) {
                console.log("status",data, this);
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

    onChangeName()
    {   
        const errors = ValidateProject.validateName(this.refs.name.value)
        this.state.errors.NameErrors = errors
       
         this.setState({
             errors: this.state.errors
         })
    }

    onChangeDuration()
    {   
        const errors = ValidateProject.validateDuration(this.refs.duration.value)
        this.state.errors.DurationErrors = errors
       
         this.setState({
             errors: this.state.errors
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
                    {this.state.errors.NameErrors}
                    <input  ref="name" className="form-control" placeholder="Name" value={this.state.project.get('Name')} onChange={this.changeData.bind(this)} onKeyUp={this.onChangeName.bind(this)}/>
                </div>
                
                <label className="col-sm-4 control-label">Status </label>     
                <select className="selectpicker" ref="status" >
                    {statusDescriptions}                    
                </select>
                <label className="col-sm-4 control-label"> Duration </label>
                <div className="col-sm-6">
                    {this.state.errors.DurationErrors}
                    <input  ref="duration" className="form-control" placeholder="Duration" value={this.state.project.get('Duration')} onChange={this.changeData.bind(this)} onKeyUp={this.onChangeDuration.bind(this)}/>
                </div>

            </div>
       
        </Modal>
        )
    }
    
    
}