import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import "./../../assets/less/index.less";
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';



export default class Dashboard extends React.Component{
    constructor(){
        super();
        this.state = {
            offices: Context.cursor.get('offices')
        }
    }

    componentWillMount(){

        Context.subscribe(this.onContextChange.bind(this));

        $.ajax({
            method: 'GET',
            url: configs.baseUrl + 'api/office/getAll',
            success: function (data) {
                if(this.state.offices.count() == 0){
                    Context.cursor.set("offices",Immutable.fromJS(data));         
                }
            }.bind(this)
        })
    }

     onContextChange(cursor){
         console.log('aaa',Context.cursor.get("offices"))
        this.setState({
            offices: Context.cursor.get("offices")         
        });

    }


    render(){

        const icons= ["moon-o", "cube", "coffee"];

        const items = this.state.offices.map((element, index) => {
            return (
                <Tile
                    key = {index}
                    parentClass="bg-aqua"
                    name={element.get('Name') + ' Office'}
                    phone={element.get('PhoneNumber')}
                    address={element.get('Address')}
                    link={"/office/" + element.get('Id') + '/' + element.get('Name') + '/' + 'departments' }
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