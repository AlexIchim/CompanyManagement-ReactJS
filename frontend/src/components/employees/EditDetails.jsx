import React from 'react';

export default (props) => {

    var employee = props.employee;

    return (
        <div className="box info-box">
            <div className="box-header with-border">
                <h3 className="box-title">{employee.name}</h3>
            </div>
            * <form className="form-horizontal">
            <div className="box-body">
                text...
            </div>

            <div className="box-footer">
                <button type="button" className="btn btn-default"> Edit</button>
                <button type="button" className="btn btn-default" onClick={props.hideFunc}> Cancel</button>
            </div>
        </form>
        </div>
    );
}
