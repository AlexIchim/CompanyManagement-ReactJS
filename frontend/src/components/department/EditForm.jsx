import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as $ from 'jquery';

export default class EditForm extends React.Component{
    
    constructor(){
        super();
        this.state={
            departmentManagers:[],
            department:{
            }
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/department/getAllDepartmentManagers',
            success: function (data) {
                this.setState({
                    departmentManagers: data,
                    department: this.props.element
                })
            }.bind(this)
        })     
    }

    
    changeData(){

        const name = this.refs.name.value;
       
        const newO = {
            Id: this.state.department.get('Id'),
            Name:name,
            DepartmentManager:this.state.department.get('DepartmentManager'),
            NbrOfEmployees:this.state.department.get('NbrOfEmployees'),
            NbrOfProjects: this.state.department.get('NbrOfProjects')
        }

        this.setState({
            department: Immutable.fromJS(newO)
        })         
    }

    edit(cb){
   
        const depManagerId=this.refs.managersDropdown.options[this.refs.managersDropdown.selectedIndex].value;
        const depManagerName=this.refs.managersDropdown.options[this.refs.managersDropdown.selectedIndex].text;

        const newDep={
            Id: this.state.department.get('Id'),
            Name:this.state.department.get('Name'),
            OfficeId:this.props.officeId,
            DepartmentManagerId:depManagerId,
            NbrOfEmployees:this.state.department.get('NbrOfEmployees'),
            NbrOfProjects: this.state.department.get('NbrOfProjects')
        }

        const np= this.state.department.set('DepartmentManager',depManagerName);

          
        $.ajax({
            method: 'PUT',
            async: false,
            url: configs.baseUrl + 'api/department/updateDepartment',
            data:newDep,
            success: function (data) { 
                 const index= Context.cursor.get('departments').indexOf(this.props.element)
                   Context.cursor.get('departments').update( index,  oldInstance => {
                        oldInstance=np
                        return oldInstance;
                    });              
                 
                 cb(); 
                 
            }.bind(this)
        })   

              
    }

  
    render(){

        const departmentManagers=this.state.departmentManagers.map((el, x) => {
            return (
                <option  value={el.Id} key={x} >{el.Name}</option>                         
            )
        });

        return(

        <Modal title={'Edit department'} button={'Edit'} close={this.props.close} action={this.edit.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <input  ref="name" className="form-control" placeholder="Name" value={this.state.department.get('Name')} onChange={this.changeData.bind(this)}/>
                </div>
            </div>
           
           <label className="col-sm-4 control-label"> Department manager </label>
       
            <select className="selectpicker" ref="managersDropdown" >
                {departmentManagers}                    
            </select>
     
       
        </Modal>
        )
    }
    
    
}