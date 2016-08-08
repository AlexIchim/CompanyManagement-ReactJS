import React from 'react';
import ModalTemplate from '../ModalTemplate';
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
        const name = model.Name;
        const duration = model.Duration;
        const status = model.Status;

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={MyController.Edit.bind(this)}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text"  ref="inputName" className="form-control"  placeholder="Name" value={name}>
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputAddress" className="col-sm-2 control-label"> Duration</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputDuration" placeholder="Duration" value={duration}>
                        </input>
                    </div>
                </div>

                <div className="col-md-4 col-md-4-custom">
                    <div className="box box-widget widget-user-2">
                        <div className="widget-user-header bg-yellow">
                            <div className="widget-user-image">
                                <img className="img-circle hoverZoomLink" src="../../../src/assets/less/themes/lte/img/full_logo.png" alt="User Avatar"></img>
                            </div>
                            <h3 className="widget-user-username">Nadia Carmichael</h3>
                            <h5 className="widget-user-desc">Lead Developer</h5>
                        </div>
                        <div className="box-footer no-padding">
                            <ul className="nav nav-stacked">
                                <li><a href="#">Projects <span className="pull-right badge bg-blue">31</span></a></li>
                                <li><a href="#">Tasks <span className="pull-right badge bg-aqua">5</span></a></li>
                                <li><a href="#">Completed Projects <span className="pull-right badge bg-green">12</span></a></li>
                                <li><a href="#">Followers <span className="pull-right badge bg-red">842</span></a></li>
                            </ul>
                        </div>
                    </div>
                </div>









            </ModalTemplate>
        )
    }

}