import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'

const Tr = (props) => {

    const linkMembers = "department/project/" + props.node.Id  + '/' + props.node.Name + "/members";

    return(
        <tr>
            <td>{props.node.Name} </td>
            <td>{props.node.EmployeesNumber}</td>
            <td>{props.node.Duration}</td>
            <td>{props.node.Status}</td>
            <td><Link to=""> Edit | </Link>
                <Link to={linkMembers}> View Members | </Link>
                <Link to="#"> Remove </Link></td>
        </tr>
    )


}

export default class Project extends React.Component{


    constructor(){
        super();
        this.state ={
            proj: []
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/project/getAllDepartmentProjects?depId=' + this.props.routeParams.departmentId+'&pageSize=3&pageNr=1',
            success: function(data){
                this.setState({
                    proj: data
                })

            }.bind(this)
        })
    }

    render(){

        const items = this.state.proj.map( (element, index) => {
            return(
                <Tr
                    node = {element}
                    key = {index}

                />
            )

        });

        return(
            <div>
                <h1>{this.props.routeParams.departmentName + ' Projects'}  </h1>
                <button className="btn btn-xs btn-info" > <span className="glyphicon glyphicon-plus-sign"></span> Add new project </button>
                <table className="table table-condensed" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Name</th>
                        <th className="col-md-2">Team members</th>
                        <th className="col-md-2">Duration</th>
                        <th className="col-md-2">Status</th>
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