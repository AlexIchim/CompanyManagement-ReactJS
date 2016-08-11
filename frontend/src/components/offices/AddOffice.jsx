import React from 'react';
import "./../../assets/less/index.less";
import * as Controller from '../../api/controller';

export default class AddOffice extends React.Component{

    constructor(){
        super();
        this.state = {
            nameMessage : "Error!!! Office name cannot be empty.",
            addressMessage : "Error!!! Office address cannot be empty.",
            phoneMessage : "Error!!! Office phone number cannot be empty."
        }
    }

    isCorrectPhoneNumber(phoneNumber){
        const str="+-()0123456789 ";
        for(let i = 0; i < phoneNumber.length; i++){
            if(str.indexOf(phoneNumber.substr(i, 1)) === -1){
                return false;
            }
        }
        return true;
    }

    addOffice()
    {
        if(this.imageFile !== null && this.imageFile !== undefined){
            let canvas = document.createElement("canvas");
            let ctx = canvas.getContext("2d");
            
            let img = new Image;    
            img.onload = function() {
                canvas.width = img.width;
                canvas.height = img.height; 
                ctx.drawImage(img, 0, 0);
                URL.revokeObjectURL(img.src); 
                   
                let outputImage = canvas.toDataURL("image/jpeg").substr(23); 

                URL.revokeObjectURL(img.src);

                let officeObject = {
                    name: this.refs.name.value,
                    address: this.refs.address.value,
                    phone: this.refs.phone.value,
                    image: outputImage
                }
                Controller.addNewOffice(
                    officeObject,
                    false,
                    this.props.saveFunc
                )
            }.bind(this);
            img.src = URL.createObjectURL(this.imageFile);
        }
        else{
            let officeObject = {
                name: this.refs.name.value,
                address: this.refs.address.value,
                phone: this.refs.phone.value,
                image: null
            }
            Controller.addNewOffice(
                officeObject,
                false,
                this.props.saveFunc
            )
        }
    }

    onImageChange(e){   
        let canvas = document.getElementById('imageCanvas');
        let ctx = canvas.getContext('2d');
        
        if(e.target.files[0] !== undefined){
            let img = new Image;
            img.onload = function() {
                ctx.drawImage(img, 0, 0, 150, 150 * img.height / img.width);
                URL.revokeObjectURL(img.src);
            }
            img.src = URL.createObjectURL(e.target.files[0]);
            this.imageFile = e.target.files[0];
        }
        else{
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            this.imageFile = null;
        }
    }

    onTextInputChangeName(){
        const newOfficeName = this.refs.name && this.refs.name.value || '';
       
        if(newOfficeName === ""){
            this.setState({
                nameMessage : "Error!!! Office name cannot be empty."
            });
        } else if(newOfficeName.length > 100){
            this.setState({
                nameMessage : "Error!!! Office name cannot contain more than 100 characters."
            });
        } else {
            this.setState({
                nameMessage : ""
            });
        }
        this.refs.name.value = newOfficeName.substr(0, 99); 
    }

    onTextInputChangeAddress(){
        const newOfficeAddress = this.refs.address && this.refs.address.value || '';

        if(newOfficeAddress === ""){
            this.setState({
                addressMessage : "Error!!! Office address cannot be empty." 
            });
        } else if(newOfficeAddress.length > 300) {
            this.setState({
                addressMessage : "Error!!! Office address cannot contain more than 300 characters."
            });
        } else {
            this.setState({
                addressMessage : ""
            });
        }
        this.refs.address.value = newOfficeAddress.substr(0, 299);
    }

    onTextInputChangePhone(){
        const newOfficePhone = this.refs.phone && this.refs.phone.value || '';
        const length = newOfficePhone.length;

        if(newOfficePhone === ""){
            this.setState({
                phoneMessage : "Error!!! Office phone number cannot be empty."
            });
            this.refs.phone.value = newOfficePhone.substring(0, length - 1);
        } else if(newOfficePhone.length > 20){
            this.setState({
                phoneMessage : "Error!!! Office phone number cannot contain more than 20 characters."
            });
            this.refs.phone.value = newOfficePhone.substring(0, length - 1);
        } else if(this.isCorrectPhoneNumber(this.refs.phone.value) === false){
            this.setState({
                phoneMessage : "Error!!! Invalid format for office phone number."
            });
            this.refs.phone.value = newOfficePhone.substring(0, length - 1);
        } else {
            this.setState({
                phoneMessage : "",
            });
            this.refs.phone.value = newOfficePhone;
        }
        
    }

    render()
    {
        
        const name = this.refs.name && this.refs.name.value || '';
        const address = this.refs.address && this.refs.address.value || '';
        const phone = this.refs.phone && this.refs.phone.value || '';

        const addButton = ((name !== "") && (name.length <= 100) && (address !== "") && (address.length <= 300) && (phone !== "") && (phone.length <= 20)) ?
            (<button type="button" className="btn btn-md btn-info" onClick={this.addOffice.bind(this)}>Add</button>) : 
            (<button type="button" className="btn btn-md btn-info" disabled>Add</button>);

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new office</h3>
                </div>
                <form className="form-horizontal">
                    <div className="box-body">
                    </div>
                    <div className="formBody">
                        <div className="form-group">
                            <div className="col-md-2 leftColoumn">
                                <label className="rightAligned">Name:</label>
                            </div>
                            <div className="col-md-8 rightColoumn">
                                <input type="text" className="leftAligned form-control" ref="name" onChange={this.onTextInputChangeName.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label className="rightAligned">Address:</label>
                            </div>
                            <div className="col-md-8">
                                <input type="text" className="leftAligned form-control" ref="address" onChange={this.onTextInputChangeAddress.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label className="rightAligned">Phone:</label>
                            </div>
                            <div className="col-md-8">
                                <input type="text" className="leftAligned form-control" ref="phone" onChange={this.onTextInputChangePhone.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label className="rightAligned" htmlFor="image">Image:</label>
                            </div>
                            <div className="col-md-8">
                                <div className="col-md-6 col-sm-6 col-xs-12">
                                    <input type="file" ref="image" defaultValue="Choose image" onChange={this.onImageChange.bind(this)}/>
                                </div>
                                <div className="col-md-6 col-sm-6 col-xs-12">
                                    <canvas id="imageCanvas"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <b>
                            <font color="red">{this.state.nameMessage}<br/>{this.state.addressMessage}<br/>{this.state.phoneMessage}</font>
                        </b>
                    </div>
                    <div className="box-footer">
                        {addButton}
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}>
                            Cancel
                        </button>
                    </div>
                </form>
            </div>
        );
    }
}
