import React from 'react';
import * as Controller from '../../api/controller';

export default class EditOffice extends React.Component {

    constructor(){
        super();
    }

    componentWillMount(){
        const office = this.props.office;
        let copy = {};

        for (var prop in office) {
            if (office.hasOwnProperty(prop)){
                copy[prop] = office[prop];
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

    onChangeHandler(e){
        let officeObject = this.state.office;
        officeObject[e.target.name] = e.target.value;

        this.setState({
            office: officeObject
        })
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


    render() {
        let name = this.state.office.name;
        let address = this.state.office.address;
        let phone = this.state.office.phone;
        let image = this.state.office.image;

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
                                <label className="rightAligned" htmlFor="name">Name:</label>
                            </div>
                            <div className="col-md-8 rightColoumn">
                                <input type="text" className="leftAligned form-control" name="name" value={name} onChange={this.onChangeHandler.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label className="rightAligned" htmlFor="address">Address:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" className="leftAligned form-control" name="address" value={address} onChange={this.onChangeHandler.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label className="rightAligned" htmlFor="phone">Phone:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" className="leftAligned form-control" name="phone" value={phone} onChange={this.onChangeHandler.bind(this)}  />
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
                        <button type="button" className="btn btn-md btn-info" onClick={this.onSave.bind(this)}> Save</button>
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
                </form>
            </div>
        );
    }
}