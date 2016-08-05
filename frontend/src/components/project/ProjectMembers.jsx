import * as React from 'react'
import config from '../helper';
import * as $ from 'jquery'

const Item = (props) => {
    return (
        <tr>
            <td>{props.element['Name']}</td>
            <td>{props.element['Position']}</td>
            <td>{props.element['Allocation']} %</td>
            <td><button id="store" className="btn btn-success margin-top">
                Edit Allocation
            </button>
            </td>
            <td>
                <button className="btn btn-danger margin-top"><i className="fa fa-trash" ></i>
                    Delete
                </button>
            </td>
        </tr>
    )
}
class ProjectMembers extends React.Component {
    componentWillMount(){

        const projectId = this.props.routeParams['projectId'];
        console.log(projectId);
        $.ajax({
            method: 'GET',
            url: config.base + 'project/members/' +projectId + '/5/1',
            async: false,
            success: function(data){
                this.setState({
                    projectMembers: data
                })
            }.bind(this)
        });
        $.ajax({
            method: 'GET',
            url: config.base + 'project/getById/' +projectId,
            async: false,
            success: function(data){
                this.setState({
                    projectName: data.Name
                })
            }.bind(this)
        });
    }

    render(){
        const items = this.state.projectMembers.map ( (projectMember, index) => {
            return (
                <Item
                    element = {projectMember}
                    key = {index}
                    />
            )
        });
        return (
            <div>
                <h1> {this.state.projectName} Members  <button id="store" className="btn btn-success margin-top" >
                    Assign Employee
                </button></h1>
            <table className="table table-stripped">

                <thead>

                <tr>
                    <td>
                       <h3> Employee Name </h3>
                    </td>
                    <td>
                        <h3>Role</h3>
                    </td>
                    <td>
                        <h3>Allocation</h3>
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

export default ProjectMembers;