import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import Context from '../../context/Context.js';
import configs from '../helpers/calls'
import * as Controller from '../controller';
import * as Immutable from 'immutable';

const Tr = (props) => {

    return(
        <tr>
            <td>{props.node.get('Name')} </td>
            <td>{props.node.get('Role')}</td>
            <td>{props.node.get('Allocation')}</td>
            <td><Link to="#"> Edit allocation | </Link>
                <Link to="#"> Remove </Link></td>
        </tr>
    )


}

export default class Member extends React.Component{


    constructor(){
        super();
        this.state ={
            members: Context.cursor.get("members"),
            assign:false,
            pageNr:1
        }
    }

    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

     componentDidMount(){
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

    showAddForm(){
        this.setState({
            assign:true
        });
    }

    closeAddForm(){
        this.setState({
            assign: !this.state.assign
        })
    }

    back(){

        if (this.state.pageNr!=1){
            const whereTo=this.state.pageNr-1

            Controller.getEmployeesByProjectId(this.props.routeParams.projectId,whereTo);
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        Controller.getEmployeesByProjectId(this.props.routeParams.projectId,whereTo);

        this.setState({
            pageNr:this.state.pageNr+1
        })
    }


    render(){

        const items = this.state.members.map( (element, index) => {
            return(
                <Tr
                    node = {element}
                    key = {index}
                />
            )

        });

        return(
            <div>
                <h1>{this.props.routeParams.projectName + ' Members'}  </h1>
                <button className="btn btn-xs btn-info" > <span className="glyphicon glyphicon-plus-sign"></span> Assign employee </button>
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

                <div>
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-arrow-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-arrow-right fa-2x" aria-hidden="true"></i>
                    </button>              
                </div>

            </div>
        )
    }



}