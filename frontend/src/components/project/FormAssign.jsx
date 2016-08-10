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
            pageSize:3,
            nrOfPages:null,
            employeeToAssign:{

            },
            filterByPosition:null,
            filterByDepartment:null
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

        this.getEmployeesThatAreNotFullyAllocated(1,null,null);

        this.setNumberOfPages(null,null);

        }

    getEmployeesThatAreNotFullyAllocated(pageNr, position, depId){
         $.ajax({
                method: 'GET',
                async: false,
                url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+ this.props.projectId  +'&pageSize='+this.state.pageSize + '&pageNr='+ pageNr  + '&departmentId=' + depId + '&ptype='+position,
                success: function (data) {
                    this.setState({
                        availableEmployees: data
                    })
                }.bind(this)
            })
    }

    setNumberOfPages(position,depId){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+ this.props.projectId  +'&pageSize=null' + '&pageNr=null'  + '&departmentId=' + depId + '&ptype='+position,
            success: function (data) {
                console.log(data)
                this.setState(
                    {
                        nrOfPages: data.length / this.state.pageSize + 1
                    }
                )
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
        this.props.setPageNr();
        Controller.getEmployeesByProjectId(this.props.projectId,null,1);
    }

     back(){

        if (this.state.pageNr > 1){
             const whereTo=this.state.pageNr-1

             this.getEmployeesThatAreNotFullyAllocated(whereTo,this.state.filterByPosition, this.state.filterByDepartment)
            
             this.setState({
                pageNr:this.state.pageNr-1
            })
        }
                
    }

    next(){

        const whereTo=this.state.pageNr+1

        if(whereTo < this.state.nrOfPages) {

            this.getEmployeesThatAreNotFullyAllocated(whereTo,this.state.filterByPosition, this.state.filterByDepartment);

            this.setState({
                pageNr:this.state.pageNr+1
            })

        }       
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
        let ptype=this.refs.positionTypes.options[this.refs.positionTypes.selectedIndex].value;
            if(ptype === ""){
                ptype={};
            }

        let depId=this.refs.departments.options[this.refs.departments.selectedIndex].value;
            if (depId=="")
                depId = null

        const pageNr = 1;

        this.setState({
            filterByPosition: ptype,
            filterByDepartment:depId,
            pageNr:pageNr
        })

        this.setNumberOfPages(ptype,depId);

        this.getEmployeesThatAreNotFullyAllocated(pageNr,ptype,depId)
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



            <div className="form-group">
                <div className="col-sm-4 dropdown-custom-left">
                    <label className="control-label"> Position </label>
                    <select className="form-control" defaultValue="Position" ref="positionTypes" onChange={this.onDropDownChange.bind(this)}>
                        <option value=""> None </option>
                        {positionTypes}
                    </select>
                </div>

                <div className="col-sm-4 dropdown-custom-right">
                    <label className="control-label label-center"> Department </label>
                    <select className="form-control"  ref="departments" onChange={this.onDropDownChange.bind(this)}>
                        <option value=""> None </option>
                        {departments}
                    </select>
                </div>
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

                  <div className="btn-wrapper">
                    <button className="leftArrow" onClick={this.back.bind(this)}>
                                <i className="fa fa-arrow-left fa-1x" aria-hidden="true"></i>
                    </button>
                    <button className="rightArrow" onClick={this.next.bind(this)}>
                                <i className="fa fa-arrow-right fa-1x" aria-hidden="true"></i>
                    </button>              
                </div>
        
       
        </Modal>
        )
    }
    
    
}