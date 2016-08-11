import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import * as Immutable from 'immutable';
import Context from '../../context/Context.js';
import * as $ from 'jquery';
import ValidateOffice from '../validators/ValidateOffice.js';

export default class Form extends React.Component{
    
    constructor(){
        super();
        this.state={
            office: {    
            },
            errors:{
                NameErrors:[],
                AddressErrors:[],
                PhoneNumberErrors:[]
            }
        }
    }

    componentWillMount(){
  
        this.setState({
            office: this.props.element
        })
    }

    changeData(){
    
        const name = this.refs.inputName.value;
        const address = this.refs.inputAddress.value;
        const phone = this.refs.inputPhone.value;
        const image = this.refs.inputImage.value;


        const newO = {
            Id: this.state.office.get('Id'),
            Name:name,
            Address:address,
            PhoneNumber:phone,
            Image:image
        }


        this.setState({
            office: Immutable.fromJS(newO)
        })
               
    }
    
    edit(cb){

        $.ajax({
            method: 'PUT',
            url: configs.baseUrl + 'api/office/updateOffice',
            data: this.state.office.toJS(),
            success: function(data){
                   const index= Context.cursor.get('offices').indexOf(this.props.element)
                   Context.cursor.get('offices').update( index,  oldInstance => {
                        oldInstance=this.state.office
                        return oldInstance;
                    });

                   cb();
            }.bind(this)
        })
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

        <Modal title={'Edit office'} button={'Edit'} action={this.edit.bind(this)} close={this.props.close}>

            <div className="form-group">
                <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                <div className="col-sm-10">
                    {this.state.errors.NameErrors}
                    <input type="text" className="form-control" ref="inputName" placeholder="Name" value={this.state.office.get('Name') || ''} onChange={this.changeData.bind(this)} onKeyUp={this.onChangeName.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                <div className="col-sm-10">
                    {this.state.errors.AddressErrors}
                    <input type="text" className="form-control" ref="inputAddress" placeholder="Address" value={this.state.office.get('Address') || ''} onChange={this.changeData.bind(this)} onKeyUp={this.onChangeAddress.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                <div className="col-sm-10">
                    {this.state.errors.PhoneNumberErrors}
                    <input type="text" className="form-control" ref="inputPhone" placeholder="Phone" value={this.state.office.get('PhoneNumber') || ''} onChange={this.changeData.bind(this)} onKeyUp={this.onChangePhoneNumber.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputImage" className="col-sm-2 control-label"> New Image </label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputImage" name="Image" placeholder="http://..." onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>


        </Modal>
        )
    }
    
    
}