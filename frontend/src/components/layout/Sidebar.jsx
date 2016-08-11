import * as React from 'react';
import {Link} from 'react-router';
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';
import * as Controller from '../controller';
import * as $ from 'jquery';
import configs from '../helpers/calls'

export default class Sidebar extends React.Component{

    constructor(){
        super();
        this.setState({
          offices: {

          }
        })
    }

    componentDidMount(){
        $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/office/getAll',
            success: function (data) {
               this.setState({
                   offices: Immutable.fromJS(data)
               })
            }.bind(this)
        })
    }

    componentWillMount(){
        this.subscription = Context.subscribe(this.onContextChange.bind(this));
    }

    onContextChange(cursor){
        this.setState({
            offices: cursor.get("offices")
        });

    }
    componentWillUnmount () {
        this.subscription.dispose();
    }


    render(){

        const items = this.state.offices.map((el, x) => {
            return (
                <li key={x} > <Link to={"/office/" + el.get('Id') + '/' + el.get('Name') + '/' + 'departments' }>{el.get('Name')} </Link></li>
            )
        });

        return(

        <div>
            <aside className="main-sidebar main-sidebar-custom">
                <section className="sidebar sidebar-custom">
                    <h1>Firm offices</h1>
                    <hr/>
                    <ul className="sidebar-menu">
                        {items}
                    </ul>
                </section>
            </aside>
        </div>
        )
    }







}