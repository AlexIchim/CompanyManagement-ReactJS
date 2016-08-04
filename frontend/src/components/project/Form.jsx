import React from 'react';
import ModalTemplate from '../ModalTemplate';
import Controller from '../Command';
import config from '../helper';
import MyController from './Controller/Controller.js';

export default class Form extends React.Component {
    constructor(){
        super();
    }
    render(){
        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={MyController.onStoreClick.bind(this)}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text"  ref="inputName" className="form-control"  placeholder="Name" >
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputAddress" className="col-sm-2 control-label"> Duration</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputDuration" placeholder="Duration" >
                        </input>
                    </div>
                </div>



            </ModalTemplate>
        )
    }

}