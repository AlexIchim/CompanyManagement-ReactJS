import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';
import FormProject from './FormProject';
import EditFormProject from './EditFormProject';
import "./../../assets/less/index.less";
import * as Controller from '../controller';
import configs from '../helpers/calls';

export default class ProjectItem extends React.Component{
    
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

    deleteProject(){
         $.ajax({
            method: 'DELETE',
            async: false,
            url: configs.baseUrl + 'api/project/delete?projectId='+this.props.node.get('Id'),
            success: function (data) { 
                if (data.Success==false){
                    alert("Nu puteti sterge un proiect ce are asignati angajati!");
                }
                this.refresh(this.props.departmentId);       
            }.bind(this),
            error: function (data) {
            }
        })  
    }

     refresh(departmentId){
         this.props.setPageNr();
         Controller.getAllDepProjects(departmentId,{},1);
     }


    render(){
        const linkMembers = "project/" + this.props.node.get('Id')  + '/' + this.props.node.get('Name') + '/' + this.props.officeId + "/members";
        
        const editModal = this.state.edit ? <EditFormProject index={this.props.index} departmentId={this.props.departmentId} element={this.props.node} show = {this.state.edit} close={this.closeEditForm.bind(this)} /> : '';
        let duration
        if (this.props.node.get('Duration') == 0)
            duration = "Variable"
        else
            duration = this.props.node.get('Duration')
        return(
                <tr>
                <td>{this.props.node.get('Name')}</td>
                <td>{this.props.node.get('EmployeesNumber')}</td>
                <td>{this.props.node.get('Status')}</td>
                <td>{duration} months</td>
                <td><Link to={linkMembers}> View members | </Link>
                    <button className="linkButton" onClick={this.showEditForm.bind(this)}> Edit | </button>
                    <button className="linkButton" onClick={this.deleteProject.bind(this)}> Delete</button>
                    {editModal}
                </td>
               
                </tr>
            
        )
    }
}
