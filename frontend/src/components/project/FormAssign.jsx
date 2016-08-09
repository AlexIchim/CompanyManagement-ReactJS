import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as Controller from '../controller';


export default class FormAssign extends React.Component{
    
    constructor(){
        super();
        this.state={
            positionTypes:[],
            departments:[],
            availableEmployees:[],
            pageNr:1,
            employeeToAssign:{

            }
        }
    }

    componentWillMount(){

         $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getPositionTypes',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    positionTypes: data
                })
            }.bind(this)
        })        

        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/office/getAllDepOffice?officeId='+ this.props.officeId+'&pageSize=null'+'&pageNr=null',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    departments: data
                })
            }.bind(this)
        })   

        $.ajax({
            method: 'GET',
            async: false,
          url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+ this.props.projectId + '&departmentName=' + "" + '&ptype='+ {} +'&pageSize=5'+'&pageNr=1',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    availableEmployees: data
                })
            }.bind(this)
        })                   
    }


    assign(cb){

        const inputInfo = {
             projectId:this.state.employeeToAssign.projectId,
             employeeId:this.state.employeeToAssign.employeeId,
             allocation:this.state.employeeToAssign.allocation

        }

        console.log(inputInfo);

        $.ajax({
            method: 'POST',
            async: false,
            url: configs.baseUrl + 'api/employee/assignEmployee',
            data:inputInfo,
            success: function (data) {               
                 cb(); 
                 this.refresh();
            }.bind(this)
        })   
              
    }

    refresh(){
        Controller.getEmployeesByProjectId(this.props.projectId,1);
    }

     back(){

        if (this.state.pageNr!=1){
            const whereTo=this.state.pageNr-1

            $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+  this.props.projectId +'&departmentName=' + "" + '&ptype='+ {}+'&pageSize=5'+'&pageNr='+whereTo,
            success: function (data) {
                console.log(data, this);
                this.setState({
                    availableEmployees: data
                })
            }.bind(this)
        })    
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+ this.props.projectId + '&departmentName=' + "" + '&ptype='+ {} +'&pageSize=5'+'&pageNr='+whereTo,
            success: function (data) {
                console.log(data, this);
                this.setState({
                    availableEmployees: data
                })
            }.bind(this)
        })    

        this.setState({
            pageNr:this.state.pageNr+1
        })
    }

    onChange(employeeId){
   
            let employee={
                projectId:this.props.projectId,
                employeeId:employeeId,
                allocation:$("#"+employeeId).data('allocation')
            }

             
            this.setState({
                employeeToAssign:employee
            })
            console.log(employee);
        }

        onDropDownChange(){
        const ptype=this.refs.positionTypes.options[this.refs.positionTypes.selectedIndex].value;
        const depName=this.refs.departments.options[this.refs.departments.selectedIndex].text;
        const pageNr = 1;

         $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+ this.props.projectId + '&departmentName=' + depName + '&ptype='+ ptype +'&pageSize=5'+'&pageNr=1',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    availableEmployees: data
                })
            }.bind(this)
        })    
    }
         

       
    render(){
        const positionTypes=this.state.positionTypes.map((el, x) => {
            return (
                <option value={el.Id} key={x} >{el.Description}</option>                         
            )
        });

        const departments=this.state.departments.map((el, x) => {
            return (
                <option value={el.Id} key={x} >{el.Name}</option>                         
            )
        });

        const items= this.state.availableEmployees.map((el,x)=>{
            return (
                <tr key={el.Id}>
                    <td>{el.Name}</td>
                    <td>{el.DepartmentName}</td>
                    <td>{el.Role}</td>
                    <td>{el.RemainingAllocation}</td>
                    <td><input id={el.Id} data-allocation={el.RemainingAllocation} ref="radio" type="radio" name="radio" onChange={this.onChange.bind(this,el.Id)} /></td>
                </tr>
            )
        })

        return(

        <Modal title={'Assign employee'} button={'Assign'} close={this.props.close} action={this.assign.bind(this)}>
            <div className="filter">
                <select className="selectpicker" ref="positionTypes" onChange={this.onDropDownChange.bind(this)} >
                    {positionTypes}                    
                </select>

                <select className="selectpicker" ref="departments" onChange={this.onDropDownChange.bind(this)} >
                    {departments}                    
                </select>
            </div>

            <table className="table table-striped" id="table1">
                    <thead>
                    <tr>
                        <th className="col-md-2">Name</th>
                        <th className="col-md-2">Department</th>
                        <th className="col-md-2">Position</th>
                        <th className="col-md-2">Remaining Allocation</th>
                        <th className="col-md-2"></th>
                    </tr>
                    </thead>
                    <tbody>

                    {items}
                  
                    </tbody>
            </table>

                  <div>
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-arrow-left fa-2x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-arrow-right fa-2x" aria-hidden="true"></i>
                    </button>              
                </div>
        
       
        </Modal>
        )
    }
    
    
}