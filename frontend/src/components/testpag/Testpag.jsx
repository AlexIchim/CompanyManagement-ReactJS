import React, { Component } from 'react';

import PaginatedTable from '../layout/PaginatedTable';


export default class TestPag extends Component {   
    constructor(){
        super();

        //mock up some data -----------------------------------------------------
        let people = [];

        let names = ['William', 'John', 'George', 'Emma', 'Jane'];
        let nr = 103
        for(let i=1; i<=nr; ++i) {
            people.push( {
                name: names[Math.floor(Math.random()*5)]+'The'+i+(
                    i%10===1? 'st' : i%10===2 ? 'nd' : i%10===3 ? 'rd' : 'th'
                ),
                age: Math.floor(Math.random()*5+1)*10
            });
        }
        //---------------------------------------------------------------------EOF

        this.state = {
            people: people,
            totalCount: nr,
            currentPeople: people.slice(0,10),
            pageSize: 10,
            pageNumber: 1
        }
    }
    


    changeHandler(pageSize, pageNumber){
        let skip = (pageNumber-1)*pageSize;
        let take = pageSize;

        this.setState({
            currentPeople: this.state.people.slice(skip, skip+take),
            pageSize: pageSize,
            pageNumber: pageNumber
        });
    }

    
    render() {
        
        let header = (
            <thead>
                <tr>
                    <th>Name:</th>
                    <th>Age:</th>
                </tr>
            </thead>
        );

        let listOfItems = this.state.currentPeople.map(
            (el, ind) => <tr key={ind}>
                            <td>{el.name}</td>
                            <td>{el.age}</td>
                         </tr>
        );

        return (
            <div>
                <h1>Hello</h1><br/>
                
                <PaginatedTable 
                    header={header} 
                    listOfItems={listOfItems}
                    totalCount={this.state.totalCount}
                    pageSize={this.state.pageSize}
                    selectedPage={this.state.pageNumber}
                    changeHandler={this.changeHandler.bind(this)}
                />

            </div>
        );
    }
}