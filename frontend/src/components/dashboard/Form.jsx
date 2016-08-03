import React from 'react';
import ModalTemplate from './ModalTemplate';


export default class Form extends React.Component{
    
    constructor(){
        super();
        this.state={
            office: {
    
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
        const newOffice = this.state.office;
        newOffice.Name = name;
        newOffice.Address = address
        newOffice.PhoneNumber = phone;
        this.setState({
            office: newOffice
        })
    }
    
    edit(){
        $.ajax({
            method: 'POST',
            url: configs.baseUrl + 'api/office/updateOffice',
            data: this.state.office,
            success: function(data){

            }.bind(this)
        })
    }
    
    
    render(){
        return(

        <ModalTemplate close={this.props.close}>

            <div className="form-group">
                <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputName" placeholder="Name" value={this.state.office.Name || ''} onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputAddress" placeholder="Address" value={this.state.office.Address || ''} onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputPhone" placeholder="Phone" value={this.state.office.PhoneNumber || ''} onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>

        </ModalTemplate>
        )
    }
    
    
}