import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import * as Immutable from 'immutable';
import Context from '../../context/Context.js';
import * as Controller from '../controller';


export default class AddForm extends React.Component{

    constructor(){
        super();

    }

    store(cb){

         let inputInfo = {
             Name: this.refs.inputName.value,
             Address:this.refs.inputAddress.value,
             PhoneNumber:this.refs.inputPhone.value
         }

        $.ajax({
            method: "POST",
            url: configs.baseUrl + 'api/office/add',
            async: false,
            data: inputInfo,
            success: function(data){
                cb();
                this.refresh();
            }.bind(this)
        });


    }

    refresh(){
       Controller.getAllOffices();
    }


    render(){



        return(
            <div>

                <Modal title={'Add new office'} button={'Add'} close={this.props.close} action={this.store.bind(this)}>
                        <div className="form-group">
                            <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                            <div className="col-sm-10">
                                <input type="text" className="form-control" ref="inputName" name="Name" placeholder="Name">
                                </input>
                            </div>
                        </div>

                        <div className="form-group">
                            <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                            <div className="col-sm-10">
                                <input type="text" className="form-control" ref="inputAddress" name="Address" placeholder="Address">
                                </input>
                            </div>
                        </div>

                        <div className="form-group">
                            <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                            <div className="col-sm-10">
                                <input type="text" className="form-control" ref="inputPhone" name="PhoneNumber" placeholder="Phone">
                                </input>
                            </div>
                        </div>

                        <div className="form-group">
                            <label htmlFor="inputPhone" className="col-sm-2 control-label"> Image </label>
                            <div className="col-sm-10">
                                <input type="text" className="form-control" ref="inputImage" name="Image" placeholder="http://...">
                                </input>
                            </div>
                        </div>


                </Modal>


            </div>
        )
    }

}