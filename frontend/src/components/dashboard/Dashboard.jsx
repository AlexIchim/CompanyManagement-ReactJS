import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import "./../../assets/less/index.less";



export default class Dashboard extends React.Component{
    constructor(){
        super();
        this.state = {
            office: []
        }
    }

    componentWillMount(){
        // /console.log(configs);
        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/office/getAll',
            success: function (data) {
                console.log(data, this);
                this.setState({
                    office: data
                })
            }.bind(this)
        })
    }



    render(){

        const icons= ["moon-o", "cube", "coffee"];

        const items = this.state.office.map((element, index) => {
            return (
                <Tile
                    key = {index}
                    parentClass="bg-aqua"
                    name={element.Name + ' Office'}
                    phone={element.PhoneNumber}
                    address={element.Address}
                    link={"/office/" + element.Id + '/' + element.Name + '/' + 'departments' }
                    icon={icons[index]}
                    office = {element}
                    
                />
            )

        });
        return (
            <div>
                {items}
            </div>

        )
    }
}