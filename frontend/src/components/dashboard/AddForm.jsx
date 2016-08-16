import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import * as Immutable from 'immutable';
import Context from '../../context/Context.js';
import * as Controller from '../controller';
import ValidateOffice from '../validators/ValidateOffice.js';


export default class AddForm extends React.Component{

    constructor(){
        super();
        this.state={
            errors:{
                NameErrors:[],
                AddressErrors:[],
                PhoneNumberErrors:[]
            }
        }
    }

    checkErrors()
    {
        if (this.state.errors.NameErrors.length==0 && this.state.errors.AddressErrors.length == 0 && this.state.errors.PhoneNumberErrors.length == 0){
             return true
        }
           
        return false
    }

    store(cb){
         if (this.checkErrors() == true)
        {
         let inputInfo = {
             Name: this.refs.inputName.value,
             Address:this.refs.inputAddress.value,
             PhoneNumber:this.refs.inputPhone.value,
             Image:this.refs.inputImage.value
         }
       
            $.ajax({
                method: "POST",
                url: configs.baseUrl + 'api/office/add',
                async: false,
                data: inputInfo,
                success: function(data){
                    if (data.Success == true)
                    {
                      cb();
                    this.refresh();}
                    else
                        alert("Invalid input!")
                }.bind(this)
        })}
        else 
        {
            alert("Invalid input!")
        }

    }

    refresh(){
       Controller.getAllOffices();
    }

    onChangeName()
    {
        const errors = ValidateOffice.validateName(this.refs.inputName.value)
        this.state.errors.NameErrors = errors
       
         this.setState({
             errors: this.state.errors
         })
    }

    onChangeAddress()
    {
        const errors = ValidateOffice.validateAddress(this.refs.inputAddress.value)
        this.state.errors.AddressErrors = errors
       
         this.setState({
             errors: this.state.errors
         })
    }

    onChangePhoneNumber()
    {
        const errors = ValidateOffice.validatePhoneNumber(this.refs.inputPhone.value)
        this.state.errors.PhoneNumberErrors = errors
       
         this.setState({
             errors: this.state.errors
         })
    }

    render(){



        return(
            <div>

                <Modal title={'Add new office'} button={'Add'} close={this.props.close} action={this.store.bind(this)}>
                        <div className="form-group">
                            <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                            <div className="col-sm-10">
                                <div className="col-sm-10 red">
                                    {this.state.errors.NameErrors}
                                </div>
                                <input type="text" className="form-control" ref="inputName" name="Name" placeholder="Name" onKeyUp={this.onChangeName.bind(this)}>
                                </input>
                            </div>
                        </div>

                        <div className="form-group">
                            <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                            <div className="col-sm-10">
                                <div className="col-sm-10 red">
                                    {this.state.errors.AddressErrors}
                                </div>
                                <input type="text" className="form-control" ref="inputAddress" name="Address" placeholder="Address" onKeyUp={this.onChangeAddress.bind(this)}>
                                </input>
                            </div>
                        </div>

                        <div className="form-group">
                            <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                            <div className="col-sm-10">
                                <div className="col-sm-10 red">
                                    {this.state.errors.PhoneNumberErrors}
                                </div>
                                <input type="text" className="form-control" ref="inputPhone" name="PhoneNumber" placeholder="Phone" onKeyUp={this.onChangePhoneNumber.bind(this)}>
                                </input>
                            </div>
                        </div>

                        <div className="form-group">
                            <label htmlFor="inputImage" className="col-sm-2 control-label"> Image </label>
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