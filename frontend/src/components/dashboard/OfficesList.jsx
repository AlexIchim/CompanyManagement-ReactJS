import * as React from 'react';
import config from '../helper';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';
import '../../assets/less/index.less';
import Controller from './OfficeController';
import SidebarTile from './OfficeSidebarTile'
export default class OfficesList extends React.Component{
    constructor(){
        super();
    }
    componentWillMount(){

        this.subscription=Context.subscribe(this.onContextChange.bind(this));

        Controller.GetPartial();
    }

    componentWillUnmount(){
        console.log('unmount dep')
        this.subscription.dispose();
    }

    onContextChange(cursor){
        //console.log("Sidebar: ", Accessors.sidebarOffices(cursor))
    }
    
    render(){
        const items = Accessors.sidebarOffices(Context.cursor).map ( (office, index) => {
            return (
                <SidebarTile
                    name={office.Name}
                    link={"office/departments/"+office.Id}
                    key={index}
                    index={index}>
                </SidebarTile>
            );
        })

        return (
            <div>
                {items}
            </div>
        )
    }
}