import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import ProjectItem from './ProjectItem.jsx'
import Form from './form/Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import MyController from './controller/Controller'
import EditForm from './form/EditForm';
import Delete from './controller';
import GetAllProjects from './controller/GetAllProjects'


export default class Project extends React.Component{
    constructor(){
        super();
    }
    componentWillMount(){
        this.setState({
            formToggle:false
        });
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
        //const projectId = this.props.routeParams['projectId'];
        Context.cursor.set("items",[]);
        MyController.GetAllProjects();

    }

    componentWillUnmount(){
        this.subscription.dispose();
    }

    onContextChange(cursor){
        this.setState({
            items: Accessors.items(Context.cursor),
            formToggle: false
        });
    }

    onAddButtonClick(){
        Context.cursor.set('model',null);
        this.setState({
            formToggle: true
        });
    }

    onEditButtonClick(project){
        Context.cursor.set('model', project)
        this.setState({
            formToggle: true
        });

    }

    toggleModal(){
        this.setState({
            formToggle: false
        });
        console.log('toggle modal?', this.state.formToggle);
    }

    render(){
        let modal = "";
        if(this.state.formToggle) {
            if (Accessors.model(Context.cursor)) {
                modal = <EditForm onCancelClick={this.toggleModal.bind(this)}
                                  FormAction={MyController.Update}
                                  Title="Edit Project"/>;
            }
            else {
                modal = <Form onCancelClick={this.toggleModal.bind(this)}
                              FormAction={MyController.Add}
                              Title="Add Project"/>;
            }
        }
        const items =this.state.items.map( (project, index) => {
            return (
                <ProjectItem
                    node = {project}
                    key = {index}
                    Link = {"project/members/" + project.Id}
                    onEdit = {this.onEditButtonClick.bind(this, project)}
                    onDelete = {MyController.Delete.bind(this, project)}
                    />
            )
        });

        return (
            <div>

                {modal}

            <table className="table table-stripped">
                <thead>
                <h1> Projects <button id="store" className="btn btn-success margin-top" onClick={this.onAddButtonClick.bind(this)}>
                    Add New Project
                </button></h1>

                <tr>
                    <td><h3> Project Name </h3></td>
                    <td><h3> Team members </h3></td>
                    <td><h3> Duration</h3></td>
                    <td><h3> Status</h3></td>

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