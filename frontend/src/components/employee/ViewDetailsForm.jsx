import React from 'react';
import ViewDetailsModal from '../modal/ViewDetailsModal.jsx';
import configs from '../helpers/calls';
import "./../../assets/less/index.less";

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
                    <div className="col-sm-6">
                         <div className="control-group">
                            <label className="col-sm-10 control-label"> Name: </label>
                            <label className=" controls readonly"> {this.props.element.get('Name')} </label>
                        </div>

                        <div className="control-group">
                            <label className="col-sm-10 control-label"> Address: </label>
                            <label className="controls readonly"> {this.props.element.get('Address')} </label>
                        </div>

                         <div className="control-group">
                            <label className="col-sm-10 control-label"> Position type: </label>
                            <label className=" controls readonly"> {this.props.element.get('PositionType')} </label>
                        </div>

                        <div className="control-group">
                            <label className="col-sm-10 control-label"> Job type: </label>
                            <label className=" controls readonly"> {this.props.element.get('JobType')} </label>
                        </div>

                        <div className="control-group">
                             <label className="col-sm-10 control-label"> Employment Date: </label>
                             <label className="controls readonly"> {this.props.element.get('EmploymentDate')} </label>
                        </div>
                       
                        <div className="control-group">
                            <label className="col-sm-10 control-label"> Release Date: </label>
                            <label className=" controls readonly"> {this.props.element.get('ReleaseDate')} </label>
                        </div>
                        </div>
                    

            <table className="table table-striped table-details table-custom" >
                <thead className="thead-custom">
                <tr>
                    <th className="col-sm-2">Project Name</th>
                    <th className="col-sm-2">Allocation</th>
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