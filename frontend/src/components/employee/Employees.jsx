import * as React from 'react';
import config from '../helper';
import * as $ from 'jquery';
import {Link} from 'react-router';
import Form from './Form';

const Employee = (props) => {
    return (
        <tr>
            <td>{props.element['Id']}</td>
            <td>{props.element['Name']}</td>
            <td>
                <button id="editEmployee" className="btn btn-danger margin-top">
                    Edit
                </button>
            </td>
        </tr>
    )
}

class Employees extends React.Component{

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

        const employeeId = this.props.routeParams['employeeId'];

        $.ajax({
            method: 'GET',
            url: config.base + 'department/members/' + employeeId,
            async: false,
            success: function (data) {
                this.setState({
                    employees: data
                })
            }.bind(this)
        })
    }
    render(){

        const modal = this.state.store ? <Form show = {this.state.store}  close={this.closeStoreForm.bind(this)} /> : '';

        const items = this.state.employees.map( (employee, index) => {
            return (
                <Employee
                    element={employee}
                    linkToEmployees={"department/members/" + employeeId}
                    key={index}
                />
            )
        })
        return (

            <div>

                {modal}

                <button id="addEmployee" className="btn btn-success margin-top" onClick={this.showStoreForm.bind(this)}>
                    Add new employee
                </button>

                <table className="table table-hover">
                    <thead>
                    <tr>
                        <td>Id</td>
                        <td>Name</td>
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

export default Employees;