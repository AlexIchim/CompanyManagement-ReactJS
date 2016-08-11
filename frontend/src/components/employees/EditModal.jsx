import React from 'react';
import * as Controller from '../../api/controller';

export default class EditModal extends React.Component { 
    constructor(){
        super();
        this.state = {
            employee: {
                id: '-1',
                name: '',
                email: '',
                address: '',
                employmentDate: '',
                releaseDate: '',
                employmentHours: 8,
                positionId: -1,
            },
            positionList: [],
            numberOfErrors : 0,
            nameMessage : "",
            emailMessage : "",
            addressMessage : "",
            employmentDateMessage : "",
            releaseDateMessage : ""
        };
    }

    componentDidMount(){
        let employee = this.props.employee;
        for (var prop in employee) {
            if (employee.hasOwnProperty(prop) && employee[prop] === null) {
                employee[prop] = '';
            }
        }


        this.setState({
            employee: employee
        });

        Controller.getPositions(
            true,
            (data) => {
                this.setState({
                    positionList: data
                });
            }
        );

    }

    changeHandler(e){
        let employee = this.state.employee;
        
        let count = (e.target.name === "name") ? 99 : ((e.target.name === "email") ? 199 : ((e.target.name === "address") ? 299 : 0));
        employee[e.target.name] = (count > 0) ? e.target.value.substr(0, count) : e.target.value;
        const attributeName = e.target.name;
        const attributeValue = e.target.value;
        const correctAttribute = this.validateAttribute(attributeName, attributeValue);
        let newNumberOfErrors = this.getNumberOfErrors();

        this.setState({
            employee: employee,
            numberOfErrors : newNumberOfErrors,
        });
    }

    save(){
        let employee = this.state.employee;
        employee.departmentId = this.props.departmentId;
        if(employee.releaseDate === ''){
            employee.releaseDate = null;
        }

        Controller.updateEmployee(employee, true, this.props.saveFunc);
    }


    render(){
        const employee = this.state.employee;

        const positionOptions = this.state.positionList.map( 
            (el,ind) => <option key={el.id} value={el.id}>{el.name}</option>
        );

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new employee</h3>
                </div>
                <div className="box-body">
                    <form>
                        <div className="form-group">  
                            <label>Name:</label>
                            <input name="name" className="form-control" type="text" 
                                value={this.state.employee.name} 
                                onChange={this.changeHandler.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Email:</label>
                            <input name="email" className="form-control" type="text" 
                                value={this.state.employee.email} 
                                onChange={this.changeHandler.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Address:</label>
                            <input name="address" className="form-control" type="text" 
                                value={this.state.employee.address} 
                                onChange={this.changeHandler.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Employment Date:</label>
                            <input name="employmentDate" className="form-control" type="date" 
                                value={this.state.employee.employmentDate} 
                                onChange={this.changeHandler.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Release Date:</label>
                            <input name="releaseDate" className="form-control" type="date" 
                                value={this.state.employee.releaseDate} 
                                onChange={this.changeHandler.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Employment Hours:</label>
                            <select name="employmentHours" className="form-control" 
                                value={this.state.employee.employmentHours}
                                onChange={this.changeHandler.bind(this)}
                            >
                                <option value="8">8</option>
                                <option value="6">6</option>
                                <option value="4">4</option>
                            </select>
                        </div>
                        <div className="form-group">  
                            <label>Position:</label>
                            <select name="positionId" className="form-control" 
                                value={this.state.employee.positionId}
                                onChange={this.changeHandler.bind(this)}
                            >
                                {positionOptions}
                            </select>
                        </div>
                    </form>
                </div>

                <div>
                    <b>
                        <font color="red">
                            {this.state.nameMessage}<br/>
                            {this.state.emailMessage}<br/>
                            {this.state.addressMessage}<br/>
                            {this.state.employmentDateMessage}<br/>
                            {this.state.releaseDateMessage}
                        </font>
                    </b>
                </div>

                <div className="box-footer">
                    <div className="btn-toolbar">
                        <button type="button" className="btn btn-default"
                            onClick={this.save.bind(this)} disabled={this.getNumberOfErrors() > 0}
                        >Save Changes</button>
                        <button type="button" className="btn btn-default" onClick={this.props.hideFunc}>Cancel</button>
                    </div>
                </div>
            </div>
        );
    }

    /*Validators */

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
        } else if(name.length > 100){
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
        } else if(email.length > 200) {
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
       
        if(address.length > 300){
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