import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default class Form extends React.Component{
    
    constructor(){
        super();
        this.state={
            departmentManagers:[]
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/department/getAllDepartmentManagers',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    departmentManagers: data
                })
            }.bind(this)
        })     
    }

    store(cb){
   
        const depManager=this.refs.managersDropdown.options[this.refs.managersDropdown.selectedIndex].value;

        var inputInfo={
            Name: this.refs.name.value,
            OfficeId: this.props.officeId,
            DepartmentManagerId: depManager
        }
         

        $.ajax({
            method: 'POST',
            async: false,
            url: configs.baseUrl + 'api/department/addDepartment',
            data:inputInfo,
            success: function (data) {               
                 cb(); 
                 this.refresh(this.props.officeId);
            }.bind(this)
        })   

              
    }

    refresh(officeId){
         $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/office/getAllDepOffice?officeId=' + officeId+'&pageSize=10&pageNr=1',
            success: function (data) {
                Context.cursor.set("departments",Immutable.fromJS(data));
            }.bind(this)
        })
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