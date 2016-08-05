import React from 'react';
import * as Controller from '../../api/controller';

export default class EditOffice extends React.Component {

    constructor(){
        super();
    }

    componentWillMount(){
        this.setState({
            office: this.props.office
        })
    }

    onSave(){
        const newOffice = this.state.office;
        Controller.updateOffice(
            newOffice,
            false,
            this.props.updateFunc
        )

    }

    onChangeHandler(e){
        let officeObject = this.state.office;
        officeObject[e.target.name] = e.target.value;

        this.setState({
            office: officeObject
        })
        console.log(this.state.office);
    }

    render() {
        let name = this.state.office.name;
        let address = this.state.office.address;
        let phone = this.state.office.phone;
        let image = this.state.office.image;

        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Edit office</h3>
                </div>
                <form className="form-horizontal">
                    <div className="box-body">
                    </div>
                    <div className="formBody">
                        <div className="form-group">
                            <div className="col-md-2 leftColoumn">
                                <label htmlFor="name">Name:</label>
                            </div>
                            <div className="col-md-8 rightColoumn">
                                <input type="text" className="form-control" name="name" value={name} onChange={this.onChangeHandler.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label htmlFor="address">Address:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" className="form-control" name="address" value={address} onChange={this.onChangeHandler.bind(this)}/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label htmlFor="phone">Phone:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <input type="text" className="form-control" name="phone" value={phone} onChange={this.onChangeHandler.bind(this)}  />
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2" id="leftColoumn">
                                <label htmlFor="image">Image:</label>
                            </div>
                            <div className="col-md-8" id="rightColoumn">
                                <span className="info-box-icon bg-gray"><i className="glyphicon glyphicon-picture"></i><img src=""/></span>
                                <div className="col-md-6 col-sm-6 col-xs-12">
                                    <button className="btn-default" onClick={this.chooseFile}>Choose image</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="box-footer">
                        <button type="button" className="btn btn-md btn-info" onClick={this.onSave.bind(this)}> Save</button>
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}> Cancel</button>
                    </div>
                </form>
            </div>
        );
    }
}