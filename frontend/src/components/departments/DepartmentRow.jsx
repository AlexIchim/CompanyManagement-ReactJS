import React, { Component } from 'react';
import {Link} from 'react-router';

export default class DepartmentRow extends React.Component<any, any>{
    render(){
        const props = this.props;
        const linkEmployees = "offices/" + props.node.OfficeId +  "/departments/" + props.node.Id  + "/employees";
        const linkProjects = "offices/" + props.node.OfficeId + "/departments/" + props.node.Id  + "/projects";
        return(

            <tr>
                <td>{props.node.Name}</td>
                <td>{props.node.DepartmentManagerName}</td>
                <td>{props.node.EmployeeCount}</td>
                <td>{props.node.ProjectCount}</td>
                <td><Link to={linkEmployees}>View employees |</Link>
                    <Link to={linkProjects}> View projects |</Link>
                    <Link to="#"> Edit </Link></td>
            </tr>
        )
    }
}