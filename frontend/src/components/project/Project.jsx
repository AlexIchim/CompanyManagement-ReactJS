import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import ProjectItem from './ProjectItem.jsx'
import Form from './Form.jsx';

export default class Project extends React.Component{
    constructor(){
        super();
        this.setState({
            form: false
        })
    }
    componentWillMount(){
        //const projectId = this.props.routeParams['projectId'];

        $.ajax({
            method: 'GET',
            url: config.base + 'department/projects/3',
            async: false,
            success: function(data){
                this.setState({
                    projects: data
                })
            }.bind(this)
        })
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
    render(){
        console.log('store?', this.state.form);
        const modal = this.state.form ? <Form show = {this.state.form} add={this.addElement.bind(this)} close = {this.closeForm.bind(this)}/> : '';


        const items = this.state.projects.map( (project, index) => {
            return (
                <ProjectItem
                    node = {project}
                    key = {index}
                    Link = {"project/members/" + project.Id}
                    />
            )
        });

        return (
            <div>

                {modal}

            <table className="table table-stripped">
                <thead>
                <h1> Projects <button id="store" className="btn btn-success margin-top" onClick={this.showForm.bind(this)}>
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