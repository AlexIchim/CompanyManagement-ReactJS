import React from 'react';

export default (props) => {
    
    const employee = props.data.employee;
    const allocation = props.data.allocation;
    
    return(
        <tr>
            <td>{employee.name}</td>
            <td>{employee.positionName}</td>
            <td>{allocation}%</td>
            <td><button>Do Nothing</button></td>
        </tr>
    )
};