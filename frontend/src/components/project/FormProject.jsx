import React from 'react';
import AddProjectModalTemplate from './AddProjectModalTemplate.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default class Form extends React.Component{

    constructor(){
        super();
    };
    /*
    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/project/getAllDepartmentManagers',
            success: function (data) {
                console.log(data, this);
                this.setState({

                })
            }.bind(this)
        })
    }*/

    store(cb){
        var inputInfo={
            Name: this.refs.name.value,
            Status: this.refs.status.value,
            DepartmentId: this.props.departmentId,
        }

        $.ajax({
            method: 'POST',
            url: configs.baseUrl + 'api/project/add',
            data:inputInfo,
            success: function (data) {
                Context.cursor.update('projects',(oldList) => {
                    return oldList.push( Immutable.fromJS(inputInfo) );
                });
                cb();
                this.refresh(this.props.departmentId);
            }.bind(this)
        })
    }

    refresh(departmentId){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/project/getAllDepartmentProjects?departmentId=' + departmentId+'&pageSize=10&pageNr=1',
            success: function (data) {
                Context.cursor.set("projects",Immutable.fromJS(data));
            }.bind(this)
        })
    }

    render(){

        return(

            <AddProjectModalTemplate close={this.props.close} store={this.store.bind(this)}>
                <div className="form-group">
                    <label className="col-sm-4 control-label"> Name </label>
                    <div className="col-sm-6">
                        <input  ref="name" className="form-control" placeholder="Name"/>
                    </div>
                    <label className="col-sm-4 control-label"> Status </label>
                    <div className="col-sm-6">
                        <input  ref="status" className="form-control" placeholder="Project Status"/>
                    </div>
                </div>

            </AddProjectModalTemplate>
        )
    }
}