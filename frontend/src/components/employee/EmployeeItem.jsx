import * as React from 'react';
import EditForm from './EditForm';
import "./../../assets/less/index.less";
import * as Controller from '../controller';
import ViewDetailsForm from './ViewDetailsForm';

export default class EmployeeItem extends React.Component{
    
    constructor(){
        super();
        this.state = {
            edit: false,
            viewDetails: false
        }
    }

    showEditForm(){       
        this.setState({
            edit: !this.state.edit
        })
    }

    closeEditForm(){
        this.setState({
            edit: !this.state.edit
        })
    }

    releaseEmployee(employeeId)
    {
        Controller.releaseEmployee(employeeId)
        this.refresh(this.props.departmentId)
    }

    refresh(departmentId)
    {
        this.props.setPageNr();
        Controller.getAllEmployeesByDepartmentId(departmentId,"",null,{},{},1)
    }

    showViewDetailsForm()
    {
         this.setState({
            viewDetails: !this.state.viewDetails
        })
    }

    closeViewDetailsForm()
    {
         this.setState({
            viewDetails: !this.state.viewDetails
        })
    }

    render(){

        const editModal = this.state.edit ? <EditForm setPageNr={this.props.setPageNr} index={this.props.index} departmentId={this.props.departmentId} element={this.props.node} show = {this.state.edit} close={this.closeEditForm.bind(this)} /> : '';
        const viewDetailsModal = this.state.viewDetails ? <ViewDetailsForm element={this.props.node} show = {this.state.viewDetails} close={this.closeViewDetailsForm.bind(this)} /> : '';
        const relDate = this.props.node.get('ReleaseDate') ? new Date(this.props.node.get('ReleaseDate')).toLocaleDateString() : " - ";
       
        return(

         
                <tr>
                    <td>{this.props.node.get('Name')}</td>
                    <td>{this.props.node.get('Address')}</td>
                    <td>{this.props.node.get('EmploymentDate')}</td>
                    <td>{relDate}</td>
                    <td>{this.props.node.get('JobType')}</td>
                    <td>{this.props.node.get('PositionType')}</td>
                    <td>{this.props.node.get('TotalAllocation')}</td>
                    <td><button className="linkButton" onClick={this.showViewDetailsForm.bind(this)}> View Details |</button>
                        {viewDetailsModal}
                        <button className="linkButton" onClick={this.releaseEmployee.bind(this,this.props.node.get('Id'))}> Release |</button>
                        <button className="linkButton" onClick={this.showEditForm.bind(this)}> Edit</button>
                        {editModal}
                    </td>
                </tr>            
        )
    }
}
