import * as React from 'react'
import config from '../helper';
import * as $ from 'jquery'
import Controller from './controller/Controller';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors'
import Form from './form/Form';
import EditAllocationForm from './form/EditAllocationForm'
import AssignEmployeeForm from './form/AssignEmployeeForm'
import '../../assets/less/index.less';

const Item = (props) => {
    const lft = {
        float: "left",
        margin: '0px 0px 0px 5px'
    };
    return (
        <tr className="table-tr">
            <td className="td-actions">
                <div className="glyphicon glyphicon-trash custom-delete-icon" onClick={props.onDelete}>

                </div>
            </td>
            <td>{props.element['Name']}</td>
            <td>{props.element['Position']}</td>
            <td><span style={lft}>{props.element['Allocation']} %</span>
                <span className="glyphicon glyphicon-edit custom-edit-icon" style={lft}
                     onClick={props.onEdit}>
                </span>
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
                modal = <AssignEmployeeForm
                              onCancelClick={this.toggleModal.bind(this)}
                              ProjectId = {this.state.projectId}
                              FormAction={Controller.Add}
                              Title="Add Project"/>;

            }
        }
        return (
            <div>
                {modal}
                <p className="table-name">Project Members</p>
                <div className=" rectangle custom-rectangle-project-member">
                    <div className="glyphicon glyphicon-plus-sign custom-add-icon"
                         onClick={this.onAddButtonClick.bind(this)}>
                        <span className="add-span" onClick={this.onAddButtonClick.bind(this)}>Assign Employee</span>
                    </div>
                </div>
            <table className="table table-stripped">

                <thead>

                <tr>
                    <td> </td>
                    <td> Employee Name </td>
                    <td>Role</td>
                    <td>Allocation</td>
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