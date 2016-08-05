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
            positionList: []
        };
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
        employee[e.target.name] = e.target.value;

        this.setState({
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

                <div className="box-footer">
                    <div className="btn-toolbar">
                        <button type="button" className="btn btn-default"
                            onClick={this.save.bind(this)}
                        >Add</button>
                        <button type="button" className="btn btn-default" onClick={this.props.hideFunc}>Cancel</button>
                    </div>
                </div>
            </div>
        );
    }
}
