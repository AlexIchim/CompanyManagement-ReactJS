import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'

const Tr = (props) => {

    return(
        <tr>
            <td>{props.node.Name} </td>
            <td>{props.node.Role}</td>
            <td>{props.node.Allocation}</td>
            <td><Link to="#"> Edit allocation | </Link>
                <Link to="#"> Remove </Link></td>
        </tr>
    )


}

export default class Member extends React.Component{


    constructor(){
        super();
        this.state ={
            members: []
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/project/getEmployeeByProjectId?projectId=' + this.props.routeParams.projectId,
            success: function(data){
                this.setState({
                    members: data
                })

            }.bind(this)
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
            </div>
        )
    }



}