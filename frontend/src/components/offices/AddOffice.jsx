import React from 'react';
import "./../../assets/less/index.less";
import * as Controller from '../../api/controller';

export default class AddOffice extends React.Component{

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

                console.log('---------------------');
                console.log(canvas.toDataURL("image/jpeg"));
                console.log('---------------------');

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

    render()
    {
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
                                <label className="rightAligned" htmlFor="name">Name:</label>
                            </div>
                            <div className="col-md-8 rightColoumn">
                                <input required="" type="text" className="leftAligned form-control" ref="name"/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label htmlFor="address" className="rightAligned">Address:</label>
                            </div>
                            <div className="col-md-8">
                                <input type="text" className="leftAligned form-control" ref="address"/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label className="rightAligned" htmlFor="phone">Phone:</label>
                            </div>
                            <div className="col-md-8">
                                <input type="text" className="leftAligned form-control" ref="phone"/>
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

                    <div className="box-footer">
                        <button type="button" className="btn btn-md btn-info" onClick={this.addOffice.bind(this)}>
                            Add
                        </button>
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}>
                            Cancel
                        </button>
                    </div>
                </form>
            </div>
        );
    }
}