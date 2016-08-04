import React from 'react';
export default class  AddProjectModalTemplate extends React.Component{


    componentDidMount() {
        const {addProject} = this.refs;
        $(addProject).modal('show');
    }

    cancel(){
        const {addProject} = this.refs;
        $(addProject).modal('hide');
        this.props.close();
    }

    render(){

        return(
            <div ref="addProject" className="modal fade"  tabIndex="-1" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="box info-box">
                            <div className="box-header with-border">
                                <h3 className="box-title">Add new project</h3>
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