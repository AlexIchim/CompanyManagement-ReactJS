import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';
import "./../../assets/less/index.less";
import EditMembersForm from './EditMembersForm.jsx'

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


    render(){

        const editForm =  this.state.edit ? <EditMembersForm projectId = {this.props.projectId} element={this.props.node} show = {this.state.edit} close={this.closeEditForm.bind(this)} /> : ''; 

         return(
        <tr>
            <td>{this.props.node.get('Name')} </td>
            <td>{this.props.node.get('Role')}</td>
            <td>{this.props.node.get('Allocation')}</td>
            <td><button className="linkButton" onClick={this.showEditForm.bind(this)} >Edit Allocation | </button>
                <Link to="#"> Remove </Link>
                {editForm}
            </td>
        </tr>
    )
    }
}
