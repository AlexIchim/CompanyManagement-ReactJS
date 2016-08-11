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
    componentWillMount(){

        this.setState({
            formToggle:false,
            jobTypeIndex: null,
            positionIndex: null,
            allocationIndex: null,
            search: null
        });
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
        //const employeeId = this.props.routeParams['employeeId'];
        MyController.getAllEmployees(null,  null, null, null);
        this.setJobTypeDropdownItems();
        this.setPositionDropdownItems();
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }

    onContextChange(cursor){
        console.log('employees:', cursor.get('items'));
        this.setState({
            formToggle: false,
            employees: cursor.get('items')
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

    setJobTypeDropdownItems(){
        $.ajax({
            method: 'GET',
            url: config.base + "/employee/getJobTypes",
            async: false,
            success: function(data){
                console.log('here');
                this.setState({
                    jobTypeDropdownItems: data
                })
            }.bind(this)
        });
    }
    setPositionDropdownItems(){
        $.ajax({
            method:'GET',
            url: config.base + "/employee/getPositions",
            async: false,
            success: function(data){
                this.setState({
                    positionDropdownItems: data
                })
            }.bind(this)
        })
    }

    filterByJobType(){
        var select = document.getElementById('jobTypeDropdown');
        var jobTypeIndex = select.options[select.selectedIndex].value;
        var positionIndex = this.state.positionIndex;
        this.setState({
            jobTypeIndex: jobTypeIndex,
            positionIndex: positionIndex,
            allocationIndex: this.state.allocationIndex,
            search: this.state.search
        })
        MyController.getAllEmployees(this.state.search, jobTypeIndex, positionIndex, this.state.allocationIndex)
    }
    filterByPosition(){
        var select = document.getElementById('positionDropdown');
        var positionIndex = select.options[select.selectedIndex].value;
        var jobTypeIndex = this.state.jobTypeIndex;
        this.setState({
            jobTypeIndex: jobTypeIndex,
            positionIndex: positionIndex,
            allocationIndex: this.state.allocationIndex,
            search: this.state.search
        })
        MyController.getAllEmployees(this.state.search, jobTypeIndex, positionIndex, this.state.allocationIndex)

    }
    filterByAllocation(){

        var select = document.getElementById('allocationDropdown');
        var allocationIndex = select.options[select.selectedIndex].value;
        console.log('all index', allocationIndex);
        this.setState({
            jobTypeIndex: this.state.jobTypeIndex,
            positionIndex: this.state.positionIndex,
            allocationIndex: allocationIndex,
            search: this.state.search
        })
        MyController.getAllEmployees(this.state.search, this.state.jobTypeIndex, this.state.positionIndex, allocationIndex)
    }

    searchByName(){
        let name = this.refs.inputName.value;
        console.log('search : ', name);
        this.setState({
            jobTypeIndex: this.state.jobTypeIndex,
            positionIndex: this.state.positionIndex,
            allocationIndex: this.state.allocationIndex,
            search: name
        })
        MyController.getAllEmployees(name, this.state.jobTypeIndex, this.state.positionIndex, this.state.allocationIndex)

    }
    render(){
        let jobTypeDropdownItems = this.state.jobTypeDropdownItems.map( (element, index) => {
            return (<option value={element.Index} key = {element.Index} > {element.Description} </option>)
        });
         let positionDropdownItems = this.state.positionDropdownItems.map( (element, index) => {
             return (<option value={element.Index} key = {element.Index} > {element.Description} </option>)
         });


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
                                      FormAction={MyController.Edit.bind(this)}
                                      Title="Edit Employee"/>;
                    }
                }
                /*modal1=<ViewDetailsForm onCancelClick={Command.hideModal.bind(this)}
                            onStoreClick={this.onModalSaveClick.bind(this)}
                            Title="Edit Employee"/>;*/
            }else{
                modal=<Form onCancelClick={this.toggleModal.bind(this)}
                            FormAction={MyController.Add.bind(this)}
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

                <p className="table-name">Employees </p>
                    <div className="input-group input-group-xs col-md-4">
                        <div className="input-group-btn">
                            <button type="button" className="btn btn-warning">Search by name</button>
                        </div>
                        <input type="text"  ref="inputName" className="form-control" placeholder="Search..." onChange={this.searchByName.bind(this)} >
                        </input>
                    </div>
                <p></p>
                <div>
                    <div className=" rectangle custom-rectangle-department">
                        <div className="glyphicon glyphicon-plus-sign custom-add-icon"
                             onClick={this.onAddButtonClick.bind(this)}>
                            <span className="add-span" onClick={this.onAddButtonClick.bind(this)}>Add Employee</span>
                        </div>
                    </div>
                    
                    <select id='jobTypeDropdown' className="selectpicker" onChange={this.filterByJobType.bind(this)}>
                        <option selected>-- Job Type --</option>

                            {jobTypeDropdownItems}

                   </select>
                    &nbsp;
                    <select id='positionDropdown' className="selectpicker" onChange={this.filterByPosition.bind(this)}>
                        <option selected>-- Position --</option>

                        {positionDropdownItems}

                    </select>
                    &nbsp;
                    <select id='allocationDropdown' className="selectpicker" onChange={this.filterByAllocation.bind(this)}>
                        <option selected>-- Allocation --</option>

                        <option value={0} key = {0} > {"Not Allocated"} </option>
                        <option value={10} key = {10} > {10} </option>
                        <option value={20} key = {20} > {20} </option>
                        <option value={30} key = {30} > {30} </option>
                        <option value={40} key = {40} > {40} </option>
                        <option value={50} key = {50} > {50} </option>
                        <option value={60} key = {60} > {60} </option>
                        <option value={70} key = {70} > {70} </option>
                        <option value={80} key = {80} > {80} </option>
                        <option value={90} key = {90} > {90} </option>
                        <option value={100} key = {100} > {"Fully Allocated"} </option>

                    </select>
                </div>
                <table className="table">
                <thead>
                <tr>
                    <td> </td>
                    <td>Name </td>
                    <td>Position</td>
                    <td>Allocation</td>
                    <td>Views</td>
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