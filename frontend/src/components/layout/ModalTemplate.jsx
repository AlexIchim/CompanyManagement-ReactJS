import React from 'react';
import $ from 'jquery';
import Modal from 'bootstrap';

export default class  ModalTemplate extends React.Component {

    componentDidMount() {
        const {dialog} = this.refs;
        $(dialog).modal('show');
    }

    hide(){
        const {dialog} = this.refs;
        $(dialog).modal('hide');
        this.props.onHide();
    }

    render(){
        return(
            <div ref="dialog" className="modal"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content">
                            {this.props.getComponent(this.hide.bind(this))}
                    </div>
                </div>
            </div>
        );
    }
}
