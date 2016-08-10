import React from 'react';
import * as Controller from '../../api/controller';

export default class EditDetails extends React.Component {
    constructor(){
        super();
        this.state = {
                department: {
                    id: '-1',
                    name: '',
                    departmentManagerId: 1
                },
                departmentManagerList: [],
                message: ["", ""]
            };
    }

    componentDidMount(){
        let department = this.props.department;
        let copy = {};

        for (var prop in department) {
            if (department.hasOwnProperty(prop)){
                copy[prop] = department[prop];
                if(copy[prop] === null) {
                    copy[prop] = '';
                }
            }
        }

        this.setState({
            department: copy
        });

        Controller.getDepartmentsManagers(
            true,
            (data) => {
                this.setState({
                    departmentManagerList: data
                });
            }
        );
    }

    onChangeFunc(d) {
        let department = this.state.department;
        department[d.target.name] = d.target.value;
        this.setState({
            department: department
        })
    }

    onNameChange(d) {
        let department = this.state.department;
        const val = d.target.value;
        if (val === "") {
            this.state.message[0] = "Name can not be empty!";
            department[d.target.name] = val;
            this.setState({
                department: department
            })
        } else if (val.length > 100) {
            this.state.message[0] = "Name too long!";
            department[d.target.name] = val;
            this.setState({
                department: department
            })
        } else {
            this.state.message[0] = "";
            department[d.target.name] = val;
            this.setState({
                department: department
            })
        }
    }

    save() {
        let department = this.state.department;
        department.officeId = this.props.officeId;


        Controller.updateDepartment(department, true, this.props.saveFunc);
    }

    render () {

        const departmentManagers = this.state.departmentManagerList.map((element, index) => {
            return (
                <option key={element.id} value={element.id}>{element.name}</option>
            )
        });

        const editButton = this.state.message[0] === "" && this.state.message[1] === "" ? (
            <button className="btn btn-md btn-info" onClick={this.save.bind(this)}>Save</button>
        ) : (<button className="btn btn-md btn-info" disabled>Save</button>);

        return (
            <div className="box">
                <div className="box-header with-border">
                    <h3 className="box-title">Edit Department</h3>
                </div>
                <form className="form-horizontal">
                    <div className="box-body">
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Name:</label>
                            </div>
                            <div className="col-md-8">
                                <input name="name" className="form-control" type="text"
                                       value={this.state.department.name}
                                       onChange={this.onNameChange.bind(this)}/>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-4">
                                <label>Department Manager:</label>
                             </div>
                            <div className="col-md-8">
                                <select name="departmentManagerId" className="form-control"
                                        value={this.state.department.departmentManagerId}
                                        onChange={this.onChangeFunc.bind(this)}
                                >
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
                        {editButton}
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
                </form>
            </div>
        );
    }
}
