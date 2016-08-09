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
            that.setState({ Image: image });
            
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
        }else{
            this.setState({
            Image:this.state.Image,
                NameValidationResult:this.state.NameValidationResult,
                PhoneValidationResult:this.state.PhoneValidationResult,
                AddressValidationResult:this.state.AddressValidationResult,
                ImageValidationResult:this.state.ImageValidationResult,
                ValidationSummary:"No idea what message to add here"
            })
        }
    }

    onChangeName(){
        var result=Validator.ValidateName(this.refs.inputName.value);
        this.setState({
            Image:this.state.Image,
            NameValidationResult:result,
            PhoneValidationResult:this.state.PhoneValidationResult,
            AddressValidationResult:this.state.AddressValidationResult,
            ImageValidationResult:this.state.ImageValidationResult
        })
    }
    onChangeAddress(){
        var result=Validator.ValidateAddress(this.refs.inputAddress.value);
        this.setState({
            Image:this.state.Image,
            NameValidationResult:this.state.NameValidationResult,
            PhoneValidationResult:this.state.PhoneValidationResult,
            AddressValidationResult:result,
            ImageValidationResult:this.state.ImageValidationResult
        })
    }
    onChangePhone(){
        var result=Validator.ValidatePhone(this.refs.inputPhone.value);
        this.setState({
            Image:this.state.Image,
            NameValidationResult:this.state.NameValidationResult,
            PhoneValidationResult:result,
            AddressValidationResult:this.state.AddressValidationResult,
            ImageValidationResult:this.state.ImageValidationResult
        })
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
        let summary=""
        

        if(this.state.NameValidationResult.valid){
            nameValidationResult=<a>{this.state.NameValidationResult.message}</a>;
        }else{
            nameValidationResult=<a>{this.state.NameValidationResult.message}</a>;
        }

        if(this.state.AddressValidationResult.valid){
            addrValidationResult=<a>{this.state.AddressValidationResult.message}</a>;
        }else{
            addrValidationResult=<a>{this.state.AddressValidationResult.message}</a>;
        }
        if(this.state.PhoneValidationResult.valid){
            phoneValidationResult=<a>{this.state.PhoneValidationResult.message}</a>;
        }else{
            phoneValidationResult=<a>{this.state.PhoneValidationResult.message}</a>;
        }
        
        if(this.state.ImageValidationResult.valid){
            imgValidationResult=<a>{this.state.ImageValidationResult.message}</a>;
        }else{
            imgValidationResult=<a>{this.state.ImageValidationResult.message}</a>;
        }

        if(this.state.ValidationSummary){
            summary=<a>{this.state.ValidationSummary}</a>
        }

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
                           Title={this.props.Title}>
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
                                onChange={this.onImageLoad.bind(this)}/>
                        </div>
                        {imgValidationResult}
                    </div>
                    {summary}
            </ModalTemplate>
        )
    }
}