import * as React from 'react';
import Department from './DepartmentItem';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';
import Controller from './DepartmentController';
import Form from './Form';
import '../../assets/less/index.less';

export default class Departments extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){

        const officeId = this.props.routeParams['officeId'];

        this.setState({
            formToggle:false,
            officeId: officeId
        });

        this.subscription=Context.subscribe(this.onContextChange.bind(this));

        Context.cursor.set("items",[]);

        Controller.getDepartments(officeId);
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }


    onContextChange(cursor){
        console.log("Context has changed!");
        this.setState({
            formToggle: false,
            items: cursor.get('items')
        });
    }

    onAddButtonClick(){
        console.log("Add button pressed!");

        Context.cursor.set('model',null);
        this.setState({
            formToggle: true
        });
    }

    onEditButtonClick(index){
        console.log("Edit button pressed!");

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

    render(){

        console.log("Form toggle: ", this.state.formToggle);

        let form="";
        if(this.state.formToggle){
            if(Accessors.model(Context.cursor)){
                form=<Form onCancelClick={this.toggleModal.bind(this)}
                           FormAction={Controller.Update.bind(this, this.state.officeId)}
                           Title="Edit Department"
                           officeId={this.state.officeId}/>;
            }else{
                form=<Form onCancelClick={this.toggleModal.bind(this)}
                           FormAction={Controller.Add.bind(this, this.state.officeId)}
                           Title="Add Department"
                           officeId={this.state.officeId}/>;
            }
        }

        console.log("Hellooooo: ", this.state.items);

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

        return (
            <div>

                {form}



                <div className=" rectangle ">
                    <div className="glyphicon glyphicon-plus-sign custom-add-icon"
                         onClick={this.onAddButtonClick.bind(this)}>
                        <span className="add-span" onClick={this.onAddButtonClick.bind(this)}>Add Department</span>
                    </div>
                </div>

                <table className="table table-stripped ">
                    <thead>
                    <tr>
                        <td> </td>
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