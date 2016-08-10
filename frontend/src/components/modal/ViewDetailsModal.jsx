import React from 'react';
export default class  ViewDetailsModal extends React.Component{


    componentDidMount() {
        const {modal} = this.refs;
        $(modal).modal('show');
    }

    cancel(){
        const {modal} = this.refs;
        $(modal).modal('hide');
        this.props.close();    
    }

    render(){
        
        return(
            <div ref="modal" className="modal fade modal-all"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="box info-box modal-box">
                            <div className="box-header with-border">
                                <h3 className="box-title">{this.props.title}</h3>
                            </div>
                            <form className="form-horizontal">
                                <div className="box-body">
                                    {this.props.children}
                                </div>

                                <div className="box-footer modal-footer">
                                    <button type="button" className="col-sm-2 btn btn-default cancel-button" onClick={this.cancel.bind(this)}> Cancel</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }


}