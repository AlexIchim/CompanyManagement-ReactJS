import React from 'react';

export default class ModalTemplate extends React.Component{

    componentDidMount() {
        const {editDepartment} = this.refs;
        $(editDepartment).modal('show');
    }

    cancel(){
        const {editDepartment} = this.refs;
        $(editDepartment).modal('hide');
        this.props.close();
    }

    render(){
        console.log('iiinside', this.props)
        return(
            <div ref="editDepartment" className="modal fade"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="box info-box">
                            <div className="box-header with-border">
                                <h3 className="box-title">Edit Office</h3>
                            </div>
                            <form className="form-horizontal">
                                <div className="box-body">
                                    {this.props.children}
                                </div>

                                <div className="box-footer">
                                    <button type="button" className="btn btn-default" onClick={this.cancel.bind(this)}> Cancel</button>
                                    <button type="submit" className="btn btn-default" onClick={this.props.store} > Edit</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}