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
            pageNr:1,
            pageSize:3,
            positionTypes:[],
            nrOfPages:null
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
    }

     componentDidMount(){
         console.log(this.props.routeParams.projectId);
         Controller.getEmployeesByProjectId(this.props.routeParams.projectId,this.state.pageNr);
    }

    componentWillUnmount () {
        this.subscription.dispose(); 
    }

     onContextChange(cursor){
        this.setState({
            members: cursor.get("members")         
        });

    }

     getAllEmployeeOnProject(pageNr){

        Controller.getEmployeesByProjectId(this.props.routeParams.projectId,pageNr);

        this.setNumberOfPages();
    }


    setNumberOfPages(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/project/getEmployeesByProjectId?projectId='+ this.props.routeParams.projectId +'&pageSize=nul&pageNr=null',
            success: function (data) {
                console.log(data)
                this.setState(
                    {
                        nrOfPages: data.length / this.state.pageSize + 1
                    }
                )
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

            this.getAllEmployeeOnProject(whereTo);
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

         this.setNumberOfPages();

        if(whereTo < this.state.nrOfPages) {
            
            this.getAllEmployeeOnProject(whereTo);

            this.setState({
                pageNr:this.state.pageNr+1
        })
        }

       
    }


    render(){

        const items = this.state.members.map( (element, index) => {
            return(
                <MemberItem
                    node = {element}
                    key = {index}
                    projectId = {this.props.routeParams.projectId}
                />
            )

        });

          const positionTypes=this.state.positionTypes.map((el, x) => {
            return (
                <option value={el} key={x} >{el.Description}</option>                         
            )
        });


        const assignModal = this.state.assign ? <FormAssign projectId={this.props.routeParams.projectId} officeId={this.props.routeParams.officeId} show = {this.state.assign} close={this.closeAssignForm.bind(this)} /> : '';

        return(
            <div>

                {assignModal}

                <h1>{this.props.routeParams.projectName + ' Members'}  </h1>
                <button className="btn btn-xs btn-info" onClick={this.showAssignForm.bind(this)} > <span className="glyphicon glyphicon-plus-sign"></span> Assign employee </button>

                <select className="selectpicker" ref="positionTypes" >
                    {positionTypes}                    
                </select>

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