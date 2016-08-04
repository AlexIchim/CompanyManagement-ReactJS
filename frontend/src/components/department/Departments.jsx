{/*import * as React from 'react';
import config from '../helper';
import * as $ from 'jquery';
import {Link} from 'react-router';
import Form from './Form';

const Department = (props) => {
    return (
        <tr>
            <td>{props.element['Name']}</td>
            <td>{props.element['DepartmentManager']}</td>
            <td>{props.element['NumberOfEmployees']}</td>
            <td>{props.element['NumberOfProjects']}</td>
            <td>
                <Link to={props.linkToEmployees}>
                    View Employees
                </Link>
                <hr/>
                <Link to={props.linkToProjects}>
                    View Projects
                </Link>
                <hr/>
                <button id="editDepartment" className="btn btn-danger margin-top">
                    Edit
                </button>
            </td>
        </tr>
    )
}

class Departments extends React.Component{

    constructor(){
        super();
        this.state = {
            store: false
        }
    }

    showStoreForm(){
        this.setState({
            store: !this.state.store
        })
    }

    closeStoreForm(){
        this.setState({
            store: !this.state.store
        })
    }

    componentWillMount(){

        const officeId = this.props.routeParams['officeId'];

        $.ajax({
            method: 'GET',
            url: config.base + 'office/departments/' + officeId,
            async: false,
            success: function (data) {
                this.setState({
                    departments: data
                })
            }.bind(this)
        })
    }

    render(){

        const modal = this.state.store ? <Form show = {this.state.store}  close={this.closeStoreForm.bind(this)} /> : '';

        const items = this.state.departments.map( (department, index) => {
            return (
                <Department
                    element={department}
                    linkToEmployees={"department/members/" + department.Id}
                    linkToProjects={"department/projects/" + department.Id}
                    key={index}
                />
            )
        })

        return (

            <div>

                {modal}

                <button id="addDepartment" className="btn btn-success margin-top" onClick={this.showStoreForm.bind(this)}>
                    Add new department
                </button>

                <table className="table table-stripped">
                    <thead>
                    <tr>
                        <td>Department</td>
                        <td>Department Manager</td>
                        <td>Employees</td>
                        <td>Projects</td>
                        <td>Actions</td>
                    </tr>
                    </thead>
                    <tbody>
                        {items}
                    </tbody>
                </table>

            </div>
        )
    }
}

export default Departments;*/}