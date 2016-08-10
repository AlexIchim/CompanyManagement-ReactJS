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
                <form className="form-horizontal">
                <div className="box-body">
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Name:</label>
                            </div>
                            <div className="col-md-8">
                                <input name="name" className="form-control" type="text"
                                    value={this.state.employee.name}
                                    onChange={this.changeHandler.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Email:</label>
                            </div>
                            <div className="col-md-8">
                                <input name="email" className="form-control" type="text"
                                    value={this.state.employee.email}
                                    onChange={this.changeHandler.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Address:</label>
                            </div>
                            <div className="col-md-8">
                                <input name="address" className="form-control" type="text"
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

                    <div className="box-footer">
                        <div className="btn-toolbar">
                            <button type="button" className="btn btn-md btn-info"
                                onClick={this.save.bind(this)}
                            >Add</button>
                            <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}>Cancel</button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}
