import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../helper';

import AddForm from './AddForm';
import EditForm from './EditForm';

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
    }
    onEditButtonClick(index){
    }

    render(){
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
                {items}
                <button className="btn btn-success"
                        onClick={this.onAddButtonClick.bind(this)}>
                    Add
                </button>
            </div>
            
        )
    }
}