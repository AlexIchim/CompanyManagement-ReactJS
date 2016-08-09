import React from 'react';
import ViewDetailsModalTemplate from '../ViewDetailsModalTemplate';
import Controller from '../Command';
import config from '../helper';
import MyController from './Controller/Controller.js';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';
import '../../assets/less/index.less'
export default class EditForm extends React.Component {
    constructor(){
        super();
    }
    render(){
        const model = Accessors.model(Context.cursor);
        const projects = model.Projects;
        const name = model.Name;
        const address = model.Address;
        const employmentDate = model.EmploymentDate;
        const releasedDate = model.ReleasedDate;
        const jobType = model.JobType;
        const position = model.Position;
        const allocation = model.Allocation;

        return(

            <ViewDetailsModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={MyController.Edit.bind(this)}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="col-md-4 col-md-4-custom">
                    <div className="box box-widget widget-user-2">
                        <div className="widget-user-header bg-yellow">
                            <div className="widget-user-image">
                                <img className="img-circle hoverZoomLink" src="../../../src/assets/less/themes/lte/img/full_logo.png" alt="User Avatar"></img>
                            </div>
                            <h3 className="widget-user-username">{name}</h3>
                            <h5 className="widget-user-desc">{position}</h5>
                        </div>
                        <div className="box-footer no-padding">
                            <ul className="nav nav-stacked">
                                <li><a href="#">Projects <span className="pull-right badge bg-aqua">{projects}</span></a></li>
                                <li><a href="#">Allocation <span className="pull-right badge bg-green">{allocation}</span></a></li>
                                <li><a href="#">Address <span className="pull-right badge bg-blue">{address}</span></a></li>
                                <li><a href="#">Job Type <span className="pull-right badge bg-red">{jobType}</span></a></li>
                                <li><a href="#">Employment Date <span className="pull-right badge bg-red">{employmentDate}</span></a></li>
                                <li><a href="#">Termination Date <span className="pull-right badge bg-red">{releasedDate}</span></a></li>
                            </ul>
                        </div>
                    </div>
                </div>

            </ViewDetailsModalTemplate>
        )
    }

}