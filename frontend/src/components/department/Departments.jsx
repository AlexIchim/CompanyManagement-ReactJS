import * as React from 'react';
import Department from './DepartmentItem';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';
import Controller from './DepartmentController';
import Form from './Form';

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

        //const officeId = this.props.routeParams['officeId'];
        Controller.getDepartments(officeId);
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }


    onContextChange(cursor){
        console.log("Context has changed!");
        this.setState({
            formToggle: false
        });
    }

    onAddButtonClick(){
        Context.cursor.set('model',null);

        this.setState({
            formToggle: true
        });
    }
    onEditButtonClick(index){
        const department=Context.cursor.get('items')[index];
        Context.cursor.set("model", department);

        this.setState({
            formToggle: true
        });
    }

    toggleModal(){
        this.setState({formToggle: false})
    }

    render(){
        let form="";
        if(this.state.formToggle){
            if(Accessors.model(Context.cursor)){
                form=<Form onCancelClick={this.toggleModal.bind(this)}
                           FormAction={Controller.Update.bind(this, this.state.officeId)}
                           Title="Edit Department"/>;
            }else{
                form=<Form onCancelClick={this.toggleModal.bind(this)}
                           FormAction={Controller.Add.bind(this, this.state.officeId)}
                           Title="Add Department"/>;
            }
        }

        const items = Accessors.items(Context.cursor).map( (department, index) => {
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

            </div>
        )
    }
}