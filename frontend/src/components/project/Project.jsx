import * as React from 'react'
import ProjectItem from './ProjectItem.jsx'
import Form from './form/Form.jsx';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import MyController from './controller/Controller'
import EditForm from './form/EditForm';

import '../../assets/less/index.less';

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
                <p className="table-name">Projects</p>
                <div className=" rectangle custom-rectangle-project">
                    <div className="glyphicon glyphicon-plus-sign custom-add-icon"
                         onClick={this.onAddButtonClick.bind(this)}>
                        <span className="add-span" onClick={this.onAddButtonClick.bind(this)}>Add Project</span>
                    </div>
                </div>
                
                <table className="table table-stripped">
                    <thead>
                <tr>
                    <td> </td>
                    <td> Project Name </td>
                    <td> Team members </td>
                    <td> Duration </td>
                    <td> Status</td>
                    <td> Wiews</td>
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