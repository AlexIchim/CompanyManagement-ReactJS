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
        for (var prop in department) {
            if (department.hasOwnProperty(prop) && department[prop] === null) {
                department[prop] = '';
            }
        }

        this.setState({
            department: department
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

    onNameChange(e){
        const val = e.target.value;
        if (val === "") {
            this.state.message[0] = "Name can not be empty!";
            this.setState({
                department: {
                    name : val
                }
            });
        } else {
            if (val.length > 100) {
                this.state.message[0] = "Name too long!";
                this.setState({
                    department: {
                        name : val
                    }
                });
            }
            else {
                this.state.message[0] = ""
                this.setState({
                    department: {
                        name : val
                    }
                });
            }
        }
        console.log(this.state.department.name);
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
            <button className="btn btn-default" onClick={this.save.bind(this)}>Update</button>
        ) : (<button className="btn btn-default" disabled>Update</button>);

        return (
            <div className="box">
                <div className="box-header with-border">
                    <h3 className="box-title">Edit Department</h3>
                </div>
                <div className="box-body">
                    <div className="form-group">
                        <label>Name:</label>
                        <input name="name" className="form-control" type="text"
                               value={this.state.department.name}
                               onChange={this.onNameChange.bind(this)}/>
                    </div>
                    <div className="form-group">
                        <label>Department Manager:</label>
                        <select name="departmentManagerId" className="form-control"
                                value={this.state.department.departmentManagerId}
                                onChange={this.onChangeFunc.bind(this)}
                        >
                            {departmentManagers}
                        </select>
                    </div>
                </div>

                <div className="box-footer">
                    {editButton}
                    <button type="button" className="btn btn-default" onClick={this.props.hideFunc}> Cancel</button>
                </div>
            </div>
        );
    }
}
