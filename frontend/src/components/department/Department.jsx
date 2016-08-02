import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'


const Tr = (props) => {
    return(
        <tr>
            <td>{ props.node.Name } </td>
            <td>{props.node.DepartmentManager}</td>
            <td>{props.node.NbrOfEmployees}</td>
            <td>{props.node.NbrOfProjects}</td>
            <td><a href="#"> View employees | </a>
                <a href="#"> View projects | </a>
                <a href="#"> Edit </a></td>
        </tr>
    )
}

export default class Department extends React.Component {

    constructor() {
        super();
        this.state = {
            dep: []
        }
    }
    
    componentWillMount(){
        console.log(configs);
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/office/getAllDepOffice?officeId=1',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    dep: data
                })
            }.bind(this)
        })
    }



    render() {

        const items = this.state.dep.map((el, x) => {
            return (
                <Tr
                node = {el}
                key= {x}

                /> 
            )
        });


        return (

            <div>
                <h1>X Department </h1>
                <button className="btn btn-xs btn-info" > <span className="glyphicon glyphicon-plus-sign"></span> Add new department </button>
                <table className="table table-condensed" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Department</th>
                        <th className="col-md-2">Department manager</th>
                        <th className="col-md-2">Employees</th>
                        <th className="col-md-2">Projects</th>
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