import * as React from 'react';
import ModalTemplate from '../ModalTemplate';
import Context from '../../context/Context';

export default class Form extends React.Component{
    constructor(){
        super();
    }

    onStoreClick(){
        let model=Context.cursor.get("model");

        if(!model){
            model={};
        }

        let name=this.refs.inputName.value;
        //let departmentManagerId = 1;

        model.Name = (name)? name : model.Name;
        model.DepartmentManagerId = 1;
        model.OfficeId = 1;

        Context.cursor.set("model", model);

        this.props.FormAction();
    }

    render(){

        const model=Context.cursor.get('model');

        const name=(model)? model.Name : "Name";

        return(

            <ModalTemplate onCancelClick={this.props.onCancelClick}
                           onStoreClick={this.onStoreClick.bind(this)}
                           Title={this.props.Title}>

                <div className="form-group">

                    <label htmlFor="inputName" className="col-sm-4 control-label">Name</label>
                    <div className="col-sm-8">
                        <input type="text"
                               className="form-control"
                               ref="inputName"
                               placeholder={name}>
                        </input>
                    </div>

                    <label htmlFor="inputDepartmentManager" className="col-sm-4 control-label"> DepartmentManager</label>
                    <div className="dropdown col-sm-8">
                        <button className="btn btn-secondary dropdown-toggle" type="button" ref="departmentManager" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            Dropdown button
                        </button>
                        <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <a className="dropdown-item" href="#">Action</a>
                            <a className="dropdown-item" href="#">Another action</a>
                            <a className="dropdown-item" href="#">Something else here</a>
                        </div>
                    </div>

                </div>

            </ModalTemplate>
        )
    }
}