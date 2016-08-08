import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';
import "./../../assets/less/index.less";
import EditMembersForm from './EditMembersForm.jsx'
import * as Controller from '../controller';
import configs from '../helpers/calls';

export default class DepartmentItem extends React.Component{
    
    constructor(){
        super();
        this.state = {
            edit: false
        }
    }

    showEditForm(){       
        this.setState({
            edit: !this.state.edit
        })
    }

    closeEditForm(){
        this.setState({
            edit: !this.state.edit
        })
    }

    deleteEmployeeFromProject(){
        console.log(this.props.node.get('Id') + this.props.projectId)
         $.ajax({
            method: 'DELETE',
            async: false,
            url: configs.baseUrl + 'api/project/deleteEmployeeFromProject?employeeId='+this.props.node.get('Id') + '&projectId='+this.props.projectId,
            success: function (data) { 
                this.refresh(this.props.projectId);          
            }.bind(this)
        })   

  
    }
    refresh(projectId){
        Controller.getEmployeesByProjectId(projectId,1);
    }



    render(){

        const editForm =  this.state.edit ? <EditMembersForm projectId = {this.props.projectId} element={this.props.node} show = {this.state.edit} close={this.closeEditForm.bind(this)} /> : ''; 

         return(
        <tr>
            <td>{this.props.node.get('Name')} </td>
            <td>{this.props.node.get('Role')}</td>
            <td>{this.props.node.get('Allocation')}</td>
            <td><button className="linkButton" onClick={this.showEditForm.bind(this)} >Edit Allocation | </button>
                <button className="linkButton" onClick={this.deleteEmployeeFromProject.bind(this)} >Delete | </button>
                {editForm}
            </td>
        </tr>
    )
    }
}
