import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../helper';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';
import Form from './Form';

export default class Dashboard extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){
        Context.subscribe(this.onContextChange.bind(this));

        $.ajax({
            method:'GET',
            url: config.base+'office/getAll',
            async:false,
            success: function(data){
                console.log("Data: ");
                console.log(data);
                
                Context.cursor.set('items',data);
                Context.cursor.set('formToggle',false);
            }.bind(this)
        });
    }

    onContextChange(cursor){
        this.setState({
            offices: cursor.get('items')
        });
    }

    onAddButtonClick(){
        Context.cursor.set('formToggle',true);
    }
    onEditButtonClick(index){
        const office=this.state.offices[index];

        Context.cursor.set("model", office);
        Context.cursor.set('formToggle',true);
    }

    onModalSaveClick(){
        console.log("STORING!");
      //Controller.hideModal();
    }

    render(){
        let form="";
        if(Accessors.formToggle(Context.cursor)){
            if(Accessors.model(Context.cursor)){
                form=<Form onCancelClick={Controller.hideModal.bind(this)}
                           onStoreClick={this.onModalSaveClick.bind(this)}
                           Title="Edit Office"/>;
            }else{
                form=<Form onCancelClick={Controller.hideModal.bind(this)}
                           onStoreClick={this.onModalSaveClick.bind(this)}
                           Title="Add Office"/>;
            }
        }

        const items = this.state.offices.map ( (office, index) => {
            return (
                <Tile
                    parentClass="bg-aqua"
                    phone={office.Phone}
                    address={office.Address}
                    link={"Departments/"+office.Id}
                    icon={office.Image}
                    key={index}
                    index={index}
                    onEditButtonClick={this.onEditButtonClick.bind(this)}
                />
            );
        })

        return (
            <div className="row">
                {form}
                {items}
                <button className="btn btn-success"
                        onClick={this.onAddButtonClick.bind(this)}>
                    Add
                </button>
            </div>
            
        )
    }
}