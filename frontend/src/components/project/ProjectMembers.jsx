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
        const projectId = this.props.routeParams['projectId'];
        Controller.GetProjectMembers(projectId, 1);
        Controller.GetNrMembers(projectId);
        $.ajax({
            method: 'GET',
            url: config.base + 'project/getById/' +projectId,
            async: false,
            success: function(data){
                this.setState({
                    formToggle: false,
                    currentPage: 1,
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
            formToggle: false,
            totalNumberOfItems: Accessors.totalNumberOfItems(Context.cursor)
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

    onPreviousButtonClick(){
        let currentPage = this.state.currentPage;
        let newCurrentpage = currentPage - 1;
        if (currentPage > 1){
            this.setState({
                currentPage: newCurrentpage
            })
            Controller.GetProjectMembers(this.state.projectId, newCurrentpage);
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
            Controller.GetProjectMembers(this.state.projectId, newCurrentpage);
        }
    }
    onGoToFirstPageButtonClick(){
        this.setState({
            currentPage: 1
        })
        Controller.GetProjectMembers(this.state.projectId, 1);
    }

    onGoToLastPageButtonClick(){
        let numberOfPages = Math.ceil((this.state.totalNumberOfItems)/5);
        this.setState({
            currentPage: numberOfPages
        });
        Controller.GetProjectMembers(this.state.projectId, numberOfPages);
    }
    render(){
        const totalNumberOfItems = this.state.totalNumberOfItems;
        console.log('total numberrr: ', this.state.totalNumberOfItems);

        const numberOfPages = (totalNumberOfItems == 0) ? 1 : Math.ceil(totalNumberOfItems/5);
        console.log('nrOfPages', totalNumberOfItems);

        const currentPage = this.state.currentPage;
        let modal = "";
        let label =  currentPage + "/" + numberOfPages;

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

export default ProjectMembers;