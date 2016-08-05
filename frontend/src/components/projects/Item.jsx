import React, { Component } from 'react';
import {Link} from 'react-router';
import * as Projects from './Projects';

export default class Item extends React.Component{
   
    deleteProject(project){
        Projects.deleteProject(project);
        Projects.fetchData();
    }

   render(){
       	const props = this.props;
        const onDelete = props.onDelete;
        const onEdit = props.onEdit;
        const linkMembers = ('offices/' + this.props.officeId + '/departments/' + this.props.node.departmentId  
                            + '/projects/' + this.props.node.id + '/members');

        return (
            <tr>
                <td>{props.node.name}</td>
                <td>{props.node.memberCount}</td>
                <td>{props.node.duration || 'variable'}</td>
                <td>{props.node.status}</td>
                <td>
                    
                        <button className = "btn btn-md btn-default" onClick = {onEdit}>
                            Edit      
                        </button>
                    
                    
                    <Link to = {linkMembers}>
                        <button className = "btn btn-md btn-default">
                            View members
                        </button>
                    </Link>
                    
                    
                        <button className = "btn btn-md btn-default" onClick = {onDelete} >
                            Remove
                        </button>
                    
                </td>
            </tr>
        )
   }
}