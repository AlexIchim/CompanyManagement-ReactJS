import React from 'react';

export default class ModalTemplate extends React.Component{

    componentDidMount(){
        const formModal=this.refs.formModal;
        $(formModal).modal('show');
    }
    onStoreClick(){
        const formModal=this.refs.formModal;
        $(formModal).modal('hide');
        this.props.onStoreClick();
    }
    onCancelClick(){
        const formModal=this.refs.formModal;
        $(formModal).modal('hide');
        this.props.onCancelClick();
    }

    render(){

        const storeLabel="Save";

        return(
            <div ref="formModal" className="modal fade"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="box info-box">
                            <div className="box-header with-border">
                                <h3 className="box-title">{this.props['Title']}</h3>
                            </div>
                            <form className="form-horizontal">
                                <div className="box-body">
                                    {this.props.children}
                                </div>

                                <div className="box-footer">
                                    <button type="button" className="btn btn-default" onClick={this.onCancelClick.bind(this)} > Cancel</button>
                                    <button type="button" className="btn btn-default" onClick={this.onStoreClick.bind(this)} > {storeLabel}</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}