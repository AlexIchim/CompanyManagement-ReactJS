import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as $ from 'jquery';
import * as Controller from '../controller';

export default class EditForm extends React.Component{
    
    constructor(){
        super();
        this.state={
            member:{
            },
            totalAllocation:null
        }
    }

    componentWillMount(){
        this.setState({
            member: this.props.element
        }) 

        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getTotalAllocation?employeeId='+this.props.element.get('Id'),
            success: function (data) { 
                this.setState({                 
                    totalAllocation: data
                })       
            }.bind(this)
        })   
    }

    
    changeData(){
        const newAllocation = this.refs.allocation.value;
        const newMember= this.state.member.set('Allocation',newAllocation);

        
  
        this.setState({
            member: newMember
        })
            
    }
    

    edit(cb){
        const inputInfo = {
            EmployeeId: this.state.member.get('Id'),
            ProjectId: parseInt(this.props.projectId) ,
            Allocation: parseInt(this.state.member.get('Allocation'))
        }

        console.log(this.state.member.toJS())
        $.ajax({
            method: 'PUT',
            async: false,
            url: configs.baseUrl + 'api/employee/updatePartialAllocation',
            data:inputInfo,
            success: function (data) { 
                    cb(); 
                   this.refresh(this.props.projectId);                        
            }.bind(this)
        })   

    }

    refresh(projectId){
         this.props.setPageNr();
         Controller.getEmployeesByProjectId(projectId,null,1);
    }

  
    render(){

    
        return(

        <Modal title={'Edit member allocation'} button={'Edit'} close={this.props.close} action={this.edit.bind(this)}>

            <div className="form-group">
                <h2> {this.state.member.get('Name')} </h2>
            </div>


            <div className="form-group">
                <label className="col-sm-4 control-label"> Allocation </label>
                <div className="col-sm-6">
                    <input  ref="allocation" className="form-control"  value={this.state.member.get('Allocation')} onChange={this.changeData.bind(this)}/>
                </div>
            </div>



      
        </Modal>
        )
    }
    
    
}