import * as React from 'react';
import {Link} from 'react-router';

export default class DepartmentItem extends React.Component<any, any> {

    onEditButtonClick(){
        this.props.onEditButtonClick(this.props.index);
    }

    render() {

        const props = this.props;

        return (
            <tr>
                <td>{props.element['Name']}</td>
                <td>{props.element['DepartmentManager']}</td>
                <td>{props.element['NumberOfEmployees']}</td>
                <td>{props.element['NumberOfProjects']}</td>
                <td>
                    <div className="btn-toolbar">
                        <Link to={""} className="btn btn-info">
                            View Members
                        </Link>

                        <Link to={"project"} className="btn btn-info">
                            View Projects
                        </Link>

                        <button id="editDepartment" className="btn btn-danger" onClick={this.onEditButtonClick.bind(this)}>
                            Edit
                        </button>
                    </div>
                </td>
            </tr>
        )
    }
}
