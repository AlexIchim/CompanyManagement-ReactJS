import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import EmployeeItem from './EmployeeItem.jsx'
import Form from './Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import MyController from './Controller/Controller'
import ViewDetailsForm from './ViewDetailsForm'
import '../../assets/less/index.less'

export default class Employees extends React.Component{
    constructor(){
        super();
    }

    mountingComponent(props){
        const departmentId = props.routeParams['departmentId'];

        this.setState({
            formToggle:false,
            departmentId: departmentId,
            currentPage: 1
        });

        this.subscription = Context.subscribe(this.onContextChange.bind(this));

        Context.cursor.set("items",[]);
        Context.cursor.set("totalNumberOfItems", -1);

        MyController.getAllEmployees(departmentId, 1);
        MyController.getTotalNumberOfEmployees(departmentId);
    }

    componentWillMount(){
        this.mountingComponent(this.props);
    }

    componentWillReceiveProps(props){
        const departmentId = props.routeParams['departmentId'];
        Context.cursor.set("items",[]);
        Context.cursor.set("totalNumberOfItems", -1);

        MyController.getAllEmployees(departmentId, 1);
        MyController.getTotalNumberOfEmployees(departmentId);
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }

    onContextChange(cursor){
        console.log('employees:', cursor.get('items'));
        this.setState({
            formToggle: false,
            employees: cursor.get('items'),
            totalNumberOfItems: cursor.get('totalNumberOfItems')
        });
    }

    onAddButtonClick(){
        Context.cursor.set('model',null);
        this.setState({
            formToggle: true
        });
    }

    onViewDetailsButtonClick(employee){
        console.log("View details clicked! ", employee);
        Context.cursor.set('model', employee);

        this.setState({
            formToggle: true,
            buttonClicked: "viewDetails"
        })
    }

    onEditButtonClick(employee){
        console.log("Edit clicked!");
        //MyController.Edit(employee);
        Context.cursor.set('model', employee)
        this.setState({
            formToggle: true,
            buttonClicked: "edit"
        })
    }

    onDeleteButtonClick(element){
        MyController.Delete(element);
    }

    toggleModal(){
        this.setState({
            formToggle: false
        })
    }

    onPreviousButtonClick(){
        let currentPage = this.state.currentPage;
        let newCurrentpage = currentPage - 1;
        if (currentPage > 1){
            this.setState({
                currentPage: newCurrentpage
            })
            MyController.getAllEmployees(this.state.departmentId, newCurrentpage);
        }
    }

    onNextButtonClick(){
        let currentPage = this.state.currentPage;
        let newCurrentpage = currentPage + 1;
        let numberOfPages = Math.ceil((this.state.totalNumberOfItems)/5);
        if (currentPage < numberOfPages){
            this.setState({
                currentPage: newCurrentpage
            })
            MyController.getAllEmployees(this.state.departmentId, newCurrentpage);
        }
    }

    onGoToFirstPageButtonClick(){
        this.setState({
            currentPage: 1
        })
        MyController.getAllEmployees(this.state.departmentId, 1);
    }

    onGoToLastPageButtonClick(){
        let numberOfPages = Math.ceil((this.state.totalNumberOfItems)/5);
        this.setState({
            currentPage: numberOfPages
        });
        MyController.getAllEmployees(this.state.departmentId, numberOfPages);
    }

    render(){

        const totalNumberOfDepartments = this.state.totalNumberOfItems;
        const numberOfPages = (totalNumberOfDepartments == 0) ? 1 : Math.ceil(totalNumberOfDepartments/5);
        const currentPage = this.state.currentPage;
        const label = currentPage + "/" + numberOfPages;

        console.log('In Employees@!@@');
        let modal = "";
        let modal1 = "";
        if(this.state.formToggle){
            if(Accessors.model(Context.cursor)){
                if (this.state.buttonClicked === "viewDetails") {
                    modal = <ViewDetailsForm onCancelClick={this.toggleModal.bind(this)}
                                             Title="View Details"/>;
                } else {
                    if (this.state.buttonClicked === "edit") {
                        modal = <Form onCancelClick={this.toggleModal.bind(this)}
                                      FormAction={() => {MyController.Edit(this.state.departmentId, currentPage)}}
                                      Title="Edit Employee"/>;
                    }
                }
            }else{
                modal=<Form onCancelClick={this.toggleModal.bind(this)}
                            FormAction={() => {MyController.Add(this.state.departmentId, currentPage)}}
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
                <p>

                </p>

                <div>

                    <button id="store" className="btn btn-success margin-top" onClick={this.onAddButtonClick.bind(this)}>
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

                <div className="btn-group">
                    <button className="btn btn-info" onClick={this.onGoToFirstPageButtonClick.bind(this)}>
                        Go to first page
                    </button>
                    <button className="btn btn-warning" onClick={this.onPreviousButtonClick.bind(this)}>
                        Prev
                    </button>
                    <button className="btn btn-warning">{label}</button>
                    <button className="btn btn-warning" onClick={this.onNextButtonClick.bind(this)}>
                        Next
                    </button>
                    <button className="btn btn-info" onClick={this.onGoToLastPageButtonClick.bind(this)}>
                        Go to last page
                    </button>
                </div>

            </div>
        )
    }
}