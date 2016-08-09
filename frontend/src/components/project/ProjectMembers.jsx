import * as React from 'react'
import config from '../helper';
import * as $ from 'jquery'
import Controller from './controller/Controller';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors'
import Form from './form/Form';
import EditAllocationForm from './form/EditAllocationForm'
import AssignEmployeeForm from './form/AssignEmployeeForm'


const Item = (props) => {
    return (
        <tr>
            <td>{props.element['Name']}</td>
            <td>{props.element['Position']}</td>
            <td>{props.element['Allocation']} %</td>
            <td><button id="store" className="btn btn-success margin-top" onClick={props.onEdit}>
                Edit Allocation
            </button>
            </td>
            <td>
                <button className="btn btn-danger margin-top" onClick={props.onDelete}><i className="fa fa-trash" ></i>
                    Delete
                </button>
            </td>
        </tr>
    )
}
class ProjectMembers extends React.Component {

    constructor(){
        super();
    }
    componentWillMount(){
        this.setState({
            formToggle: false
        });
        const projectId = this.props.routeParams['projectId'];
        Controller.GetProjectMembers(projectId);
        $.ajax({
            method: 'GET',
            url: config.base + 'project/getById/' +projectId,
            async: false,
            success: function(data){
                this.setState({
                    projectId: data.Id,
                    projectName: data.Name
                })
            }.bind(this)
        });


        this.subscription = Context.subscribe(this.onContextChange.bind(this));

    }
    componentWillUnmount(){
        this.subscription.dispose();
    }
    onContextChange(cursor){
        this.setState({
            projectMembers: Accessors.items(Context.cursor),
            formToggle: false
        });
    }

    onAddButtonClick(){
        console.log('clicked add');
        Context.cursor.set('model',null);
        this.setState({
            formToggle: true
        });
    }
    onEditAllocationClick(projectMember){
        Context.cursor.set('model', projectMember)
        this.setState({
            formToggle: true
        });
    }

    toggleModal(){
        this.setState({formToggle: false})
    }
    render(){
        let modal = "";

        const items = this.state.projectMembers.map ( (projectMember, index) => {
            return (
                <Item
                    element = {projectMember}
                    onEdit = {this.onEditAllocationClick.bind(this, projectMember)}
                    onDelete = {Controller.DeleteAssignment.bind(this, projectMember.Id, this.state.projectId)}
                    key = {index}
                    FormAction={Controller.EditAllocation.bind(this, projectMember, this.state.projectId)}
                    />
            )
        });
        if(this.state.formToggle) {
            if (Accessors.model(Context.cursor)) {
                modal = <EditAllocationForm
                                  onCancelClick={this.toggleModal.bind(this)}
                                      FormAction={Controller.EditAllocation.bind(this, this.state.projectId)}
                                  Title="Edit Project Allocation"/>;
            } else {
                modal = <AssignEmployeeForm onCancelClick={this.toggleModal.bind(this)}
                              FormAction={Controller.Add}
                              Title="Add Project"/>;

            }
        }
        return (
            <div>
                {modal}
                <h1> {this.state.projectName} Members  <button id="store" className="btn btn-success margin-top" onClick={this.onAddButtonClick.bind(this)} >
                    Assign Employee
                </button></h1>
            <table className="table table-stripped">

                <thead>

                <tr>
                    <td><h3> Employee Name </h3></td>
                    <td><h3>Role</h3></td>
                    <td><h3>Allocation</h3></td>
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

export default ProjectMembers;