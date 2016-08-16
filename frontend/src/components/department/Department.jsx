import React, { Component } from 'react';
import {Link} from 'react-router';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Form from './Form.jsx'
import EditForm from './EditForm.jsx'
import DepartmentItem from './DepartmentItem.jsx'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as Controller from '../controller';



export default class Department extends React.Component {

    constructor() {
        super();
        this.state = {
            add: false,
            departments: Context.cursor.get("departments"),
            pageNr:1,
            pageSize:3,
            nrOfPages:null
        }
    }

    
    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this))
        Controller.getAllOffices();
    }

    componentWillReceiveProps(props){
        this.setState({
            pageNr:1
        })
        this.getAllDepartments(1, props.params.officeId)
        this.setNumberOfPages(props.params.officeId);
    }

    componentDidMount(){
         this.getAllDepartments(this.state.pageNr);
        this.setNumberOfPages();
    }

    componentWillUnmount () {
        this.subscription.dispose(); 
    }

     onContextChange(cursor){
         this.setState({
            departments: cursor.get("departments")         
        });

    }

    getAllDepartments(pageNr, officeId = null){

        if(! officeId){
            officeId = this.props.routeParams.officeId;
        }
        Controller.getAllDepOffice(officeId,pageNr);


    }

    setNumberOfPages(officeId = null){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/office/getAllDepOffice?officeId=' +(officeId || this.props.routeParams.officeId)+'&pageSize=null&pageNr=null',
            success: function (data) {
                if (data.length==0){
                    this.setState(
                        {
                            nrOfPages: 1
                        }
                    )

                }else{
                    this.setState(
                        {
                            nrOfPages: Math.ceil( data.length / this.state.pageSize)
                        }
                    )

                }


            }.bind(this)
    })
}
    
    showAddForm(){
        this.setState({
            add:true
        });
    }

    closeAddForm(){
        this.setState({
            add: !this.state.add
        })
    }

    back(){
        if (this.state.pageNr>1){

            const whereTo=this.state.pageNr-1

            this.getAllDepartments(whereTo);
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){
       
        const whereTo=this.state.pageNr+1

        if (whereTo <= this.state.nrOfPages){
            
             this.getAllDepartments(whereTo);

            this.setState({
                pageNr:this.state.pageNr+1
            })

        }

    }
    last(){

        
        this.setNumberOfPages();


        this.getAllDepartments(this.state.nrOfPages, this.props.routeParams.officeId);

        this.setState({
            pageNr: this.state.nrOfPages
        })

    
    }

    first(){
        if (this.state.pageNr!=1){
             this.getAllDepartments(1,this.props.routeParams.officeId);

        this.setState({
            pageNr:1
        })

        }
       
    }

    render() {
        const items = this.state.departments.map((el, x) => {
            return (
                <DepartmentItem
                node = {el}
                key= {x}
                officeId={this.props.routeParams.officeId}      
                /> 
            )
        });

        const addModal = this.state.add ? <Form officeId={this.props.routeParams.officeId} show = {this.state.add} close={this.closeAddForm.bind(this)} /> : '';
    
        return (
            <div>
                {addModal}


                <div className="form-group">
                    <button className="btn btn-md btn-info btn-add-custom" onClick={this.showAddForm.bind(this)} > <span className="glyphicon glyphicon-plus-sign"></span> Add new department </button>
                </div>
                
                <table className="table table-striped" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Department</th>
                        <th className="col-md-2">Department manager</th>
                        <th className="col-md-2">Employees</th>
                        <th className="col-md-2">Projects</th>
                        <th className="col-md-2">Actions</th>
                    </tr>
                    </thead>
                    <tbody>

                    {items}
                  
                    </tbody>
                </table>

                <div className="btn-wrapper">
                    <button className="rightArrow" onClick={this.first.bind(this)}>
                        <i className="fa fa-angle-double-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-angle-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <label className="to-right">{this.state.pageNr} </label>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-angle-right fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.last.bind(this)}>
                        <i className="fa fa-angle-double-right fa-2x" aria-hidden="true"></i>
                    </button>





                </div>

            </div>
        )
    }


}