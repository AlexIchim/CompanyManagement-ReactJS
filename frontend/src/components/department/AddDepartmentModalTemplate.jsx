import React from 'react';
export default class  AddDepartmentModalTemplate extends React.Component{


    componentDidMount() {
        const {addDepartment} = this.refs;
        $(addDepartment).modal('show');
    }

    cancel(){
        const {addDepartment} = this.refs;
        $(addDepartment).modal('hide');
        this.props.close();
    }

    render(){
        
        return(
            <div ref="addDepartment" className="modal fade"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="box info-box">
                            <div className="box-header with-border">
                                <h3 className="box-title">Add new department</h3>
                            </div>
                            <form className="form-horizontal">
                                <div className="box-body">
                                    {this.props.children}
                                </div>

                                <div className="box-footer">
                                    <button type="submit" className="btn btn-default" onClick={this.props.store.bind(this, this.cancel.bind(this))} > Add </button>
                                    <button type="button" className="btn btn-default" onClick={this.cancel.bind(this)}> Cancel</button>     
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }


}