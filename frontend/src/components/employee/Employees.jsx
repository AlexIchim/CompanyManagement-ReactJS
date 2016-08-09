import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import EmployeeItem from './EmployeeItem.jsx'
import Form from './Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import Command from '../Command';
import MyController from './Controller/Controller'
import ViewDetailsForm from './ViewDetailsForm'
import '../../assets/less/index.less'

export default class Employees extends React.Component{
    constructor(){
        super();
        this.setState({
            form: false
        })
    }
    componentWillMount(){
        Context.subscribe(this.onContextChange.bind(this));
        //const employeeId = this.props.routeParams['employeeId'];

        MyController.getAllEmployees();
    }

    onContextChange(cursor){
        console.log('employees:', cursor.get('items'));
        this.setState({
            employees: cursor.get('items')
        });
    }

    closeForm(){
        this.setState({
            form: !this.state.form
        });
    }
    showForm(){
        this.setState({
            form: !this.state.form
        });
    }

    onAddButtonClick(){
        Context.cursor.set('formToggle',true);
    }

    onViewDetailsButtonClick(employee){
        console.log("View details clicked!");
        Context.cursor.set('formToggle', true);
        Context.cursor.set('model', employee)

        this.setState({
            buttonClicked: "viewDetails"
        })
    }

    onEditButtonClick(employee){
        console.log("Edit clicked!");
        Context.cursor.set('formToggle', true);
        Context.cursor.set('model', employee)
        this.setState({
            buttonClicked: "edit"
        })
    }

    onDeleteButtonClick(element){
        MyController.Delete(element);
    }

    onModalSaveClick(){
        console.log("STORING!");
        Command.hideModal();
    }

    render(){
        console.log('model is:', Accessors.model(Context.cursor));
        let modal = "";
        let modal1 = "";
        console.log('store?', Accessors.formToggle(Context.cursor));
        if(Accessors.formToggle(Context.cursor)){
            if(Accessors.model(Context.cursor)){
                if (this.state.buttonClicked === "viewDetails") {
                    modal = <ViewDetailsForm onCancelClick={Command.hideModal.bind(this)}
                                             onStoreClick={this.onModalSaveClick.bind(this)}
                                             Title="Edit Employee"/>;
                } else {
                    if (this.state.buttonClicked === "edit") {
                        modal = <Form onCancelClick={Command.hideModal.bind(this)}
                                      onStoreClick={this.onModalSaveClick.bind(this)}
                                      Title="Edit Employee"/>;
                    }
                }
                /*modal1=<ViewDetailsForm onCancelClick={Command.hideModal.bind(this)}
                            onStoreClick={this.onModalSaveClick.bind(this)}
                            Title="Edit Employee"/>;*/
            }else{
                modal=<Form onCancelClick={Command.hideModal.bind(this)}
                           onStoreClick={this.onModalSaveClick.bind(this)}
                           Title="Add Employee"/>;
            }
        }
        const items = this.state.employees.map( (employee, index) => {
            return (
                <EmployeeItem
                    node = {employee}
                    key = {index}
                    Link = {"department/members/" + employee.Id}
                    onViewDetails = {this.onViewDetailsButtonClick.bind(this, employee)}
                    onEdit = {this.onEditButtonClick.bind(this, employee)}
                    onDelete = {this.onDeleteButtonClick.bind(this, employee)}
                    />
            )
        });

        return (
            <div>

                {modal}

                <h1> Employees </h1>
                    <div className="input-group input-group-xs col-md-4">
                        <div className="input-group-btn">
                            <button type="button" className="btn btn-warning">Search by name</button>
                        </div>
                        <input type="text"  ref="inputName" className="form-control" placeholder="Search..." >
                        </input>
                    </div>
                <p></p>
                <div><button id="store" className="btn btn-success margin-top" onClick={this.onAddButtonClick.bind(this)}>
                    Add New Employee
                </button>
                    &nbsp;
                    <div className="btn-group">
                        <button type="button" className="btn btn-info">Job Type</button>
                        <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span className="caret"></span>
                            <span className="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul className="dropdown-menu" role="menu">
                            <li><a href="#">Part Time 4</a></li>
                            <li><a href="#">Part Time 6</a></li>
                            <li><a href="#">Full Time</a></li>
                        </ul>
                    </div>
                    &nbsp;
                    <div className="btn-group">
                        <button type="button" className="btn btn-info">Position</button>
                        <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span className="caret"></span>
                            <span className="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul className="dropdown-menu" role="menu">
                            <li><a href="#">Developer</a></li>
                            <li><a href="#">Project Manager</a></li>
                            <li><a href="#">QA</a></li>
                            <li><a href="#">Department Manager</a></li>
                        </ul>
                    </div>
                    &nbsp;
                    <div className="btn-group">
                        <button type="button" className="btn btn-info">Allocation</button>
                        <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span className="caret"></span>
                            <span className="sr-only">Toggle Dropdown</span>
                        </button>
                        <ul className="dropdown-menu" role="menu">
                            <li><a href="#">Fully Allocated</a></li>
                            <li><a href="#">Available</a></li>
                        </ul>
                    </div>
                </div>
                <table className="table table-hover">
                <thead>
                <tr>
                    <td><h3> Name </h3></td>
                    <td><h3> Position </h3></td>
                    <td><h3> Allocation </h3></td>
                    <td><h3> Actions </h3></td>
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