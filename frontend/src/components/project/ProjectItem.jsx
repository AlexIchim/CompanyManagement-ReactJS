import * as React from 'react'
import * as $ from 'jquery'
import {Link} from 'react-router';

const ProjectItem = (props) => {


    const id = props.node.Id;
    const name = props.node.Name;
    const nrMembers = props.node.NrMembers;
    const duration = props.node.Duration;
    const status = props.node.Status;
    const link = props.Link;
    const edit = props.onEdit;
    const deleteF = props.onDelete;

    return (
        <tr>

            <td>
                {name}
            </td>
            <td>
                {nrMembers}
            </td>
            <td>
                {duration}
            </td>
            <td>
                {status}
            </td>
            <td>

                <tr>
                    <td>
                        <Link to={link} className="small-box-footer">
                            View Members
                        </Link></td>
                    <td>
                <button id="store" className="btn btn-success margin-top" onClick={edit}>
                    Edit
                </button>
                </td><td>

                <button className="btn btn-danger margin-top" onClick={deleteF}><i className="fa fa-trash" ></i>
                    Delete
                </button></td>
                        </tr>

            </td>

        </tr>
    )
}

export default ProjectItem;