import * as React from 'react';
import ModalTemplate from '../ModalTemplate';

export default class Form extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){

    }

    componentWillReceiveProps(props){
    }

    render(){
        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.props.onStoreClick}
                           Title={this.props.Title}
                           Model={this.props.Model}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputName" placeholder="Name" value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputAddress" placeholder="Address" value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputPhone" placeholder="Phone" value="">
                        </input>
                    </div>
                </div>

            </ModalTemplate>
        )
    }
}