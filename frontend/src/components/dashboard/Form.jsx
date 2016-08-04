import * as React from 'react';
import ModalTemplate from '../ModalTemplate';

export default class Form extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){
        if(this.props.Model){
            this.setState({
                model: this.props.Model
            });
        }else{
            this.setState({
                model:{}
            });
        }
    }

    onImageLoad(){
        var value=document.querySelector('input[type=file]').files[0];

        var reader=new FileReader();
        var image;
        reader.onload=function(event){
            image=this.result;
        }

        reader.readAsDataURL(value);

        let newModel= this.state.model;
        newModel.Image=image;
        this.setState({
            model: newModel
        })
    }

    onStoreClick(){
        const name;
        const addr;
        const phone;

        var model=this.props.model;

        if(model){
            model.Name=name;
            model.Address=addr;
            model.
        }else{
            model={
                Name: name,
                Address: addr
            }
        }

        this.props.onStoreClick(model);
    }

    render(){

        const model=this.props.Model;

        console.log(model);

        const name=(model)? model.Name : "Name";
        const addr=(model)? model.Address : "Address";
        const phone=(model)? model.Phone : "Phone";

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.props.onStoreClick.bind(this)}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text" 
                               className="form-control" 
                               ref="inputName" 
                               placeholder={name}
                               value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                    <div className="col-sm-10">
                        <input  type="text" 
                                className="form-control" 
                                ref="inputAddress" 
                                placeholder={addr}
                                value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                    <div className="col-sm-10">
                        <input type="text" 
                               className="form-control" 
                               ref="inputPhone" 
                               placeholder={phone}
                               value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputImage" className="col-sm-2 control-label"> Image </label>
                    <div className="col-sm-10">
                        <input type="file" 
                               ref="inputImage" 
                               onChange={this.onImageLoad.bind(this)}/>
                    </div>
                </div>

            </ModalTemplate>
        )
    }
}