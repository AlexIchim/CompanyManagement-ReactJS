import * as React from 'react';
import  config from '../helper';
import * as $ from 'jquery';

const Item = (props) => {
    return(
        <tr>
            <td>{props.element['Name']}</td>
            <td>{props.element['Address']}</td>
            <td>{props.element['EmploymentDate']}</td>
            <td>{props.element['ReleasedDate']}</td>
            <td>{props.element['JobType']}</td>
            <td>{props.element['Position']}</td>
            <td>{props.element['Allocation']}</td>
            <button id="viewEmployeeDetails" className="btn btn-info margin-top">
                View details
            </button>
            &nbsp;
            <button id="releaseEmployee" className="btn btn-danger margin-top">
                Release
            </button>
                &nbsp;
            <button id="editEmployee" className="btn btn-success margin-top">
                Edit
            </button>
        </tr>
    )
}

class Employees extends React.Component{

    constructor(){
        super();
    }

    componentWillMount(){

        const departmentId = this.props.routeParams['departmentId'];

        $.ajax({
            method: 'GET',
            url: config.base + 'department/members/' + departmentId,
            async: false,
            success: function(data) {
                this.setState({
                    employees: data
                })
            }.bind(this)
        })

    }

    render(){
        const items = this.state.employees.map( (element, i) => {
            return(
                <Item
                    element={element}
                    key={i}
                />
            )
        });

        return (
            <div>
                <h1>Employees</h1>

                <button id="addEmployee" className="btn btn-success margin-top">
                    Add Employee
                </button>
                <hr/>
                <div className="input-group input-group-normal">
                    <div className="input-group-btn">
                        <button type="button" className="btn btn-warning dropdown-toggle" data-toggle="dropdown" aria-expanded="false">Search
                            <span className="fa fa-caret-down"></span></button>
                    </div>
                    <input type="text" className="form-control"></input>
                </div>
                <hr/>                <div className="btn-group">


                    <button type="button" className="btn btn-info">Allocation</button>
                    <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <span className="caret"></span>
                        <span className="sr-only">Toggle Dropdown</span>
                    </button>
                </div>
                &nbsp;
                <div className="btn-group">
                    <button type="button" className="btn btn-info">Position</button>
                    <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <span className="caret"></span>
                        <span className="sr-only">Toggle Dropdown</span>
                    </button>
                </div>
                &nbsp;
                <div className="btn-group">
                    <button type="button" className="btn btn-info">Job Type</button>
                    <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <span className="caret"></span>
                        <span className="sr-only">Toggle Dropdown</span>
                    </button>
                </div>
            <table className="table table-hover">
                <thead>
                <tr>
                    <td>Name</td>
                    <td>Address</td>
                    <td>Employment Date</td>
                    <td>Termination Date</td>
                    <td>Job Type</td>
                    <td>Position</td>
                    <td>Allocation</td>
                    <td>Actions</td>
                </tr>
                </thead>
                <tbody>

                {items}

                </tbody>
            </table>
            </div>
        )}
}

export default Employees;

{/*import * as React from 'react';
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

 export default Employees;*/}
