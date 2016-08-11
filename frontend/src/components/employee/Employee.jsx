import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import Form from './Form';
import * as Controller from '../controller';
import EmployeeItem from './EmployeeItem.jsx'

export default class Employee extends React.Component{


    constructor(){
        super();
        this.state ={
            add: false,
            employees: Context.cursor.get("employees"),
            pageNr: 1,
            positionTypes:[],
            jobTypes:[],
            allocation: [0,10,20,30,40,50,60,70,80,90,100, 'None']
        }
    }

    componentWillMount(){
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

        Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,"",null,{},{},this.state.pageNr);
       this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

      componentDidMount(){
          Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,"",null,{},{},this.state.pageNr);
     }

    componentWillUnmount () {
        this.subscription.dispose(); 
    }

    onContextChange(cursor){
        this.setState({
            employees: cursor.get('employees')
        })
    }

    showAddForm(){
        this.setState({
            add:true
        });
    }

    closeAddForm(){
        this.setState({
            add: !this.state.add
        })
    }

    back(){

        if (this.state.pageNr!=1){
            const whereTo=this.state.pageNr-1

            Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,"",null,{},{},whereTo)
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,"",null,{},{},whereTo)

        this.setState({
            pageNr:this.state.pageNr+1
        })
    }

    onDropDownChange(){
        
        let ptype=this.refs.positionTypes.options[this.refs.positionTypes.selectedIndex].value;
            if (ptype == 'None')
                ptype = {}

        let jtype=this.refs.jobTypes.options[this.refs.jobTypes.selectedIndex].value;
            if (jtype == 'None')
                jtype={}

        let allocation = this.refs.allocation.options[this.refs.allocation.selectedIndex].value;
            if (allocation == 'None')
                allocation = null

        let employeeName = this.refs.search.value;
        const pageNr = 1;

        Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,employeeName,allocation,ptype,jtype,this.state.pageNr)
    }

    onSearchChange(){
        let ptype=this.refs.positionTypes.options[this.refs.positionTypes.selectedIndex].value;
            if (ptype == 'None')
                ptype = {}

        let jtype=this.refs.jobTypes.options[this.refs.jobTypes.selectedIndex].value;
            if (jtype == 'None')
                jtype={}

        let allocation = this.refs.allocation.options[this.refs.allocation.selectedIndex].value;
            if (allocation == 'None')
                allocation = null

        const pageNr = 1;
        const employeeName = this.refs.search.value;      
        Controller.getAllEmployeesByDepartmentId(this.props.routeParams.departmentId,employeeName,allocation,ptype,jtype,this.state.pageNr)
    }

render(){
    let positionTypes=this.state.positionTypes.map((el, x) => {
            return (
                <option value={el.Id} key={x} >{el.Description}</option>                         
            )
        });

    const jobTypes=this.state.jobTypes.map((el, x) => {
            return (
                <option value={el.Id} key={x} >{el.Description}</option>                         
            )
        });

    const allocation = this.state.allocation.map((el,x) =>{
        return(
            <option value={el} key={x}> {el} </option>
        )    
    }
    )

    
    
    const items = this.state.employees.map( (element, index) => {
        return(
            <EmployeeItem
                node = {element}
                key = {index}
                departmentId = {this.props.routeParams.departmentId}

            />
        )


    });
 
    
   const addModal = this.state.add ? <Form departmentId={this.props.routeParams.departmentId} show={this.state.add} close ={this.closeAddForm.bind(this)} /> : ""
   
    return(
        <div>
            
            <button className="btn btn-md btn-info" onClick={this.showAddForm.bind(this)}> <span className="glyphicon glyphicon-plus-sign"></span> Add new employee </button>

         <div>
            <div className="row">
                <div className="col-sm-6 pull-right form-group">
                    <div className="col-sm-4">
                        <label className="control-label"> Job Type </label>
                        <select className="form-control" ref="jobTypes" onChange={this.onDropDownChange.bind(this)}>
                            <option value=""> None </option>
                            {jobTypes}
                        </select>
                    </div>

                    <div className="col-sm-4">
                        <label className="control-label"> Position </label>
                        <select className="form-control" ref="positionTypes" onChange={this.onDropDownChange.bind(this)}>
                            <option value=""> None </option>
                            {positionTypes}
                        </select>
                    </div>

                    <div className="col-sm-4">
                        <label className="control-label"> Allocation </label>
                        <select className="form-control" ref="allocation" onChange={this.onDropDownChange.bind(this)}>
                            <option value=""> None </option>
                            {allocation}
                        </select>
                    </div>

                </div>

                <div className="col-sm-2 pull-left form-group">
                        <label />
                        <input type="search" ref="search" className="form-control" placeholder="Search employee" onChange={this.onSearchChange.bind(this)}/>
                </div>
            </div>
        </div>

            {addModal}

                <table className="table table-striped table-custom">
                    <thead>
                    <tr>
                        <th className="col-md-2">Name</th>
                        <th className="col-md-2">Address</th>
                        <th className="col-md-2">Employment Date</th>
                        <th className="col-md-2">Termination Date</th>
                        <th className="col-md-2">Job Type</th>
                        <th className="col-md-2">Position</th>
                        <th className="col-md-2">Allocation</th>
                        <th className="col-md-2">Actions </th>
                    </tr>
                    </thead>
                    <tbody>
                        {items}
                    </tbody>
                </table>

                <div className="btn-wrapper">
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-arrow-left fa-1x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-arrow-right fa-1x" aria-hidden="true"></i>
                    </button>
                </div>
            
        </div>
    )
}
    
    
    
}