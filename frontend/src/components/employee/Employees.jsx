import * as React from 'react';
import  config from '../helper';
import * as $ from 'jquery';

const Item = (props) => {
    return(
        <tr>
            <td>{props.element['Name']}</td>
            <td>{props.element['Address']}</td>
        </tr>
    )
}

class Employees extends React.Component{

    constructor(){
        super();
    }

    componentWillMount(){

        const departmentId = this.props.routeParams['departmentId'];

        $.ajax({
            method: 'GET',
            url: config.base + 'department/members/' + departmentId,
            async: false,
            success: function(data) {
                this.setState({
                    employees: data
                })
            }.bind(this)
        })

    }

    render(){
        const items = this.state.employees.map( (element, i) => {
           return(
               <Item
                   element={element}
                   key={i}
               />
           )
        });

        return (
        <table className="table table-hover">
            <thead>
            <tr>
                <td>Id</td>
                <td>Name</td>
                <td>Employee Id</td>
            </tr>
            </thead>
            <tbody>

            {items}

            </tbody>
        </table>
        )}


}

export default Employees;