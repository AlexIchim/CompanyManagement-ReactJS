import * as React from 'react';
import Department from './DepartmentItem';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';
import Controller from './DepartmentController';
import Form from './Form';
import classnames from 'classnames';

const PageButton = (props) => {
    const nameClass = classnames({"active" : props.isActive, "btn btn-success btn-xs" : true});
    return (
        <button id={props.index} className = {nameClass} onClick={props.onPageButtonClick}>
            {props.index}
        </button>
    );
}

export default class Departments extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){

        const officeId = this.props.routeParams['officeId'];

        this.setState({
            formToggle:false,
            officeId: officeId,
            currentPage: 1
        });

        this.subscription=Context.subscribe(this.onContextChange.bind(this));

        Context.cursor.set("items",[]);
        Context.cursor.set("totalNumberOfItems", -1);

        Controller.getDepartments(officeId, 1);
        Controller.getTotalNumberOfDepartments(officeId);
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }


    onContextChange(cursor){
        //console.log("Context has changed!");
        this.setState({
            formToggle: false,
            items: cursor.get('items'),
            totalNumberOfItems: cursor.get('totalNumberOfItems')
        });
    }

    onAddButtonClick(){
        //console.log("Add button pressed!");

        Context.cursor.set('model',null);
        this.setState({
            formToggle: true
        });
    }

    onEditButtonClick(index){
        //console.log("Edit button pressed!");

        const department = this.state.items[index];
        Context.cursor.set("model", department);

        this.setState({
            formToggle: true
        });
    }

    toggleModal(){
        this.setState({
            formToggle: false
        })
    }

    onPageButtonClick(pageNumber){
        //console.log("Page number: ", pageNumber);
        Controller.getDepartments(this.state.officeId, pageNumber);
        this.setState({
            currentPage: pageNumber
        })
    }

    render(){

        const totalNumberOfDepartments = this.state.totalNumberOfItems;
        const numberOfPages = Math.ceil(totalNumberOfDepartments/5);
        const currentPage = this.state.currentPage;


        //console.log("CurrentPage: ", currentPage);
        //console.log("Totaaaal: ", totalNumberOfDepartments, numberOfPages);

        let form="";
        if(this.state.formToggle){
            if(Accessors.model(Context.cursor)){
                form=<Form onCancelClick={this.toggleModal.bind(this)}
                           FormAction={Controller.Update.bind(this, this.state.officeId, currentPage)}
                           Title="Edit Department"
                           officeId={this.state.officeId}/>;
            }else{
                form=<Form onCancelClick={this.toggleModal.bind(this)}
                           FormAction={Controller.Add.bind(this, this.state.officeId, currentPage)}
                           Title="Add Department"
                           officeId={this.state.officeId}/>;
            }
        }

        const items = this.state.items.map( (department, index) => {
            return (
                <Department
                    element={department}
                    linkToEmployees={"department/members/" + department.Id}
                    linkToProjects={"department/projects/" + department.Id}
                    key={index}
                    onEditButtonClick={this.onEditButtonClick.bind(this, index)}
                />
            )
        })

        const buttons = [];
        for (var i=1; i <= numberOfPages; i++) {
            buttons.push(
                <PageButton
                    index = {i}
                    key = {i}
                    onPageButtonClick = {this.onPageButtonClick.bind(this, i)}
                    isActive = {i === currentPage}
                />
            );
        }

        return (
            <div>

                {form}

                <button className="btn btn-success" onClick={this.onAddButtonClick.bind(this)}>
                    Add Department
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

                {buttons}

            </div>
        )
    }
}