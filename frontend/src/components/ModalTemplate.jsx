import React from 'react';

export default class ModalTemplate extends React.Component{

    componentDidMount() {
        const {formModal} = this.refs;
        $(formModal).modal('show');
    }

    cancel(){
        const {formModal} = this.refs;
        this.props['onCancelClick']();
        $(formModal).modal('hide');
    }

    render(){
        return(
            <div ref="formModal" className="modal fade"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
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
                                    <button type="button" className="btn btn-default" onClick={this.cancel.bind(this)} > Cancel</button>
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