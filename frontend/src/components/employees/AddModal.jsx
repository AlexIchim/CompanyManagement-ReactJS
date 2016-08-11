import React from 'react';
import * as Controller from '../../api/controller';

export default class AddModal extends React.Component { 
    constructor(){
        super();
        this.state = {
            employee: {
                name: '',
                email: '',
                address: '',
                employmentDate: '',
                employmentHours: 8,
                positionId: -1,
            },
            positionList: [],
            numberOfErrors : 3,
            nameMessage : "Error!!! Employee name cannot be empty.",
            emailMessage : "Error!!! Employee e-mail cannot be empty.",
            addressMessage : "",
            employmentDateMessage : "Error!!! Employment date is null or invalid.",
        };
    }
    
    componentWillMount(){
        
        this.validateName(this.state.employee.name);
        this.validateEmail(this.state.employee.email);
        this.validateAddress(this.state.employee.address);
        this.validateEmploymentDate(this.state.employee.employmentDate);
        this.setState({
            numberOfErrors : this.getNumberOfErrors()
        });
    }

    componentDidMount(){
        Controller.getPositions(
            true,
            (data) => {
                let employee = this.state.employee;
                employee.positionId = data[0].id;
                this.setState({
                    employee: employee,
                    positionList: data
                });
            }
        );
    }

    changeHandler(e){
        
        let employee = this.state.employee;
        let count = (e.target.name === "name") ? 100 : ((e.target.name === "email") ? 200 : ((e.target.name === "address") ? 300 : 0));
        employee[e.target.name] = (count > 0) ? e.target.value.substr(0, count) : e.target.value;
        const attributeName = e.target.name;
        const attributeValue = e.target.value;
        const correctAttribute = this.validateAttribute(attributeName, attributeValue);
        let newNumberOfErrors = this.getNumberOfErrors();

        this.setState({
            numberOfErrors : newNumberOfErrors,
            employee: employee
        });
    }

    save(){
        let employee = this.state.employee;
        employee.departmentId = this.props.departmentId;

        Controller.addEmployee(employee, true, this.props.saveFunc);
    }

    

    render(){
        const employee = this.props.employee;
        
        const positionOptions = this.state.positionList.map( 
            (el,ind) => <option key={el.id} value={el.id}>{el.name}</option>
        );

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new employee</h3>
                </div>
                <form className="form-horizontal">
                <div className="box-body">
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Name:</label>
                            </div>
                            <div className="col-md-8">
                                <input autoComplete="off" name="name" className="form-control" type="text" 
                                    value={this.state.employee.name}
                                    onChange={this.changeHandler.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Email:</label>
                            </div>
                            <div className="col-md-8">
                                <input autoComplete="off" name="email" className="form-control" type="text" 
                                    value={this.state.employee.email}
                                    onChange={this.changeHandler.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Address:</label>
                            </div>
                            <div className="col-md-8">
                                <input autoComplete="off" name="address" className="form-control" type="text" 
                                    value={this.state.employee.address}
                                    onChange={this.changeHandler.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Employment Date:</label>
                            </div>
                            <div className="col-md-8">
                                <input name="employmentDate" className="form-control" type="date"
                                    value={this.state.employee.employmentDate}
                                    onChange={this.changeHandler.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Employment Hours:</label>
                            </div>
                            <div className="col-md-8">
                                <select name="employmentHours" className="form-control"
                                    value={this.state.employee.employmentHours}
                                    onChange={this.changeHandler.bind(this)}
                                >
                                    <option value="8">8</option>
                                    <option value="6">6</option>
                                    <option value="4">4</option>
                                </select>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Position:</label>
                            </div>
                            <div className="col-md-8">
                                <select name="positionId" className="form-control"
                                    value={this.state.employee.positionId}
                                    onChange={this.changeHandler.bind(this)}
                                >
                                    {positionOptions}
                                </select>
                            </div>
                        </div>
                </div>
                <div>
                    <b>
                        <font color="red">
                            {this.state.nameMessage}<br/>
                            {this.state.emailMessage}<br/>
                            {this.state.addressMessage}<br/>
                            {this.state.employmentDateMessage}
                        </font>
                    </b>
                </div>
                    <div className="box-footer">
                        <div className="btn-toolbar">
                            <button type="button" className="btn btn-md btn-info"
                                onClick={this.save.bind(this)} disabled={this.getNumberOfErrors() > 0}
                            >Add</button>
                            <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}>Cancel</button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }

   /* Validators */

    getNumberOfErrors(){
        let numberOfErrors = 0;

        if(this.state.nameMessage !== ""){
            numberOfErrors++;
        }

        if(this.state.addressMessage !== ""){
            numberOfErrors++;
        }

        if(this.state.emailMessage !== ""){
            numberOfErrors++;
        }

        if(this.state.employmentDateMessage !== ""){
            numberOfErrors++;
        }

        return numberOfErrors;
    }

    validateEmploymentDate(employmentDate){
        const year = parseInt(employmentDate.substr(0, 4));
        const month = parseInt(employmentDate.substr(5, 2));
        const day = parseInt(employmentDate.substr(8, 2));
        if(isNaN(year) || isNaN(month) || isNaN(day)){
            this.setState({
                employmentDateMessage : "Error!!! Employment date is null or invalid."
            });
            return false;
        } else if(year < 1990){
            this.setState({
                employmentDateMessage : "Error!!! Employment date year should be at least 1990."
            });
            return false;
        } else {
            this.setState({
                employmentDateMessage : ""
            });
            return true;
        }
       
    }

    validateName(name){
        if(name === ""){
            this.setState({
                nameMessage : "Error!!! Employee name cannot be empty."
            });
            return false;
        } else if(name.length > 99){
            this.setState({
                nameMessage : "Error! Employee name cannot contain more than 100 characters."
            });
            return false;
        } else {
            this.setState({
                nameMessage : ""
            });
            return true;
        } 
    }

    validateEmail(email){
        if(email === ""){
            this.setState({
                emailMessage : "Error!!! Employee e-mail cannot be empty."
            });
            return false;
        } else if(email.length > 199) {
            this.setState({
                emailMessage : "Error!!! Employee e-mail cannot contain more than 200 characters."
            });
            return false;
        } else {
            this.setState({
                emailMessage : ""
            });
            return true;
        }
    }

    validateAddress(address){
        if(address.length > 299){
            this.setState({
                addressMessage : "Error!!! Employee address cannot contain more than 300 characters."
            });
            return false;
        } else {
            this.setState({
                addressMessage : ""
            });
            return true;
        }
    }

    validateAttribute(attributeName, attributeValue){
        switch(attributeName){
            case 'name' : return this.validateName(attributeValue);
            case 'email' : return this.validateEmail(attributeValue);
            case 'address' : return this.validateAddress(attributeValue);
            case 'employmentDate' : return this.validateEmploymentDate(attributeValue);
        }
    }

   
} 


