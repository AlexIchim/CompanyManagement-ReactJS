import React from 'react';
import ModalTemplate from '../ModalTemplate';
import Controller from '../Controller';
import config from '../helper';
export default class Form extends React.Component {
    constructor(){
        super();
    }

    onStoreClick(){
        $.ajax({
            method: 'POST',
            url: config.base + 'project/add',
            data: {
                Name:  this.refs.inputName.value,
                DepartmentId: 3,
                Duration: this.refs.inputDuration.value,
                Status: "NotStartedYet"
            },
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        Controller.hideModal();
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