import React from 'react';

export default (props) => {

    const employee = props.employee;
    const relDate = employee.releaseDate ? new Date(employee.releaseDate).toLocaleDateString() : " - ";
    const address = employee.address || ' - ';

    const allocationList = props.allocationList.length>0 ? 
        props.allocationList.map(
            (el,index) => <tr key={index}><td>{el.projectName}</td><td>{el.allocationPercentage} %</td></tr>
        ) : (
        <tr><td>(none)</td><td></td></tr>
    );

    return (
        <div className="box info-box">
            <div className="box-header with-border">
                <h3 className="box-title">{employee.name}</h3>
            </div>
            <div className="box-body">
                <table className="table">
                    <tbody>
                        <tr>
                            <td>Name:</td>
                            <td>{employee.name}</td>
                        </tr>
                        <tr>
                            <td>Email:</td>
                            <td>{employee.email}</td>
                        </tr>
                        <tr>
                            <td>Address:</td>
                            <td>{address}</td>
                        </tr>
                        <tr>
                            <td>Employment Date:</td>
                            <td>{new Date(employee.employmentDate).toLocaleDateString()}</td>
                        </tr>
                        <tr>
                            <td>Termination Date:</td>
                            <td>{relDate}</td>
                        </tr>
                        <tr>
                            <td>Employment Hours:</td>
                            <td>{employee.employmentHours}</td>
                        </tr>
                        <tr>
                            <td>Position:</td>
                            <td>{employee.positionName}</td>
                        </tr>
                        <tr>
                            <td>Total Allocation:</td>
                            <td>{employee.totalAllocation} %</td>
                        </tr>
                    </tbody>
                </table>

                <br/><h4>Allocations:</h4><br/>

                <table className="table table-striped table-bordered">
                    <thead>
                        <tr><th>Project</th><th>Allocation</th></tr>
                    </thead>
                    <tbody>
                        {allocationList}
                    </tbody>
                </table>

            </div>

            <div className="box-footer">
                <button type="button" className="btn btn-md btn-info" onClick={props.hideFunc}>Cancel</button>
            </div>
        </div>
    );
}
