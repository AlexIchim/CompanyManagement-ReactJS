import React from 'react';

export default class ModalTemplate extends React.Component{
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
                                    <button type="button" className="btn btn-default" onClick={this.props.onCancelClick} > Cancel</button>
                                    <button type="submit" className="btn btn-default" onClick={this.props.onStoreClick} > {storeLabel}</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}