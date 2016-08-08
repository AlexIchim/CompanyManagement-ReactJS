import React from 'react';
import {Link} from 'react-router';

export default (props) => {
    
    const employee = props.data.employee;
    const allocation = props.data.allocation;
    let onDelete = props.onDelete;
    let onEdit = props.onEdit;
    
    return(
        <tr>
            <td>{employee.name}</td>
            <td>{employee.departmentName}</td>
            <td>{employee.positionName}</td>
            <td>{allocation} %</td>
            <td className="btn-toolbar">
                <button className="btn" onClick={onEdit} >Edit allocation</button>
                <button className="btn" onClick={onDelete} >Remove </button>
            </td>
        </tr>
    )
};