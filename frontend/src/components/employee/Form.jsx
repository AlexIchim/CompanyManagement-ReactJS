import React from 'react';
import AddEmployeeModalTemplate from './AddEmployeeModalTemplate.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default class Form extends React.Component{

    constructor(){
        super();
        this.state={
            jobTypes:["Part time 4 hours", "Part Time 6 hours", "FullTime"],
            positionTypes:["Developer", "ProjectManager","TeamLead", "QA", "BA", "DepartmentManager"]
        }
    }

    componentWillMount(){
           
    }

    componentDidMount(){
        $('#datepicker1').datepicker({});
        $('#datepicker2').datepicker({});
    }

    store(cb){
        var inputInfo = {
            DepartmentId: this.props.departmentId,
            Name: this.refs.name.value,
            Address: this.refs.address.value,
            EmploymentDate: this.refs.employmentDate.value,
            ReleaseDate: this.refs.releaseDate.value,
            JobType: this.refs.jobType.value,
            PositionType: this.refs.positionType.value
        }
         $.ajax({
            method: 'POST',
            url: configs.baseUrl + 'api/employee/addEmployee',
            data:inputInfo,
            success: function (data) {              
                Context.cursor.update('employees',(oldList) => {      
                    return oldList.push( Immutable.fromJS(inputInfo) );
                   
                });
                 cb(); 
                 this.refresh(this.props.departmentId);
            }.bind(this)
        })   
        console.log(3)
    }

    refresh(departmentId){
         $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/employee/getAllDepartmentEmployees?departmentIdId=' + departmentId+'&pageSize=10&pageNr=1',
            success: function (data) {
                Context.cursor.set("employees",Immutable.fromJS(data));
            }.bind(this)
        })
    }

    render(){
        const jobTypes = this.state.jobTypes.map((el,x)=>{
            return (
                <option value={el.Id} key={x} >{el}</option>                         
            )
        });
        const positionTypes = this.state.positionTypes.map((el,x)=>{
            return (
                <option value={el.Id} key={x} >{el}</option>                         
            )
        });
        return (

        <AddEmployeeModalTemplate close={this.props.close} store={this.store.bind(this)}>
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
                <label>employmentDate:</label>

                <div className="input-group date">
                  <div className="input-group-addon">
                    <i className="fa fa-calendar"></i>
                  </div>
                  <input type="text" ref ="employmentDate"className="form-control pull-right" id="datepicker1"/>
                </div>
                
              </div>
            <div className="form-group">
                <label>releaseDate:</label>

                <div className="input-group date">
                  <div className="input-group-addon">
                    <i className="fa fa-calendar"></i>
                  </div>
                  <input type="text" ref ="releaseDate"className="form-control pull-right" id="datepicker2"/>
                </div>
                
              </div>

            <label className="col-sm-4 control-label"> Job Type </label>
            <select ref="jobType" className="selectpicker">
                {jobTypes}                    
            </select>
             <label className="col-sm-4 control-label"> Position Type </label>
       
            <select ref="positionType" className="selectpicker">
                {positionTypes}                    
            </select>

        </AddEmployeeModalTemplate>
        )
    }
}