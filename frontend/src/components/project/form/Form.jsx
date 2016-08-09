import React from 'react';
import ModalTemplate from '../../ModalTemplate';
import config from '../../helper';
import MyController from '../controller/Controller.js';
import Context from '../../../context/Context';

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
        var select = document.getElementById('dropdown');
        var periodOfTime = select.options[select.selectedIndex].value;
        duration += " ";
        duration += periodOfTime;
        console.log('duration chosen :', duration);



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
                    <select id="dropdown" className="selectpicker">
                        <option >weeks</option>
                        <option>months</option>
                        <option>years</option>
                    </select>

                    <div className="col-sm-3">
                        <input type="number" className="form-control" ref="inputDuration" placeholder="Number" >
                        </input>
                    </div>
                </div>



            </ModalTemplate>
        )
    }

}