import React from 'react';

export default (props) => { 
    const relDate = props.data.releaseDate ? new Date(props.data.releaseDate).toLocaleDateString() : " - ";
    const address = props.data.address || ' - ';

    const deleteButton = props.data.releaseDate===null || props.data.releaseDate === '' ? 
        (<button className="btn" onClick={props.onDelete}>Release</button>) :
        (<button className="btn btn-disabled" disabled="true">Release</button>);

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
            <td className="btn-toolbar">
                <button className="btn" onClick={props.onView}>View details</button>
                {deleteButton}
                <button className="btn" onClick={props.onEdit}>Edit</button>
            </td>
        </tr>
    )
};