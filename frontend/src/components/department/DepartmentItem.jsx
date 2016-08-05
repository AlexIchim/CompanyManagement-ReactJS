import * as React from 'react';
import {Link} from 'react-router';

export default class DepartmentItem extends React.Component<any, any> {

    submit(){
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
                        <button id="editDepartment" className="btn btn-info" onClick={this.submit.bind(this)}>
                            View Members
                        </button>
                        <button id="editDepartment" className="btn btn-info" onClick={this.submit.bind(this)}>
                            View Projects
                        </button>
                        <button id="editDepartment" className="btn btn-danger" onClick={this.submit.bind(this)}>
                            Edit
                        </button>
                    </div>
                </td>
            </tr>
        )
    }
}
