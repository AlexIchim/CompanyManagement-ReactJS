import React from 'react';
import "./../../assets/less/index.less";
import * as Controller from '../../api/controller';

export default class AddOffice extends React.Component{

    addOffice()
    {
        let officeObject = {
            name: this.refs.name.value,
            address: this.refs.address.value,
            phone: this.refs.phone.value,
            image: this.refs.image.value
        }
        Controller.addNewOffice(
            officeObject,
            false,
            this.props.saveFunc
        )
    }

    render()
    {
        return (
            <div className="box info-box">
                <div className="box-header with-border">
                    <h3 className="box-title">Add new office</h3>
                </div>
                <form className="form-horizontal">
                    <div className="box-body">
                    </div>
                    <div className="formBody">
                        <div className="form-group">
                            <div className="col-md-2 leftColoumn">
                                <label className="rightAligned" htmlFor="name">Name:</label>
                            </div>
                            <div className="col-md-8 rightColoumn">
                                <input required="" type="text" className="leftAligned form-control" ref="name"/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label htmlFor="address" className="rightAligned">Address:</label>
                            </div>
                            <div className="col-md-8">
                                <input type="text" className="leftAligned form-control" ref="address"/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label className="rightAligned" htmlFor="phone">Phone:</label>
                            </div>
                            <div className="col-md-8">
                                <input type="text" className="leftAligned form-control" ref="phone"/>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-2">
                                <label className="rightAligned" htmlFor="image">Image:</label>
                            </div>
                            <div className="col-md-8">
                                <span className="info-box-icon bg-gray"><i className="glyphicon glyphicon-picture"></i><img src=""/></span>
                                <div className="col-md-6 col-sm-6 col-xs-12">
                                    <input type="file" ref="image" defaultValue="Choose image"/>
                                    {/*<button className="btn-default" onClick={this.chooseFile}>Choose image</button>*/}
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="box-footer">
                        <button type="button" className="btn btn-md btn-info" onClick={this.addOffice.bind(this)}>
                            Add
                        </button>
                        <button type="button" className="btn btn-md btn-info" onClick={this.props.hideFunc}>
                            Cancel
                        </button>
                    </div>
                </form>
            </div>
        );
    }
}