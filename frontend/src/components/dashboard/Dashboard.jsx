import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../helper';

import AddForm from './AddForm';
import EditForm from './EditForm';
import Modal from './Modal';

export default class Dashboard extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){
        $.ajax({
            method:'GET',
            url: config.base+'office/getAll',
            async:false,
            success: function(data){
                console.log("Data: ");
                console.log(data);
                this.setState({
                    offices:data,
                    formToggle:null
                })
            }.bind(this)
        });
    }
    onAddButtonClick(){
        this.setState({
            offices: this.state.offices,
            formToggle: 1
        });
        console.log("Clicked Add Button");
    }
    onEditButtonClick(index){
        console.log("Clicked Edit Button"+index);
        this.setState({
            offices: this.state.offices,
            formToggle: 1,
            office: index
        });
    }

    render(){
        const items=this.state.offices.map((office,index)=>{
            return(
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

        let form;
        if(this.state.formToggle){
            if(this.state.office){
                form=<EditForm/>;
            }else{
                form=<AddForm/>;
            }
        }
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