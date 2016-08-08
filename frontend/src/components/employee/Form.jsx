import React from 'react';
import ModalTemplate from '../ModalTemplate';
import Controller from '../Command';
import config from '../helper';
import MyController from './Controller/Controller.js';
import Context from '../../context/Context';

export default class Form extends React.Component {
    constructor(){
        super();
    }

    componentWillMount(){
        Context.subscribe(this.onContextChange.bind(this));
    }

    onContextChange(newGlobalCursor){
        this.setState({
            model: newGlobalCursor.get('model'),
            employeeName: newGlobalCursor.get('model') && newGlobalCursor.get('model').Name || ''
        })
    }

    onModelChange(){
        this.setState({
            model: this.state.model,
            employeeName: this.refs.inputName.value
        })
    }

    render(){

        const employeeName = this.state.employeeName;

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={MyController.Add.bind(this)}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text"  ref="inputName" className="form-control" onChange={this.onModelChange.bind(this)} value={employeeName} placeholder="Name" >
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