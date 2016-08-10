import * as React from 'react';
import ModalTemplate from '../ModalTemplate';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';
import config from '../helper';
import Validator from '../validator/OfficeValidator';

export default class Form extends React.Component{
    constructor(){
        super();
    }
    
    componentWillMount(){
        this.setState({
            Image:null,
            NameValidationResult:{valid: false, message: ""},
            PhoneValidationResult:{valid: false, message: ""},
            AddressValidationResult:{valid: false, message: ""},
            ImageValidationResult:{valid: false, message: ""}
        })
    }

    onImageLoad(){
       var value=document.querySelector('input[type=file]').files[0];

        var reader=new FileReader();
        var image;
        var that = this;
        reader.onload=function(event){
            image=this.result;

            var result=Validator.ValidateImage(image); 

            that.updateState(image, null, null, null, result);
            console.log("Finished Loading Image");
        }

        reader.readAsDataURL(value);
    }

    onStoreClick(){
        this.onChangeName();
        this.onChangeAddress();
        this.onChangePhone();

        if(     this.state.NameValidationResult.valid
            &&  this.state.AddressValidationResult.valid
            &&  this.state.PhoneValidationResult.valid  )
        {
            let model=Context.cursor.get("model");

            if(!model){
                model={};
            }
            let name=this.refs.inputName.value;
            let addr=this.refs.inputAddress.value;
            let phone=this.refs.inputPhone.value;
            let image=this.state.Image;

            model.Name=(name)?name:model.Name;
            model.Address=(addr)?addr:model.Address;
            model.Phone=(phone)?phone:model.Phone;
            model.Image=(image)?image:model.Image;

            Context.cursor.set("model", model);

            this.props.FormAction();
        }
    }

    onChangeName(){
        var result=Validator.ValidateName(this.refs.inputName.value);

        this.updateState(null, result, null, null, null);
    }
    onChangeAddress(){
        var result=Validator.ValidateAddress(this.refs.inputAddress.value);

        this.updateState(null, null, result, null, null);
    }
    onChangePhone(){
        var result=Validator.ValidatePhone(this.refs.inputPhone.value);

        this.updateState(null, null, null, result, null);
    }
    updateState(img, nameVR, phoneVR, addrVR, imgVR){
        this.setState({
            Image:                      (img)?      img:        this.state.Image,
            NameValidationResult:       (nameVR)?   nameVR:     this.state.NameValidationResult,
            PhoneValidationResult:      (phoneVR)?  phoneVR:    this.state.PhoneValidationResult,
            AddressValidationResult:    (addrVR)?   addrVR:     this.state.AddressValidationResult,
            ImageValidationResult:      (imgVR)?    imgVR:      this.state.ImageValidationResult
        });
    }

    render(){
        const model=Context.cursor.get('model');
        const name=(model)? model.Name : "Name";
        const addr=(model)? model.Address : "Address";
        const phone=(model)? model.Phone : "Phone";

        let nameValidationResult="";
        let addrValidationResult="";
        let phoneValidationResult="";
        let imgValidationResult="";
        

        if(!this.state.NameValidationResult.valid){
            nameValidationResult=<span>{this.state.NameValidationResult.message}</span>;
        }

        if(!this.state.AddressValidationResult.valid){
            addrValidationResult=<span>{this.state.AddressValidationResult.message}</span>;
        }
        if(!this.state.PhoneValidationResult.valid){
            phoneValidationResult=<span>{this.state.PhoneValidationResult.message}</span>;
        }
        if(!this.state.ImageValidationResult.valid){
            imgValidationResult=<span>{this.state.ImageValidationResult.message}</span>;
        }

        var formIsValid=false;
        if(    this.state.NameValidationResult.valid 
            && this.state.AddressValidationResult.valid
            && this.state.PhoneValidationResult.valid
            && this.state.ImageValidationResult.valid){
            formIsValid=true;
        
        }

        return(
            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
                           Title={this.props.Title}
                           formIsValid={formIsValid}  
                           >

                    <div className="form-group">
                        <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                        <div className="col-sm-10">
                            <input type="text" 
                                className="form-control" 
                                ref="inputName" 
                                placeholder={name}
                                onKeyUp={this.onChangeName.bind(this)}>
                            </input>
                            {nameValidationResult}
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                        <div className="col-sm-10">
                            <input  type="text" 
                                    className="form-control" 
                                    ref="inputAddress" 
                                    placeholder={addr}
                                    onChange={this.onChangeAddress.bind(this)}>
                            </input>
                            {addrValidationResult}
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                        <div className="col-sm-10">
                            <input type="text" 
                                className="form-control" 
                                ref="inputPhone" 
                                placeholder={phone}
                                onKeyUp={this.onChangePhone.bind(this)}>
                            </input>
                            {phoneValidationResult}
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="inputImage" className="col-sm-2 control-label"> Image </label>
                        <div className="col-sm-10">
                            <input
                                type="file" 
                                ref="inputImage" 
                                accept="image/*" data-type='image'
                                onChange={this.onImageLoad.bind(this)}/>
                        </div>
                        {imgValidationResult}
                    </div>
                    
            </ModalTemplate>
        )
    }
}