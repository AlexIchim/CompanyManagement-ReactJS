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
class EmployeeMembers extends React.Component {
    componentWillMount(){

        const employeeId = this.props.routeParams['employeeId'];
        console.log(employeeId);
        $.ajax({
            method: 'GET',
            url: config.base + 'employee/members/' +employeeId,
            async: false,
            success: function(data){
                this.setState({
                    employeeMembers: data
                })
            }.bind(this)
        });
        $.ajax({
            method: 'GET',
            url: config.base + 'employee/getById/' +employeeId,
            async: false,
            success: function(data){
                this.setState({
                    employeeName: data.Name
                })
            }.bind(this)
        });
    }

    render(){
        const items = this.state.employeeMembers.map ( (employeeMember, index) => {
            return (
                <Item
                    element = {employeeMember}
                    key = {index}
                    />
            )
        });
        return (
            <div>
                <h1> {this.state.employeeName} Members  <button id="store" className="btn btn-success margin-top" >
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

export default EmployeeMembers;