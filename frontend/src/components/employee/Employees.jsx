import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import EmployeeItem from './EmployeeItem.jsx'
import Form from './Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import Command from '../Command';
import MyController from './Controller/Controller'
import EditForm from './EditForm';

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

    onEditButtonClick(employee){
        Context.cursor.set('formToggle', true);
        Context.cursor.set('model', employee)
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
        console.log('store?', Accessors.formToggle(Context.cursor));
        if(Accessors.formToggle(Context.cursor)){
            if(Accessors.model(Context.cursor)){
                modal=<EditForm onCancelClick={Command.hideModal.bind(this)}
                           onStoreClick={this.onModalSaveClick.bind(this)}
                           Title="Edit Employee"/>;
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
                    onEdit = {this.onEditButtonClick.bind(this, employee)}
                    onDelete = {this.onDeleteButtonClick.bind(this, employee)}
                    />
            )
        });

        return (
            <div>

                {modal}

            <table className="table table-hover">
                <thead>
                <h1> Employees </h1>
                <button id="store" className="btn btn-success margin-top" onClick={this.onAddButtonClick.bind(this)}>
                    Add New Employee
                </button>
                <p></p>
                <div className="btn-group">
                    <button type="button" className="btn btn-info">Job Type</button>
                    <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                        <span className="caret"></span>
                        <span className="sr-only">Toggle Dropdown</span>
                    </button>
                    {/*<ul className="dropdown-menu" role="menu">
                        <li><a href="#">Action</a></li>
                        <li><a href="#">Another action</a></li>
                        <li><a href="#">Something else here</a></li>
                        <li className="divider"></li>
                        <li><a href="#">Separated link</a></li>
                    </ul>*/}
                    &nbsp;
                </div>
                    <div className="btn-group">
                        <button type="button" className="btn btn-info">Position</button>
                        <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span className="caret"></span>
                            <span className="sr-only">Toggle Dropdown</span>
                        </button>
                        {/*<ul className="dropdown-menu" role="menu">
                         <li><a href="#">Action</a></li>
                         <li><a href="#">Another action</a></li>
                         <li><a href="#">Something else here</a></li>
                         <li className="divider"></li>
                         <li><a href="#">Separated link</a></li>
                         </ul>*/}
                    </div>
                    &nbsp;
                        <div className="btn-group">
                            <button type="button" className="btn btn-info">Allocation</button>
                            <button type="button" className="btn btn-info dropdown-toggle" data-toggle="dropdown">
                                <span className="caret"></span>
                                <span className="sr-only">Toggle Dropdown</span>
                            </button>
                            {/*<ul className="dropdown-menu" role="menu">
                             <li><a href="#">Action</a></li>
                             <li><a href="#">Another action</a></li>
                             <li><a href="#">Something else here</a></li>
                             <li className="divider"></li>
                             <li><a href="#">Separated link</a></li>
                             </ul>*/}
                        </div>

                <tr>
                    <td><h3> Name </h3></td>
                    <td><h3> Address </h3></td>
                    <td><h3> Employment Date </h3></td>
                    <td><h3> Termination Date </h3></td>
                    <td><h3> Job Type </h3></td>
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