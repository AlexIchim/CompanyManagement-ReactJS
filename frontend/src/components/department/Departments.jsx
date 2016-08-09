import * as React from 'react';
import config from '../helper';
import * as $ from 'jquery';

const Item = (props) => {
    return (
        <tr>
            <td>{props.element['Id']}</td>
            <td className="test-class">{props.element['Name']}</td>
            <td><a href="">Edit</a> </td>
        </tr>
    )
}

class Departments extends React.Component{

    constructor(){
        super();
    }

    componentWillMount() {
        const officeId = this.props.routeParams['officeId'];
        $.ajax({
            method: 'GET',
            url: config.base + 'department/getAll',
            async: false,
            success: function(data){
                console.log(data);
                this.setState({
                    departments: data
                })
            }.bind(this)
        })








    }


    render(){

        const items = this.state.departments.map( (element, i) => {
            return (
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
                 <td>Actions</td>
             </tr>
             </thead>
             <tbody>
             {items}
             </tbody>
         </table>
     )
 }
}

export default Departments;