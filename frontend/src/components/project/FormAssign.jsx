import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';


export default class Form extends React.Component{
    
    constructor(){
        super();
        this.state={
            
        }
    }

    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

    store(cb){
   
        const depManager=this.refs.managersDropdown.options[this.refs.managersDropdown.selectedIndex].value;

        var inputInfo={
            EmployeeId: this.refs.employeeId.value,
            ProjectId: this.props.projectId,
            Allocation: 
        }
         

        $.ajax({
            method: 'POST',
            async: false,
            url: configs.baseUrl + 'api/employee/addEmployeeToProject',
            data:inputInfo,
            success: function (data) {               
                 cb(); 
                 this.refresh(this.props.officeId);
            }.bind(this)
        })   

              
    }

    refresh(officeId){
         Controller.getAllDepOffice(this.props.routeParams.officeId,1);
    }
    
    render(){
        const departmentManagers=this.state.departmentManagers.map((el, x) => {
            return (
                <option value={el.Id} key={x} >{el.Name}</option>                         
            )
        });

        return(

        <Modal title={'Add new department'} button={'Add'} close={this.props.close} action={this.store.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <input  ref="name" className="form-control" placeholder="Name"/>
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