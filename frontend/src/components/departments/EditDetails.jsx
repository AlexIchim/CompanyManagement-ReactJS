import React from 'react';

export default (props) => {

    var department = props.department;

    const departmentManagers = props.managers.map((element, index) => {
        return (
            <li key={index}><a>{element.name}</a></li>
        )
    });

    return (
        <div className="box info-box">
            <div className="box-header with-border">
                <h3 className="box-title">Edit Department</h3>
            </div>
            <form className="form-horizontal">
            <div className="box-body">
                <div className="form-group">
                    <label className="col-sm-4 control-label"> Name </label>
                    <div className="col-sm-6">
                        <input className="form-control" placeholder={department.name}/>
                    </div>
                </div>

                <div className="dropdown">
                    <label className="col-sm-4 control-label"> Department manager </label>
                    <button className="btn btn-default dropdown-toggle col-sm-6" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">

                        <span className="caret"></span>
                    </button>
                    <ul className="dropdown-menu col-sm-6" aria-labelledby="dropdownMenu1">
                        {departmentManagers}
                    </ul>
                </div>
            </div>

            <div className="box-footer">
                <button type="button" className="btn btn-default"> Edit</button>
                <button type="button" className="btn btn-default" onClick={props.hideFunc}> Cancel</button>
            </div>
        </form>
        </div>
    );
}
