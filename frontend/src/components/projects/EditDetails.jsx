import React from 'react';
import {hideFunc} from '../layout/ModalTemplate';

export default class EditDetails extends React.Component {

    render(){
        var project = props.project;

        return(
            <div className = "box info-box">
                <div className = "box-header with-border">
                <h3 className = "box-title">ProjectName</h3> 
                </div>
                <form className = "form-horizontal">
                    <div className = "box-body">
                        <label>New name</label>
                        <input type="text" ref = "newName"></input>
                        <label>New status</label>
                        <input type="text" ref = "newStatus"></input>
                        <label>New duration</label>
                        <input type="text" ref = "newDuration"></input>
                    </div>
                    <div className = "box-footer">
                        <button className = "btn btn-default">Edit</button>
                        <button className = "btn btn-default" onClick = {this.props.hideFunc}>Cancel</button>
                    </div>
                </form>
            </div>
        )
    }
}