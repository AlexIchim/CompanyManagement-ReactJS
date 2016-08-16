import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import Context from '../../context/Context.js';
import configs from '../helpers/calls'
import * as Controller from '../controller';
import * as Immutable from 'immutable';
import MemberItem from './MemberItem.jsx'
import FormAssign from './FormAssign.jsx'


export default class Member extends React.Component{


    constructor(){
        super();
        this.state ={
            members: Context.cursor.get("members"),
            assign:false,
            positionTypes:[],
            pageNr:1,
            pageSize:3,
            nrOfPages:null,
            filter:null
        }
    }

    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));

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

        Controller.getAllOffices();
    }

     componentDidMount(){
         this.getAllEmployeeOnProject(this.state.pageNr,null);
         this.setNumberOfPages(null);
    }

    componentWillUnmount () {
        this.subscription.dispose(); 
    }

     onContextChange(cursor){
        this.setState({
            members: cursor.get("members")         
        });

    }

     getAllEmployeeOnProject(pageNr,position){

        Controller.getEmployeesByProjectId(this.props.routeParams.projectId,position,pageNr);
    
    }


    setNumberOfPages(position){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/project/getEmployeesByProjectId?projectId='+ this.props.routeParams.projectId+'&ptype=' + position +'&pageSize=nul&pageNr=null',
            success: function (data) {
                if (data.length==0){
                    this.setState(
                        {
                            nrOfPages: 1
                        }
                    )

                }else{
                    this.setState(
                        {
                            nrOfPages: Math.ceil( data.length / this.state.pageSize)
                        }
                    )

                }
            }.bind(this)
        })
    }

    showAssignForm(){
        this.setState({
            assign:true
        });
    }

    closeAssignForm(){
        this.setState({
            assign: !this.state.assign
        })
    }

    back(){

        if (this.state.pageNr > 1){
            const whereTo=this.state.pageNr-1

            this.getAllEmployeeOnProject(whereTo,this.state.filter);
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        if(whereTo <= this.state.nrOfPages) {
            
            this.getAllEmployeeOnProject(whereTo, this.state.filter);

            this.setState({
                pageNr:this.state.pageNr+1
        })
        }

       
    }

    last(){
        this.setNumberOfPages(this.state.filter);


        this.getAllEmployeeOnProject(this.state.nrOfPages, this.state.filter);

        this.setState({
            pageNr: this.state.nrOfPages
        })
    }

    first(){

            if (this.state.pageNr!=1){
        this.getAllEmployeeOnProject(1, this.state.filter);

        this.setState({
            pageNr:1
        })

            }


    }

    onDropDownChange(){
        const positionTypes=this.refs.positionTypes.options[this.refs.positionTypes.selectedIndex].id;
        const pageNr = 1;
       
        this.setState({
            filter: positionTypes,
            pageNr:pageNr
        })

        this.setNumberOfPages(positionTypes);
        
        this.getAllEmployeeOnProject(pageNr,positionTypes);
    }

    setPageNr(){
        
        this.setState({
            pageNr:1,
            filter:null,
        })

        this.setNumberOfPages(null);
    }

    render(){

        const items = this.state.members.map( (element, index) => {
            return(
                <MemberItem
                    node = {element}
                    key = {index}
                    projectId = {this.props.routeParams.projectId}
                    setPageNr= {this.setPageNr.bind(this)}
                />
            )

        });

          const positionTypes=this.state.positionTypes.map((el, x) => {
            return (
                <option value={el} key={x} id={el.Id} >{el.Description}</option>                         
            )
        });


        const assignModal = this.state.assign ? <FormAssign setPageNr={this.setPageNr.bind(this)} projectId={this.props.routeParams.projectId} officeId={this.props.routeParams.officeId} show = {this.state.assign} close={this.closeAssignForm.bind(this)} /> : '';

        return(
            <div>

                {assignModal}

                <h1>{this.props.routeParams.projectName + ' Members'}  </h1>

                <div className="form-group">
                    <button className="btn btn-md btn-info btn-add-custom" onClick={this.showAssignForm.bind(this)} > <span className="glyphicon glyphicon-plus-sign"></span> Assign employee </button>
                    <div className="col-sm-2 dropdown-custom">
                        <label className="control-label"> Position </label>
                        <select className="form-control" ref="positionTypes" onChange={this.onDropDownChange.bind(this)}>
                            <option value=""> None </option>
                            {positionTypes}
                        </select>
                    </div>
                </div>



                <table className="table table-condensed" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Name</th>
                        <th className="col-md-2">Role</th>
                        <th className="col-md-2">Allocation</th>
                        <th className="col-md-2">Actions</th>
                    </tr>
                    </thead>
                    <tbody>

                    {items}

                    </tbody>
                </table>

                <div className="btn-wrapper">
                    <button className="rightArrow" onClick={this.first.bind(this)}>
                        <i className="fa fa-angle-double-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                        <i className="fa fa-angle-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <label className="to-right">{this.state.pageNr} </label>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                        <i className="fa fa-angle-right fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.last.bind(this)}>
                        <i className="fa fa-angle-double-right fa-2x" aria-hidden="true"></i>
                    </button>
                </div>

                

            </div>
        )
    }



}