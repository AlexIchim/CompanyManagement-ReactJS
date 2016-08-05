import React from 'react';
import ModalTemplate from '../ModalTemplate';
import config from '../helper';
import MyController from './controller/Controller.js';
import Context from '../../context/Context';

export default class Form extends React.Component {
    constructor(){
        super();
    }

    onStoreClick(){
        let model = Context.cursor.get('model');

        if(!model){
            model = {}
        }

        let name = this.refs.inputName.value;
        let duration = this.refs.inputDuration.value;

        model.Name = (name) ? name : model.Name;
        model.Duration = (duration) ? duration : model.Duration;

        Context.cursor.set("model", model);

        this.props.FormAction();
    }
    render(){
        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
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
                    <label htmlFor="inputDuration" className="col-sm-2 control-label"> Duration</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputDuration" placeholder="Duration" >
                        </input>
                    </div>
                </div>



            </ModalTemplate>
        )
    }

}