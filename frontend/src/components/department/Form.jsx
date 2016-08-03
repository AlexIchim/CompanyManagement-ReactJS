import React from 'react';
import AddDepartmentModalTemplate from './AddDepartmentModalTemplate.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default class Form extends React.Component{
    
    constructor(){
        super();
        this.state={
            departmentManagers:[]
        }
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/department/getAllDepartmentManagers',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    departmentManagers: data
                })
            }.bind(this)
        })     
    }

    store(cb){
   
        var inputInfo={
            Name: this.refs.name.value,
            OfficeId: this.props.officeId,
            DepartmentManagerId: 7
        }

        $.ajax({
            method: 'POST',
            url: configs.baseUrl + 'api/department/addDepartment',
            data:inputInfo,
            success: function (data) {              
                Context.cursor.update('departments',(oldList) => {      
                    return oldList.push( Immutable.fromJS(inputInfo) );
                });
            }.bind(this)
        })    

        cb(); 
    }

    
    render(){
        const departmentManagers=this.state.departmentManagers.map((el, x) => {
            return (
                <li key={x}><a href="#">{el.Name}</a></li>                         
            )
        });

        return(

        <AddDepartmentModalTemplate close={this.props.close} store={this.store.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    <input  ref="name" className="form-control" placeholder="Name"/>
                </div>
            </div>
           
            <div className="dropdown">
                <label className="col-sm-4 control-label"> Department manager </label>
                <button className="btn btn-default dropdown-toggle col-sm-6" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                    
                    <span className="caret"></span>
                </button>
                <ul className="dropdown-menu col-sm-6" aria-labelledby="dropdownMenu1">
                    {departmentManagers}                    
                </ul>
            </div>
       
        </AddDepartmentModalTemplate>
        )
    }
    
    
}