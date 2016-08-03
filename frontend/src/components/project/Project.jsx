import config from '../helper';
import * as React from 'react'
import * as $ from 'jquery'
import ProjectItem from './ProjectItem.jsx'

export default class Project extends React.Component{
    constructor(){
        super();
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

    render(){
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

            <table className="table table-stripped">
                <thead>
                <h1> Projects <button id="store" className="btn btn-success margin-top" >
                    Add New Project
                </button></h1>

                <tr>
                    <td>
                        <h3> Project Name </h3>
                    </td>
                    <td>
                        <h3> Team members </h3>
                    </td>
                    <td>
                        <h3>Duration</h3>
                    </td>
                    <td>
                        <h3>Status</h3>
                    </td>

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