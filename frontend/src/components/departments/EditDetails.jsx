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
            }
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

        Controller.getDepartmentsManagers(null,
            true,
            (data) => {
                this.setState({
                    departmentManagerId: data[0].id,
                    managers: data
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


    save() {
        let department = this.state.department;
        department.officeId = this.props.officeId;


        Controller.updateDepartment(department, true, this.props.saveFunc);
    }

    render () {
        const departmentManagers = this.props.managers.map((element, index) => {
            return (
                <option key={element.id} value={element.id}>{element.name}</option>
            )
        });

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
                               onChange={this.onChangeFunc.bind(this)}/>
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
                    <button type="button" className="btn btn-default" onClick={this.save.bind(this)}> Update</button>
                    <button type="button" className="btn btn-default" onClick={this.props.hideFunc}> Cancel</button>
                </div>
            </div>
        );
    }
}
