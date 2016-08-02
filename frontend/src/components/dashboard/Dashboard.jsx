import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import configs from '../helpers/calls'



const Office = (props) => {
    return(
        <div>
            {props.node.Id}
            {props.node.Name }
            {props.node.Address}
            {props.node.PhoneNuber}
        </div>
    )

}

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

        const items = this.state.office.map((element, index) => {
            return (
                <Office
                    node = {element}
                    key = {index}

                />
            )

        });
        return (
            <div>
                <Tile
                    parentClass="bg-aqua"
                    name={items.Name}
                    phone={items.PhoneNumber}
                    address={items.Address}
                    link="/department"
                    // link= configs.baseUrl + 'api/office/getAllDepOffice?officeId=1', {items.Id}
                    icon="envelope-o"
                />
            </div>





            // <aside className="offices">
            //     <div className="row">
            //         <Tile
            //             parentClass="bg-aqua"
            //             name="AAA"
            //             phone="213123"
            //             address="Address: Ajax requests"
            //             link="/department"
            //             // link= configs.baseUrl + 'api/office/getAllDepOffice?officeId=1',
            //             icon="envelope-o"
            //         />
            //         <Tile
            //             parentClass="bg-green"
            //             name="BBB"
            //             phone="213123"
            //             address="Address: Sample controls"
            //             link="/department"
            //             icon="user"
            //         />
            //
            //         <Tile
            //             parentClass="bg-yellow"
            //             name="CCC"
            //             phone="213123"
            //             address="Address: Sample joi validation"
            //             link="/department"
            //             icon="users"
            //         />
            //         <Tile
            //             parentClass="bg-red"
            //             name="DDD"
            //             phone="213123"
            //             address="Address: State manipulation demo"
            //             link="/department"
            //             icon="trash"
            //         />
            //
            //
            //     </div>
            // </aside>

        )
    }
}