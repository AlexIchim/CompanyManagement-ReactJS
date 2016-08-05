import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import ProjectItem from './ProjectItem.jsx'
import Form from './Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import Command from '../Command';
import MyController from './Controller/Controller'
import EditForm from './EditForm';

export default class Project extends React.Component{
    constructor(){
        super();
        this.setState({
            form: false
        })
    }
    componentWillMount(){
        Context.subscribe(this.onContextChange.bind(this));
        //const projectId = this.props.routeParams['projectId'];

        MyController.getAllProjects();
    }

    onContextChange(cursor){
        console.log('projects:', cursor.get('items'));
        this.setState({
            projects: cursor.get('items')
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

    onEditButtonClick(project){
        Context.cursor.set('formToggle', true);
        Context.cursor.set('model', project)
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
                           Title="Edit Project"/>;
            }else{
                modal=<Form onCancelClick={Command.hideModal.bind(this)}
                           onStoreClick={this.onModalSaveClick.bind(this)}
                           Title="Add Project"/>;
            }
        }
        const items = this.state.projects.map( (project, index) => {
            return (
                <ProjectItem
                    node = {project}
                    key = {index}
                    Link = {"project/members/" + project.Id}
                    onEdit = {this.onEditButtonClick.bind(this, project)}
                    onDelete = {this.onDeleteButtonClick.bind(this, project)}
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