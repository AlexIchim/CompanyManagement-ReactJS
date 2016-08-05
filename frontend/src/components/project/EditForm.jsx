import React from 'react';
import ModalTemplate from '../ModalTemplate';
import Controller from '../Command';
import config from '../helper';
import MyController from './controller/Controller.js';
import Accessors from '../../context/Accessors';
import Context from '../../context/Context';

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



                <div className="form-group">
                    <select className="form-" Choose />
                </div>



            </ModalTemplate>
        )
    }

}