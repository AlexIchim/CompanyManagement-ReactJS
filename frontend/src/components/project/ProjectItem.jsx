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
        console.log("projId",this.props.node.get('Id'));
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
                console.log("asd");
                
            }
        })  
    }

     refresh(departmentId){
         Controller.getAllDepProjects(departmentId,"",1);
     }


    render(){
        const linkMembers = "project/" + this.props.node.get('Id')  + '/' + this.props.node.get('Name') + "/members";
        
        const editModal = this.state.edit ? <EditFormProject departmentId={this.props.departmentId} element={this.props.node} show = {this.state.edit} close={this.closeEditForm.bind(this)} /> : '';

        return(
                <tr>
                <td>{this.props.node.get('Name')}</td>
                <td>{this.props.node.get('EmployeesNumber')}</td>
                <td>{this.props.node.get('Status')}</td>
                <td>{this.props.node.get('Duration')}</td>
                <td><Link to={linkMembers}> View members | </Link>
                    <button className="linkButton" onClick={this.showEditForm.bind(this)}> Edit |</button>
                    <button className="linkButton" onClick={this.deleteProject.bind(this)}> Delete Project</button>
                    {editModal}
                </td>
               
                </tr>
            
        )
    }
}
