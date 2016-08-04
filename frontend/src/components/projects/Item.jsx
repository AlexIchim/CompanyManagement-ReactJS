import * as React from './react';
import ModalTemplate from '../../layout/ModalTemplate';

export default class Item extends React.Component{
   
    constructor(){
        super();
    }

   render(){
        const linkMembers = ('offices/' + props.officeId + '/departments/' + props.node.departmentId  
                            + '/projects/' + props.node.id + '/members');

        return (
            <tr>
                <td>{props.node.name}</td>
                <td>{props.node.memberCount}</td>
                <td>{props.node.duration || 'variable'}</td>
                <td>{props.node.status}</td>
                <td>
                    <Link to = "#">
                        <button className = "btn btn-md btn-default" onClick = >
                            Edit      
                        </button>
                    </Link>
                    
                    <Link to = {linkMembers}>
                        <button className = "btn btn-md btn-default">
                            View members
                        </button>
                    </Link>
                    
                    <Link to = "#">
                        <button className = "btn btn-md btn-default">
                            Remove
                        </button>
                    </Link>
                </td>
            </tr>
        )
   }
}