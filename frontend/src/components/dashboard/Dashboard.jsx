import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../helper';

import Form from './Form';

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
                    formToggle:false
                })
            }.bind(this)
        });
    }
    onAddButtonClick(){
    }
    onEditButtonClick(index){
    }

    

    toggleModal(){
        this.setState({
            offices:this.state.offices,
            formToggle:!this.state.formToggle
        })
    }

    render(){
        let form="";
        if(this.state.formToggle){
            form=<Form onCancelClick={this.toggleModal.bind(this)}/>;
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
                        onClick={this.toggleModal.bind(this)}>
                    Add
                </button>
            </div>
            
        )
    }
}