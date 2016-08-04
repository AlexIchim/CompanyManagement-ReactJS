import React from 'react';
import {hideFunc} from '../layout/ModalTemplate';

export default class AddProject extends React.Component{
    render(){
        var project = this.props.project;

        return (
            <div className = "box info-box">
                <div className = "box-header with-border">
                    <h3 className = "box-title">ProjectName</h3>
                </div>
            <form className = "form-horizontal">
                <div className = "box-body">
                    <label>Name</label>
                    <input type = "text" className = "form-control" ref = "name" placeholder = "Project name"></input>
                    <label>Status</label>
                    <input type = "text" className = "form-control" ref = "status" placeholder = "Project status"></input>
                    <label>Duration</label>
                    <input type = "text" className = "form-control" ref = "duration" placeholder = "Project duration"></input>
                </div>

                <div className = "box-footer">
                    <button className = "btn btn-default">Add</button>
                    <button className = "btn btn-default" onClick = {this.props.hideFunc}>Cancel</button>
                </div>
            </form>
            </div>
        );
    }
}