import React from 'react';
import ModalTemplate from '../../ModalTemplate';
import Context from'../../../context/Context';

export default class EditAllocationForm extends React.Component{

    constructor(){
        super();
    }
    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }
    onContextChange(newGlobalCursor){
        this.setState({
            model: newGlobalCursor.get('model'),
            projectAllocation: newGlobalCursor.get('model') && newGlobalCursor.get('model').Allocation || ""
        })
    }

    onAllocationChange() {
        this.setState({
            model: this.state.model,
            projectAllocation: this.refs.inputAllocation.value
        })
    }
    render(){
        let projectAllocation = this.state.projectAllocation;
        return(
            <ModalTemplate
                onCancelClick={this.props.onCancelClick}
                onStoreClick={this.onStoreClick.bind(this)}
            >
                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> New Allocation </label>
                    <div className="col-sm-10">
                        <input type="text"
                               ref="inputAllocation"
                               className="form-control"
                               onChange={this.onAllocationChange.bind(this)}
                               value = {projectAllocation}
                        >
                        </input>
                    </div>
                </div>


            </ModalTemplate>
        )
    }
}