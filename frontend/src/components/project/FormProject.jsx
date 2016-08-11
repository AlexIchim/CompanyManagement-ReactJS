import React from 'react';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import Modal from '../modal/Modal.jsx';
import * as Controller from '../controller';
import ValidateProject from '../validators/ValidateProject.js';

export default class Form extends React.Component{

    constructor(){
        super();
        this.state={
            statusDescriptions:[],
            errors:{
                NameErrors:[],
                DurationErrors:[]
            }
        }
    };

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
    }

    checkErrors()
    {
        if (this.state.errors.NameErrors.length == 0 && this.state.errors.DurationErrors.length == 0)
            return true
        return false
    }

    store(cb){
        if (this.checkErrors() == true)
        {
        const status=this.refs.status.options[this.refs.status.selectedIndex].id;

        var inputInfo={
            Name: this.refs.name.value,
            Status: status,
            Duration: this.refs.duration.value,
            DepartmentId: this.props.departmentId,
        }
        
        $.ajax({
            method: 'POST',
            async: false,
            url: configs.baseUrl + 'api/project/add',
            data:inputInfo,
            success: function (data) {
                if (data.Success == true)
                {
                    cb();
                     this.refresh(this.props.departmentId);
                }
                else
                    alert("Invalid input!")
            }.bind(this)
        })}
        else{
            alert("Invalid input!")
        }
    }

    refresh(departmentId){
        this.props.setPageNr();
        Controller.getAllDepProjects(departmentId,{},1);             
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
                <option value={el} key={x} id={el.Id} >{el.Description}</option>                         
            )
        });

        return(

           <Modal title={'Add new project'} button={'Add'} close={this.props.close} action={this.store.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <div className="col-sm-10 red">
                        {this.state.errors.NameErrors}'
                    </div>'
                    <input  ref="name" className="form-control" placeholder="Name" onKeyUp={this.onChangeName.bind(this)}/>
                </div>
            </div>
               <div className="form-group">
                <label className="col-sm-4 control-label"> Duration </label>
                <div className="col-sm-6">
                    <div className="col-sm-10 red">
                        {this.state.errors.DurationErrors}
                    </div>
                        <input  ref="duration" className="form-control" placeholder="Project Duration" onKeyUp={this.onChangeDuration.bind(this)}/>
                </div>
            </div>
               <div className="form-group">
                   <label className="col-sm-4 control-label"> Status </label>
                   <div className="col-sm-6">
                       <select ref="status" className="selectpicker form-control">
                           {statusDescriptions}
                       </select>
                   </div>
               </div>


     
       
        </Modal>
        )
    }
}