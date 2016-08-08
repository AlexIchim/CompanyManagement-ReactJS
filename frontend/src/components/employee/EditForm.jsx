import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as $ from 'jquery';

export default class EditForm extends React.Component{
    
    constructor(){
        super();
        this.state={
            jobTypes:[],
            positionTypes:[],
            employee:{
            }
        }
    }
     componentWillMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getJobTypes',
            success: function (data) {
                this.setState({
                    jobTypes: data,
                    employee: this.props.element
                })
            }.bind(this)
        })

        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getPositionTypes',
            success: function (data) {
                this.setState({
                    positionTypes: data,
                    employee: this.props.element
                })
            }.bind(this)
        })          
    }

    changeData(){
       
        const newO = {
            Id: this.state.employee.get('Id'),
            Name:this.refs.name.value,
            Address:this.refs.address.value,
            DepartmentManager:this.state.department.get('DepartmentManager'),
            NbrOfEmployees:this.state.department.get('NbrOfEmployees'),
            NbrOfProjects: this.state.department.get('NbrOfProjects')
        }

        this.setState({
            employee: Immutable.fromJS(newO)
        })         
    }
}