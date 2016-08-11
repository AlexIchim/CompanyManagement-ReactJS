import React from 'react';
import * as Controller from '../../api/controller';

export default class EditOffice extends React.Component {

    constructor(){
        super();
        this.state = {
            office : {},
            nameMessage : "",
            phoneMessage : "",
            addressMessage : ""
        }
    }

    componentWillMount(){
        const office = this.props.office;

        let copy = {};
        for(let key in office){
            if(office.hasOwnProperty(key)){
                copy[key] = office[key];
            }
        }

        this.setState({
            office: copy
        })
    }

    onSave(){
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

                const newOffice = this.state.office;
                newOffice.image = outputImage;
                Controller.updateOffice(
                    newOffice,
                    false,
                    this.props.updateFunc
                );
            }.bind(this);
            img.src = URL.createObjectURL(this.imageFile);
        }
        else{
            const newOffice = this.state.office;
            Controller.updateOffice(
                newOffice,
                false,
                this.props.updateFunc
            );
        }



        const newOffice = this.state.office;
        Controller.updateOffice(
            newOffice,
            false,
            this.props.updateFunc
        )
    }

    /*onChangeHandler(e){
        let officeObject = this.state.office;
        officeObject[e.target.name] = e.target.value;

        this.setState({
            office: officeObject
        })
    }*/

    isCorrectPhoneNumber(phoneNumber){
        const str="+-()0123456789 ";
        for(let i = 0; i < phoneNumber.length; i++){
            if(str.indexOf(phoneNumber.substr(i, 1)) === -1){
                return false;
            }
        }
        return true;
    }

    componentDidMount(){
        let canvas = document.getElementById('imageCanvas');
        let ctx = canvas.getContext('2d');
        let img = new Image;
        img.onload = function() {
            ctx.drawImage(img, 0, 0, 150, 150 * img.height / img.width);    
        }
        img.src = 'data:image/jpg;base64,' + this.state.office.image;
        this.imageFile = null;
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
            img.onload = function() {
                ctx.drawImage(img, 0, 0, 150, 150 * img.height / img.width);    
            }
            img.src = 'data:image/jpg;base64,' + this.state.office.image;
            this.imageFile = null;
        }
    }

    onTextInputChangeName(){
     
        const newOfficeName = this.refs.name && this.refs.name.value || '';
        let newOffice = this.state.office;

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
                nameMessage : "",
                correctName : newOfficeName
            });
        }
        this.refs.name.value = newOfficeName.substr(0, 99);
        newOffice.name = this.refs.name.value;

        this.setState({
            office : newOffice
        });
        
    }

    onTextInputChangeAddress(){
        
        const newOfficeAddress = this.refs.address && this.refs.address.value || '';
        const newOffice = this.props.office; 

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
                addressMessage : "",
                correctAddress : newOfficeAddress
            });
        }
        
        this.refs.address.value = newOfficeAddress.substr(0, 299);
        newOffice.address = this.refs.address.value;

        this.setState({
            office : newOffice
        });

    }

    onTextInputChangePhone(){
        
        const newOfficePhone = this.refs.phone && this.refs.phone.value || '';
        const length = newOfficePhone.length;
        const newOffice = this.props.office;

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

        newOffice.phone = this.refs.phone.value;

        this.setState({
            office : newOffice
        });
        
    }

    render() {
        let name = this.state.office.name;
        let address = this.state.office.address;
        let phone = this.state.office.phone;
        let image = this.state.office.image;
        const saveButton = ((name !== "") && (name.length <= 100) && (address !== "") && (address.length <= 300) && (phone !== "") && (phone.length <= 20) && (this.isCorrectPhoneNumber(phone))) ?
                            (<button type="button" className="btn btn-md btn-info" onClick={this.onSave.bind(this)}>Save</button>) :
                            (<button type="button" className="btn btn-md btn-info" disabled>Save</button>);

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Edit office</h3>
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
                                <input type="text" ref="name" className="leftAligned form-control"  value={name} onChange={this.onTextInputChangeName.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label className="rightAligned">Address:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" ref="address" className="leftAligned form-control"  value={address} onChange={this.onTextInputChangeAddress.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label className="rightAligned">Phone:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" ref="phone" className="leftAligned form-control"  value={phone} onChange={this.onTextInputChangePhone.bind(this)}  />
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
                    <div><b><font color="red">{this.state.nameMessage}<br/>
                            {this.state.addressMessage}<br/>
                            {this.state.phoneMessage}
                            </font>
                    </b></div>
                    <div className="box-footer">
                        {saveButton}
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
                </form>
            </div>
        );
    }
}