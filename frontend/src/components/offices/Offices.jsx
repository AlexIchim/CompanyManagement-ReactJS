import * as React from 'react';
import Tile from './Tile';
import * as $ from 'jquery';
import config from '../../api/config';

import ModalTemplate from '../layout/ModalTemplate';
import AddOffice from './AddOffice';
import EditOffice from './EditOffice';
import * as Controller from '../../api/controller';

import * as Command from '../../context/commands';

export default class Offices extends React.Component{
    constructor(){
        super();
        this.state = {
            officeName: 'Office',
            showModalTemplate: null,
            offices: [],
            modalOffice: {}
        };
    }

    componentWillMount(){
        Command.setCurrentOffice(null);
        this.fetchData();
    }

    fetchData(){
        Controller.getAllOffices(
            true,
            (data) => {
                this.setState({
                    offices: data
                });
            }
        )
     }

    editOffice(office){
        this.setState({
            showModalTemplate: 'edit',
            modalOffice: office
        });
    }

    addOffice(){
        this.setState({
            showModalTemplate: 'add'
        });
    }

    hideModal() {
        this.setState({
            showModalTemplate: null,
            modalOffice: {}
        });
    }

    render(){
        const offices = this.state.offices.map ((office, index) => {
            return <Tile
                parentClass="bg-aqua"
                name = {office.name}
                phone={office.phone}
                address={office.address}
                link={"/offices/" + office.id + "/departments"}
                editIcon="glyphicon glyphicon-pencil fa-2x"
                icon={office.image}
                key={index}
                onEdit={this.editOffice.bind(this, office)}
            />
        })

        const modalTemplate = this.state.showModalTemplate !== null ? (
            <ModalTemplate
                getComponent={
                    (hideFunc) => {
                        switch(this.state.showModalTemplate) {
                            case 'edit':
                                return <EditOffice office={this.state.modalOffice} updateFunc={function(){
                                    hideFunc();
                                    this.fetchData();
                                }.bind(this)} hideFunc={hideFunc}/>;
                            case 'add':
                                return <AddOffice saveFunc={function(){
                                    hideFunc();
                                    this.fetchData();
                                }.bind(this)} hideFunc={hideFunc}/>;
                        }
                    }
                }
                onHide={this.hideModal.bind(this)}
            />
        ) : null;


        return (
            <div>
                <button className="btn btn-md btn-info" onClick={this.addOffice.bind(this)}><span className="glyphicon glyphicon-plus-sign"></span> Add new office</button>
                {modalTemplate}
                <br/><br/>
                <div className="row">

                    {offices}

                </div>

                <br/>


            </div>

        )
    }
}