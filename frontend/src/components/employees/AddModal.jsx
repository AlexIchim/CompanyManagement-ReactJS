import React from 'react';
import * as Controller from '../../api/controller';

export default class AddModal extends React.Component { 
    constructor(){
        super();
        this.state = {
            name: '',
            email: '',
            address: '',
            employmentDate: '',
            employmentHours: 8,
            positionId: -1,
            positionList: []
        };
    }
    
    componentDidMount(){
        Controller.getPositions(
            true,
            (data) => this.setState({
                positionId: data[0].id,
                positionList: data
            })
        );
    }

    save(){
        Controller.addEmployee({
            name: this.state.name,
            email: this.state.email,
            address: this.state.address,
            employmentDate: this.state.employmentDate,
            employmentHours: this.state.employmentHours,
            positionId: this.state.positionId,
            departmentId: this.props.departmentId,
        },true,this.props.saveFunc);

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
                            <input className="form-control" type="text" 
                                value={this.state.name} 
                                onChange={this.onNameChange.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Email:</label>
                            <input className="form-control" type="text" 
                                value={this.state.email} 
                                onChange={this.onEmailChange.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Address:</label>
                            <input className="form-control" type="text" 
                                value={this.state.address} 
                                onChange={this.onAddressChange.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Employment Date:</label>
                            <input className="form-control" type="date" 
                                value={this.state.employmentDate} 
                                onChange={this.onEmploymentDateChange.bind(this)}/>
                        </div>
                        <div className="form-group">  
                            <label>Employment Hours:</label>
                            <select className="form-control" 
                                value={this.state.employmentHours}
                                onChange={this.onEmploymentHoursChange.bind(this)}
                            >
                                <option value="8">8</option>
                                <option value="6">6</option>
                                <option value="4">4</option>
                            </select>
                        </div>
                        <div className="form-group">  
                            <label>Position:</label>
                            <select className="form-control" 
                                value={this.state.positionId}
                                onChange={this.onPositionChange.bind(this)}
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




    onNameChange(e){
        this.setState({
            name: e.target.value
        });
    }
    onEmailChange(e){ 
        this.setState({
            email: e.target.value
        });
    }
    onAddressChange(e){
        this.setState({
            address: e.target.value
        });
    }
    onEmploymentDateChange(e){
        this.setState({
            employmentDate: e.target.value
        });
    }
    onEmploymentHoursChange(e){
        this.setState({
            employmentHours: e.target.value
        });
    }
    onPositionChange(e){
        this.setState({
            positionId: e.target.value
        });
    }
}
