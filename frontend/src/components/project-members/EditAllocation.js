import React from 'react';
import * as Controller from '../../api/controller';

export default class EditOffice extends React.Component {

    constructor(){
        super();
    }

    componentWillMount(){
        this.setState({
            employeeAllocation: this.props.employeeAllocation,
            allocation: {
                id: this.props.employeeAllocation.allocationId,
                allocationPercentage: this.props.employeeAllocation.allocation
            }
        })
    }

    onChangeHandler(e){
        let newAllocation = this.state.allocation;
        newAllocation[e.target.name] = e.target.value;
        this.setState({
            allocation: newAllocation
        })

    }

    onSave(){
        Controller.updateAllocation(
            this.state.allocation,
            true,
            this.props.updateFunc
        )
    }

    render(){
        let freeAllocation = 100 - this.state.employeeAllocation.employee.totalAllocation;
        let name = this.state.employeeAllocation.employee.name;
        let role = this.state.employeeAllocation.employee.positionName;
        let allocationPercentage = this.state.employeeAllocation.allocation;

        return(
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Edit allocation</h3>
                </div>
                <form className="form-horizontal">
                    <div className="box-body">
                    </div>
                    <div className="formBody">
                        <div className="form-group">
                            <div className="col-md-2 leftColoumn">
                                <label className="rightAligned" htmlFor="name">Name:</label>
                            </div>
                            <div className="col-md-8 rightColoumn">
                                <input type="text" className="leftAligned form-control" name="name" defaultValue={name} readOnly />
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label className="rightAligned" htmlFor="address">Role:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" className="leftAligned form-control" name="role" defaultValue={role} readOnly/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label className="rightAligned" htmlFor="phone">Alocation:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="number" min="1" max={freeAllocation} className="leftAligned form-control" name="allocationPercentage" defaultValue={allocationPercentage} onChange={this.onChangeHandler.bind(this)}  />
                            </div>
                        </div>
                    </div>

                    <div className="box-footer">
                        <button type="button" className="btn btn-md btn-info" onClick={this.onSave.bind(this)}> Save</button>
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
                </form>
            </div>

        )


    }

}