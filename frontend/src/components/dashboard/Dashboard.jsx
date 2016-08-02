import * as React from 'react';
import Tile from './Tile';

export default class Dashboard extends React.Component{
    constructor(){
        super();
    }


    render(){
        return (
            <aside className="offices">
                <div className="row">
                    <Tile
                        parentClass="bg-aqua"
                        name="AAA"
                        phone="213123"
                        address="Address: Ajax requests"
                        link="/department"
                        icon="envelope-o"
                    />
                    <Tile
                        parentClass="bg-green"
                        name="BBB"
                        phone="213123"
                        address="Address: Sample controls"
                        link="/department"
                        icon="user"
                    />

                    <Tile
                        parentClass="bg-yellow"
                        name="CCC"
                        phone="213123"
                        address="Address: Sample joi validation"
                        link="/department"
                        icon="users"
                    />
                    <Tile
                        parentClass="bg-red"
                        name="DDD"
                        phone="213123"
                        address="Address: State manipulation demo"
                        link="/department"
                        icon="trash"
                    />
                </div>
            </aside>

        )
    }
}