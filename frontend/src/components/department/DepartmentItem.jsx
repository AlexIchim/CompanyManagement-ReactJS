import * as React from 'react';
import {Link} from 'react-router';
import '../../assets/less/index.less';

export default class DepartmentItem extends React.Component<any, any> {

    submit(){
        this.props.onEditButtonClick(this.props.index);
    }

    render() {

        const props = this.props;

        const lft = {
            float: "left"
        };

        return (
            <tr className="table-tr">
                <td>
                    <div id="editDepartment" className="glyphicon glyphicon-edit custom-edit-icon" style={lft} onClick={this.submit.bind(this)}></div>
                </td>

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
                    </div>
                </td>
            </tr>
        )
    }
}
