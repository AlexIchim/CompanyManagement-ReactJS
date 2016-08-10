import React from 'react';
import ViewDetailsModal from '../modal/ViewDetailsModal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
//import * as $ from 'jquery';

export default class ViewDetailsForm extends React.Component{
    
    constructor(){
        super();
        this.state={
            showForm:false,
            projects:[]
        }
    }

     componentWillMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getAllProjectsOfAnEmployee?employeeId=' + this.props.element.get('Id'),
            success: function (data) {
                this.setState({
                    projects: data
                })
            }.bind(this)
        })
     }

     render()
     {
         console.log(this.state.projects)

                const items = this.state.projects.map( (element, index) => {
        return(
            <tr key = {index}>
                

               <td>{element.Name}</td>
               <td>{element.Allocation}</td>

            </tr>
        )

    });
         return(
             <div>
                <ViewDetailsModal title={this.props.element.get('Name')} close={this.props.close}>
                 <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <label className="col-sm-4 control-label"> {this.props.element.get('Name')} </label>
                
            </div>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Address </label>
                 <label className="col-sm-4 control-label"> {this.props.element.get('Address')} </label>
              
            </div>
           
             <div className="form-group">
                <label className="col-sm-4">Employment Date:</label>
                 <label className="col-sm-4 control-label"> {this.props.element.get('EmploymentDate')} </label>
            </div>
                
            <div className="form-group">
                <label className="col-sm-4">Release Date:</label>
                 <label className="col-sm-4 control-label"> {this.props.element.get('ReleaseDate')} </label>
              </div>
                <div className="form-group">
                <label className="col-sm-4">Job type: </label>
                 <label className="col-sm-4 control-label"> {this.props.element.get('JobType')} </label>
              </div>
                <div className="form-group">
                <label className="col-sm-4">Position type:</label>
                 <label className="col-sm-4 control-label"> {this.props.element.get('PositionType')} </label>
              </div>

      

            <table className="table table-condensed" id="table1">
                <thead>
                <tr>
                    <th className="col-md-2">Project Name</th>
                    <th className="col-md-2">Allocation</th>
                </tr>
                </thead>
                <tbody>
                    {items}
                </tbody>
            </table>


                </ViewDetailsModal>

             </div>



         )
     }
}