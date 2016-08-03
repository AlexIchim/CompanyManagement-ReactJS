import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../../api/config';

export default class Offices extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){
        $.ajax({
            method: "GET",
            url: config.baseUrl+"office/getAll",
            async: false,
            success: function(data){
                this.setState({
                    offices: data
                })
            }.bind(this)

        })
    }

    render(){

        const offices = this.state.offices.map ((office, index) => {
            return <Tile
                    parentClass="bg-aqua"
                    name = {office.Name}
                    phone={office.Phone}
                    address={office.Address}
                    link={"/offices/" + office.Id + "/departments"}
                    editIcon="pencil-square-o fa-3x"
                    icon={office.Image}
                    key={index}
                    />
        })

        return (
            <div>
                <div className="row">

                    {offices}


                </div>

                <br/>

                    <button className="btn btn-primary">Add new office</button>

            </div>

        )
    }
}