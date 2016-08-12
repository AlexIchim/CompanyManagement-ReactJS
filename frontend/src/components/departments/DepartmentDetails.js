import $ from 'jquery';
import React from 'react';
import {Link} from 'react-router';
import * as Command from '../../context/commands';
import * as Controller from '../../api/controller';

export default class DepartmentDetails extends React.Component {

    constructor(){
        super();
        this.state = {
            departmentName: ''
        }
    }

    componentWillReceiveProps(newProps){
        this.props = newProps;
        this.reinit();
    }

    componentWillMount(){
        this.reinit();
    }

    reinit(){
        Command.setCurrentOffice(this.props.params.officeId);
        Command.setCurrentDepartment(this.props.params.departmentId);

        Controller.getDepartmentById(
            this.props.params.departmentId,
            true,
            (data) => {
                this.setState({
                    departmentName: data.name
                });
            }
        )
    }

    render(){

        const linkProjects = "offices/" + this.props.params.officeId + "/departments/" + this.props.params.departmentId  + "/projects";
        const linkEmployees = "offices/" + this.props.params.officeId +  "/departments/" + this.props.params.departmentId  + "/employees";

        return(
            <div>
                <h1>{this.state.departmentName} Department</h1>
                <br/>
                <div className="row">
                    <div className="col-md-3 col-sm-3 col-xs-12">
                        <span className="info-box-icon projectsSpan"> <i className=" fa fa-folder"/> </span>
                        <Link to={linkProjects} >
                            <button className="btn btn-md btn-default">
                            Projects <i className="fa fa-arrow-circle-right"></i>
                            </button>
                        </Link>
                    </div>

                    <div className="col-md-3 col-sm-3 col-xs-12">
                        <span className="info-box-icon projectsSpan"><i className="fa fa-users"/> </span>
                        <Link to={linkEmployees} >
                            <button className="btn btn-md btn-default">
                             Employees <i className="fa fa-arrow-circle-right"></i>
                            </button>
                        </Link>
                    </div>
                </div>
            </div>
        )
    }
}