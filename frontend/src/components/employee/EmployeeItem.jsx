import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';
import Form from './Form';
//import EditForm from './EditForm';
import "./../../assets/less/index.less";

export default class EmployeeItem extends React.Component{
    
    constructor(){
        super();
        this.state = {
            edit: false
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


    render(){
        //const linkEmployees = "department/" + this.props.node.get('Id')  + '/' + this.props.node.get('Name') + "/employees";
        //const linkProjects = "department/" + this.props.node.get('Id')  + '/' + this.props.node.get('Name') + "/projects";

        //const editModal = this.state.edit ? <EditForm officeId={this.props.officeId} element={this.props.node} show = {this.state.edit} close={this.closeEditForm.bind(this)} /> : '';

        return(

         
                <tr>
                <td>{this.props.node.get('Name')}</td>
                <td>{this.props.node.get('Address')}</td>
                <td>{this.props.node.get('EmploymentDate')}</td>
                <td>{this.props.node.get('ReleaseDate')}</td>
                <td>{this.props.node.get('JobType')}</td>
                <td>{this.props.node.get('PositionType')}</td>
                <td>{this.props.node.get('TotalAllocation')}</td>
               
                </tr>
           

                
        )
    }
}
