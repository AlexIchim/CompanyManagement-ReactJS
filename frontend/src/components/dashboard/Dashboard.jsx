import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../helper';

export default class Dashboard extends React.Component{
    constructor(){
        super();
    }

    componentWillMount(){
        $.ajax({
            method: 'GET',
            url: config.base + 'office/getAll',
            async: false,
            success: function (data) {
                this.setState({
                    offices: data
                })
            }.bind(this)
        })
    }
    render(){

        const icons = ["user", "users", "trash", "envelope-o", "calendar-o"];
        const items = this.state.offices.map ( (office, index) => {
            return (
                    link="department/members/1"
                />
            )
        })

        return (
            <div className="row">

                {items}

            </div>

        )
    }
}