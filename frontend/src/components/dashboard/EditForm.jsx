import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import * as Immutable from 'immutable';
import Context from '../../context/Context.js';
import * as $ from 'jquery';

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


        const newO = {
            Id: this.state.office.get('Id'),
            Name:name,
            Address:address,
            PhoneNumber:phone
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

    
    
    
    render(){
        return(

        <Modal title={'Edit office'} button={'Edit'} action={this.edit.bind(this)} close={this.props.close}>

            <div className="form-group">
                <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputName" placeholder="Name" value={this.state.office.get('Name') || ''} onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputAddress" placeholder="Address" value={this.state.office.get('Address') || ''} onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                <div className="col-sm-10">
                    <input type="text" className="form-control" ref="inputPhone" placeholder="Phone" value={this.state.office.get('PhoneNumber') || ''} onChange={this.changeData.bind(this)}>
                    </input>
                </div>
            </div>

        </Modal>
        )
    }
    
    
}