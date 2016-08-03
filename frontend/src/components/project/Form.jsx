import React from 'react';
import ModalTemplate from '../department/ModalTemplate';

export default class Form extends React.Component {
    constructor(){
        super();
    }
    render(){
        return(

            <ModalTemplate close={this.props.close} store={function(){ console.log('haha') }}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputName" placeholder="Name" value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputAddress" className="col-sm-2 control-label"> Duration</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputAddress" placeholder="Duration" value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputPhone" className="col-sm-2 control-label"> Status </label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputPhone" placeholder="Status" value="">
                        </input>
                    </div>
                </div>

            </ModalTemplate>
        )
    }

}