import React from 'react';
import * as Controller from '../../api/controller';

export default class AddDetails extends React.Component {
    constructor(){
        super();
        this.state = {
            name: '',
            departmentManagerId: -1,
            departmentManagerList: [],
            message: ["Name can not be empty!", "Choose department manager!!"]
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

        const addButton = this.state.message[0] === "" && this.state.message[1] === "" ? (
            <button className="btn btn-default" onClick={this.save.bind(this)}>Add</button>
        ) : (<button className="btn btn-default" disabled>Add</button>);


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
                                    onChange={this.onDepartmentManagerChange.bind(this)}
                            >
                                <option value="" disabled selected>Select your option</option>
                                {departmentManagers}
                            </select>
                        </div>
                        <div>
                           {this.state.message[0]}
                        </div>
                        <div>
                           {this.state.message[1]}
                        </div>
                    </div>

                    <div className="box-footer">
                        {addButton}
                        <button type="button" className="btn btn-default" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
            </div>
        );
    }

    onNameChange(e){
        const val = e.target.value;
        if (val === "") {
            this.state.message[0] = "Name can not be empty!";
            this.setState({
                name: val
            });
        } else {
            if (val.length > 100) {
                this.setState({
                    name:val
                });
                this.state.message[0] = "Name too long!";
            }
            else {
                this.state.message[0] = ""
                this.setState({
                    name: val
                });
            }
        }
    }

    onDepartmentManagerChange(e){

        const val = e.target.value;
        if (this.state.departmentManagerId == -1) {
            this.state.message[1] = "Choose department manager!!";
            this.setState({
                departmentManagerId: val
            });
        } else {
            this.state.message[1] = "";
            this.setState({
                departmentManagerId: e.target.value
            });
        }
    }
}
