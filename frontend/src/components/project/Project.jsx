import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import ProjectItem from './ProjectItem.jsx'
import Form from './Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import MyController from './controller/Controller'
import EditForm from './EditForm';
import Delete from './controller';
import GetAllProjects from './controller/GetAllProjects'


export default class Project extends React.Component{
    constructor(){
        super();
    }
    componentWillMount(){
        this.setState({
            formToggle:false,
            form: false
        });
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
        //const projectId = this.props.routeParams['projectId'];

        MyController.GetAllProjects();
    }

    componentWillUnmount(){
        this.subscription.dispose();
    }

    onContextChange(cursor){
        this.setState({
            formToggle: false
        });

    }

    onAddButtonClick(){
        Context.cursor.set('formToggle',true);
    }

    onEditButtonClick(project){
        Context.cursor.set('formToggle', true);
        Context.cursor.set('model', project)
    }


    hideModal(){
        Context.cursor.set('formToggle',false);
        Context.cursor.set('model',null);
        this.setState({
            formToggle: true
        });
    }
    toggleModal(){
        this.setState({formToggle: false})
    }

    render(){
        let modal = "";
        if(Accessors.formToggle(Context.cursor)){
            if(Accessors.model(Context.cursor)){
                modal=<EditForm onCancelClick={this.toggleModal.bind(this)}
                                FormAction={MyController.Update}
                           Title="Edit Project"/>;
            }else{
                modal=<Form onCancelClick={this.toggleModal.bind(this)}
                            FormAction={MyController.Add}
                           Title="Add Project"/>;
            }
        }

        const items = Accessors.items(Context.cursor).map( (project, index) => {
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