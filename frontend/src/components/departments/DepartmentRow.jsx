import React, { Component } from 'react';
import {Link} from 'react-router';

export default class DepartmentRow extends React.Component<any, any>{
    render(){
        const props = this.props;
        const linkEmployees = "offices/" + props.node.officeId +  "/departments/" + props.node.id  + "/employees";
        const linkProjects = "offices/" + props.node.officeId + "/departments/" + props.node.id  + "/projects";
        return(

            <tr>
                <td>{props.node.name}</td>
                <td>{props.node.departmentManagerName}</td>
                <td>{props.node.employeeCount}</td>
                <td>{props.node.projectCount}</td>
                <td><Link to={linkEmployees}>View employees |</Link>
                    <Link to={linkProjects}> View projects |</Link>
                    <Link to="#"> Edit </Link></td>
            </tr>
        )
    }
}