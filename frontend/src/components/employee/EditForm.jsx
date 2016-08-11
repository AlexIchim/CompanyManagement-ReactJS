import React from 'react';
import Modal from '../modal/Modal.jsx';
import configs from '../helpers/calls';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import ValidateEmployee from '../validators/ValidateEmployee.js';

export default class EditForm extends React.Component{
    
    constructor(){
        super();
        this.state={
            jobTypes:[],
            positionTypes:[],
            employee:{
            },
            errors:{
                NameErrors:[],
                AddressErrors:[]
            }
        }
    }
     componentWillMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getJobTypes',
            success: function (data) {
                this.setState({
                    jobTypes: data,
                    employee: this.props.element
                })
            }.bind(this)
        })

        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getPositionTypes',
            success: function (data) {
                this.setState({
                    positionTypes: data,
                    employee: this.props.element
                })
            }.bind(this)
        })          
    }

    componentDidMount(){
        $('#datepicker1').datepicker({});
        $('#datepicker2').datepicker({});
    }

    changeData(){
        const namee = this.refs.name.value

        const newO = {
            Id: this.state.employee.get('Id'),
            Name: namee,
            Address: this.refs.address.value,
            EmploymentDate: this.refs.employmentDate.value,
            ReleaseDate: this.refs.releaseDate.value,
            JobType: this.refs.jobType.options[this.refs.jobType.selectedIndex].text, 
            PositionType: this.refs.positionType.options[this.refs.positionType.selectedIndex].text,
            TotalAllocation:this.state.employee.get("TotalAllocation")
        }


        
        this.setState({
            employee: Immutable.fromJS(newO)
        }) 
      
    }

    edit(cb){
   
        const jobTypeDescription=this.refs.jobType.options[this.refs.jobType.selectedIndex].value;
        const positionTypeDescription=this.refs.positionType.options[this.refs.positionType.selectedIndex].value
        const employmentDate=this.refs.employmentDate.value;
        const releaseDate=this.refs.releaseDate.value;

        const newEmployee={
            Id: this.state.employee.get('Id'),
            Name:this.state.employee.get('Name'),
            Address: this.state.employee.get("Address"),
            EmploymentDate: employmentDate,
            ReleaseDate: releaseDate,
            JobType: jobTypeDescription,
            PositionType: positionTypeDescription,
            DepartmentId:this.props.departmentId
        }

         console.log("lala",newEmployee);

         const np =this.state.employee.set('JobType',this.refs.jobType.options[this.refs.jobType.selectedIndex].text);
         const np2=np.set('PositionType', this.refs.positionType.options[this.refs.positionType.selectedIndex].text);
         const np3=np2.set('EmploymentDate',employmentDate);
         const np4=np3.set('ReleaseDate',releaseDate);
       
      
        $.ajax({
            method: 'PUT',
            async: false,
            url: configs.baseUrl + 'api/employee/updateEmployee',
            data:newEmployee,
            success: function (data) { 
                //const index= Context.cursor.get('employees').indexOf(this.props.node)
                   Context.cursor.get('employees').update( this.props.index,  oldInstance => {
                        oldInstance=np4
                        return oldInstance;
                    });              
                 
                 cb(); 
                 
            }.bind(this)
        })                
    }

    onChangeName()
    { 
        const errors = ValidateEmployee.validateName(this.refs.name.value)
        this.state.errors.NameErrors = errors
       
         this.setState({
             errors: this.state.errors
         })
    }

    onChangeAddress()
    { 
        const errors = ValidateEmployee.validateAddress(this.refs.address.value)
        this.state.errors.AddressErrors = errors
       
         this.setState({
             errors: this.state.errors
         })
    }

    render(){

        const jobTypes=this.state.jobTypes.map((el, x) => {
            return (
                <option  value={el.Id} key={x} id={el.Id} >{el.Description}</option>                         
            )
        });
        const positionTypes=this.state.positionTypes.map((el, x) => {
            return (
                <option  value={el.Id} key={x} id={el.Id} >{el.Description}</option>                         
            )
        });

        const currentJobType = this.state.employee.get("JobType") ? this.state.employee.get("JobType"):""
        const currentPositionType = this.state.employee.get("PositionType") ? this.state.employee.get("PositionType"):""

        return(

        <Modal title={'Edit employee'} button={'Edit'} close={this.props.close} action={this.edit.bind(this)}>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Name </label>
                <div className="col-sm-6">
                    {this.state.errors.NameErrors}
                    <input  ref="name" className="form-control" placeholder="Name" value={this.state.employee.get('Name')} onChange={this.changeData.bind(this)} onKeyUp={this.onChangeName.bind(this)}/>
                </div>
            </div>
            <div className="form-group">
                <label className="col-sm-4 control-label"> Address </label>
                <div className="col-sm-6">
                    {this.state.errors.AddressErrors}
                    <input  ref="address" className="form-control" placeholder="Address" value={this.state.employee.get('Address')} onChange={this.changeData.bind(this)} onKeyUp={this.onChangeAddress.bind(this)}/>
                </div>
            </div>
           
             <div className="form-group">
                <label className="col-sm-4 control-label">Employment Date</label>
                 <div className="col-sm-6">
                    <div className="input-group date">
                      <div className="input-group-addon">
                        <i className="fa fa-calendar"></i>
                      </div>
                      <input type="text" ref="employmentDate" className="form-control" id="datepicker1" value={this.state.employee.get('EmploymentDate')}onChange={this.changeData.bind(this)}/>
                    </div>
                 </div>
                
              </div>
            <div className="form-group">
                <label className="col-sm-4 control-label">Release Date</label>
                <div className="col-sm-6">
                    <div className="input-group date">
                        <div className="input-group-addon">
                            <i className="fa fa-calendar"></i>
                        </div>
                        <input type="text" ref ="releaseDate" className="form-control" id="datepicker2" value={this.state.employee.get('ReleaseDate')} onChange={this.changeData.bind(this)}/>
                    </div>
                </div>
            </div>

           <div className="form-group">
               <label className="col-sm-4 control-label"> Job Type </label>
               <div className="col-sm-6">
                    <select className="form-control" ref="jobType" >
                        {jobTypes}
                    </select>
                </div>
            </div>


            <div className="form-group">
                <label className="col-sm-4 control-label"> Position Type </label>
                <div className="col-sm-6">
                    <select className="form-control" ref="positionType" >
                        {positionTypes}
                    </select>
                </div>
            </div>
           
        </Modal>
        )
    }
}