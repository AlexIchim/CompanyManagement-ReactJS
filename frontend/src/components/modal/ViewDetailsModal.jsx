import React from 'react';
import "./../../assets/less/index.less";

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
                            <div className="box-header">

                                <button className="exit-details"  onClick={this.cancel.bind(this)}><i className="fa fa-times-circle fa-2x" aria-hidden="true"></i></button>
                            </div>
                            <form className="form-horizontal">
                                <div className="box-body">
                                    <h3 className="box-title title-position">{this.props.title}</h3>
                                    {this.props.children}
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        )
    }


}