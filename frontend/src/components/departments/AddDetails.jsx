import React from 'react';
import * as Controller from '../../api/controller';

export default class AddDetails extends React.Component {
    constructor(){
        super();
        this.state = {
            name: '',
            departmentManagerId: -1,
            departmentManagerList: [],
            message: ["Department name cannot be empty.","Choose department manager!!"]
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

        const addButton = this.state.message[0] === ""  && this.state.message[1] === "" ? (<button type="button" className="btn btn-md btn-info" onClick={this.save.bind(this)}>Add</button>)
      :(<button type="button" className="btn  btn-md btn-info" disabled>Add</button>);

        return (
            <div className="box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add Department</h3>
                </div>
                <form className="form-horizontal">
                    <div className="box-body">
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Name:</label>
                            </div>
                            <div className="col-md-8">
                                <input className="form-control" type="text" ref="name"
                                   value={this.state.name}
                                   onChange={this.onNameChange.bind(this)}/>
                             </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Department Manager:</label>
                            </div>
                            <div className="col-md-8">
                                <select className="form-control"
                                        onChange={this.onDepartmentManagerChange.bind(this)}
                                >
                                    <option value="-1">Select your option</option>
                                    {departmentManagers}
                                </select>
                            </div>
                        </div>
                        <div>
                            <font color="red"><b>{this.state.message[0]}</b></font>
                        </div>
                        <div>
                            <font color="red"><b>{this.state.message[1]}</b></font>
                        </div>
                        </div>
                    <div className="box-footer">
                        {addButton}
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
                </form>
            </div>
        );
    }

    onNameChange() {
        const newDepartmentName = this.refs.name.value;
        if (newDepartmentName === "") {
            this.state.message[0] = "Department name cannot be empty.";
        }
        else if (newDepartmentName.length > 100) {
            this.state.message[0] = "Department name cannot be longer than 100 characters.";
        }
        else {
            this.state.message[0] = "";
        }

        this.setState({
            name: newDepartmentName
        });
    }

    onDepartmentManagerChange(e){
        const val = e.target.value;
        this.state.message[1] =  val == -1 ? "Choose department manager!!" : "";

        this.setState({
            departmentManagerId: val
        });
    }
}
