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
            statusDescriptions:[]
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

    store(cb){
        const status=this.refs.status.options[this.refs.status.selectedIndex].id;

        var inputInfo={
            Name: this.refs.name.value,
            Status: status,
            Duration: this.refs.duration.value,
            DepartmentId: this.props.departmentId,
        }
        ValidateProject(inputInfo)
        $.ajax({
            method: 'POST',
            async: false,
            url: configs.baseUrl + 'api/project/add',
            data:inputInfo,
            success: function (data) {
                cb();
                this.refresh(this.props.departmentId);
            }.bind(this)
        })
    }

    refresh(departmentId){
        this.props.setPageNr();
        Controller.getAllDepProjects(departmentId,{},1);             
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
                    <input  ref="name" className="form-control" placeholder="Name"/>
                </div>
            </div>
               <div className="form-group">
                <label className="col-sm-4 control-label"> Duration </label>
                <div className="col-sm-6">
                    <input  ref="duration" className="form-control" placeholder="Project Duration"/>
                </div>
                   <label className="col-sm-1 control-label"> months </label>
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