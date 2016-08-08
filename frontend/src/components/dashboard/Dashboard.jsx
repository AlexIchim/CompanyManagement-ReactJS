import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import "./../../assets/less/index.less";
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import EditForm from './AddForm';



export default class Dashboard extends React.Component{
    constructor(){
        super();
        this.state = {
            offices: Context.cursor.get('offices'),
            add: false
        }
    }

    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

    componentDidMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/office/getAll',
            success: function (data) {
                if(this.state.offices.count() == 0){
                    Context.cursor.set("offices",Immutable.fromJS(data)); 
                }
            }.bind(this)
        })
    }

     componentWillUnmount () {
        this.subscription.dispose(); 
    }

     onContextChange(cursor){
        this.setState({
            offices: Context.cursor.get("offices")         
        });

    }

    showAddForm(){
        this.setState({
            add: true
        });
    }

    closeAddForm(){
        this.setState({
            add: !this.state.add
        })
    }


    render(){

        const icons= ["moon-o", "cube", "coffee"];

        const editModal = this.state.add ? <EditForm show={this.showAddForm.bind(this)} close={this.closeAddForm.bind(this)} /> : ''

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
            )}

            );


        return (
            <div>
                {editModal}

                <button className="btn btn-md btn-info btn-addOffice" onClick={this.showAddForm.bind(this)}> <span className="glyphicon glyphicon-plus-sign"></span> Add new office </button>

                <div className="row">
                    {items}
                </div>


            </div>

        )
    }
}