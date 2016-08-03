import * as React from 'react';
import Tile from './Tile';

export default class Offices extends React.Component{
    constructor(){
        super();
    }


    render(){
        return (
            <div className="row">


                <Tile
                    parentClass="bg-aqua"
                    phone="213123"
                    address="Address: Ajax requests"
                    link="/change-me"
                    icon="envelope-o"
                />
                <Tile
                    parentClass="bg-green"
                    phone="213123"
                    address="Address: Sample controls"
                    link="/change-me"
                    icon="user"
                />

                <Tile
                    parentClass="bg-yellow"
                    phone="213123"
                    address="Address: Sample joi validation"
                    link="/change-me"
                    icon="users"
                />
                <Tile
                    parentClass="bg-red"
                    phone="213123"
                    address="Address: State manipulation demo"
                    link="/change-me"
                    icon="trash"
                />
            </div>

        )
    }
}