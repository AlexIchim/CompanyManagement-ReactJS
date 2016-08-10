import React from 'react';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as Controller from '../controller';
import Modal from '../modal/Modal.jsx';

export default class Form extends React.Component{

    constructor(){
        super();
        this.state={
            jobTypes:[],
            positionTypes:[]
        }
    }

    componentWillMount(){
         $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getJobTypes',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    jobTypes: data

                })
            }.bind(this)
        })   
        
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getPositionTypes',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    positionTypes: data
                })
            }.bind(this)
        })          
    }

    componentDidMount(){
        $('#datepicker1').datepicker({});
        $('#datepicker2').datepicker({});
    }

    store(cb){

        const jobType = this.refs.jobType.options[this.refs.jobType.selectedIndex].value
        const positionType = this.refs.positionType.options[this.refs.positionType.selectedIndex].value

        var inputInfo = {
            DepartmentId: this.props.departmentId,
            Name: this.refs.name.value,
            Address: this.refs.address.value,
            EmploymentDate: this.refs.employmentDate.value,
            ReleaseDate: this.refs.releaseDate.value,
            JobType: jobType,
            PositionType: positionType
        }

        console.log(inputInfo)
         $.ajax({
            method: 'POST',
            url: configs.baseUrl + 'api/employee/addEmployee',
            data:inputInfo,
            success: function (data) {              
                 cb(); 
                 this.refresh(this.props.departmentId);
            }.bind(this)
        })   
        
    }

    refresh(departmentId){
         Controller.getAllEmployeesByDepartmentId(departmentId,1)
    }

    render(){
        const jobTypes = this.state.jobTypes.map((el,x)=>{
            return (
                <option value={el.Id} key={x} id={el.Id}  > {el.Description} </option>                         
            )
        });
        const positionTypes = this.state.positionTypes.map((el,x)=>{
            return (
                <option value={el.Id} key={x} id ={el.Id} >{el.Description}</option>                         
            )
        });
         return(

        <Modal title={'Add new employee'} button={'Add'} close={this.props.close} action={this.store.bind(this)}>
      
           <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <input  ref="name" className="form-control" placeholder="Name"/>
                </div>
            </div>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Address </label>
                <div className="col-sm-6">
                    <input  ref="address" className="form-control" placeholder="Address"/>
                </div>
            </div>
            <div className="form-group">
                <label className="col-sm-4 control-label">Employment Date</label>
                <div className="col-sm-6">
                    <div className="input-group date">
                      <div className="input-group-addon">
                        <i className="fa fa-calendar"></i>
                      </div>
                      <input type="text" ref ="employmentDate"className="form-control pull-right" id="datepicker1"/>
                    </div>
                </div>
                
              </div>
            <div className="form-group">
                <label className="col-sm-4 control-label">Release Date</label>
                <div className="col-sm-6">
                    <div className="input-group date">
                      <div className="input-group-addon">
                        <i className="fa fa-calendar"></i>
                      </div>
                      <input type="text" ref ="releaseDate"className="form-control pull-right" id="datepicker2"/>
                    </div>
                </div>
              </div>
            
            <div className="form-group">
             <label className="col-sm-4 control-label"> Job Type </label>
                <div className="col-sm-6">
                    <select ref="jobType" className="selectpicker form-control">
                        {jobTypes}
                    </select>
                </div>
            </div>




            <div className="form-group">
                <label className="col-sm-4 control-label"> Position Type </label>
                 <div className="col-sm-6">
                <select ref="positionType" className="selectpicker form-control">
                          {positionTypes}           
                </select>
                </div>
            </div>
           

            </Modal>


        
        )
    }
}