import React from 'react';
import * as Controller from '../../api/controller';

export default class AddDetails extends React.Component {
    constructor(){
        super();
        this.state = {
            name: '',
            departmentManagerId:-1,
            departmentManagerList: []
        };
    }

    componentDidMount(){
        Controller.getDepartmentsManagers(
            true,
            (data) => {
                this.setState({
                    departmentManagerId: data[0].id,
                    departmentManagerList: data
                });
            }
        );
    }

    save() {
        Controller.addDepartment({
                name: this.state.name,
                officeId: this.props.officeId,
                departmentManagerId: this.state.departmentManagerId
            }, true, this.props.saveFunc);
    }

    render () {

        const departmentManagers = this.state.departmentManagerList.map((element, index) => {
            return (
                <option key={element.id} value={element.id}>{element.name}</option>
            )
        });

        console.log(this.state.departmentManagerList);
        return (
            <div className="box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add Department</h3>
                </div>
                    <div className="box-body">
                        <div className="form-group">
                            <label>Name:</label>
                            <input className="form-control" type="text"
                               value={this.state.name}
                               onChange={this.onNameChange.bind(this)}/>
                        </div>
                        <div className="form-group">
                            <label>Department Manager:</label>
                            <select className="form-control"
                                    value={this.state.departmentManagerId}
                                    onChange={this.onDepartmentManagerChange.bind(this)}
                            >
                                {departmentManagers}
                            </select>
                        </div>
                    </div>

                    <div className="box-footer">
                        <button type="button" className="btn btn-default" onClick={this.save.bind(this)}> Add</button>
                        <button type="button" className="btn btn-default" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
            </div>
        );
    }

    onNameChange(e){
        this.setState({
            name: e.target.value
        });
    }

    onDepartmentManagerChange(e){
        this.setState({
            departmentManagerId: e.target.value
        });
    }
}
