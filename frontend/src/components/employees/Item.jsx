import React from 'react';

export default (props) => { 
    const relDate = props.data.releaseDate ? new Data(props.data.releaseDate).toLocaleDateString() : " - ";
    const address = props.data.address || ' - ';

    return(
        <tr>
            <td>{props.data.name}</td>
            <td>{props.data.email}</td>
            <td>{address}</td>
            <td>{new Date(props.data.employmentDate).toLocaleDateString()}</td>
            <td>{relDate}</td>
            <td>{props.data.employmentHours}</td>
            <td>{props.data.positionName}</td>
            <td>{props.data.totalAllocation}%</td>
            <td>
                <button onClick={props.onView}>View details</button>
                <button>Release --nothing</button>
                <button onClick={props.onEdit}>Edit</button>
            </td>
        </tr>
    )
};