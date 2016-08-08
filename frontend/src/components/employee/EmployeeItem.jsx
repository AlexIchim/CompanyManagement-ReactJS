import * as React from 'react'
import * as $ from 'jquery'
import {Link} from 'react-router';
import Command from '../Command';

const EmployeeItem = (props) => {


    const id = props.node.Id;
    const name = props.node.Name;
    const address = props.node.Address;
    const employmentDate = props.node.EmploymentDate;
    const releasedDate = props.node.ReleasedDate;
    const jobType = props.node.JobType;
    const position = props.node.Position;
    const allocation = props.node.Allocation;
    const link = props.Link;
    const edit = props.onEdit;
    const viewDetails = props.onViewDetails;
    const deleteF = props.onDelete;

    return (
        <tr>
            <td>
                {name}
            </td>
            <td>
                {address}
            </td>
            <td>
                {employmentDate}
            </td>
            <td>
                {releasedDate}
            </td>
            <td>
                {jobType}
            </td>
            <td>
                {position}
            </td>
            <td>
                {allocation}
            </td>
            <td>
                <tr>
                    <td>
                        {/*<Link to={link} className="btn btn-success margin-top" onClick={edit}>
                            View Details
                        </Link>*/}
                        <button className="btn btn-success margin-top" onClick={viewDetails}>
                            View Details
                        </button>
                        &nbsp;
                        <button id="store" className="btn btn-success margin-top" onClick={edit}>
                            Edit
                        </button>
                                &nbsp;
                        <button className="btn btn-danger margin-top" onClick={deleteF}><i className="fa fa-trash" ></i>
                            Delete
                        </button>
                    </td>
                </tr>
            </td>

        </tr>
    )
}

export default EmployeeItem;