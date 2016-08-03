import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';
import Form from './Form';
import "./../../assets/less/index.less";

class Tile extends React.Component{
    
    constructor(){
        super();
        this.state = {
            edit: false
        }
    }

    showEditForm(){
        console.log(this.state);
        this.setState({
            edit: !this.state.edit
        })
    }

    closeEditForm(){
        this.setState({
            edit: !this.state.edit
        })
    }

    render(){
        const props = this.props;
        const parentClass = classNames("info-box-icon", props['parentClass']);
        const icon = classNames("fa", "fa-" + props['icon']);

        const modal = this.state.edit ? <Form show = {this.state.edit} element = {props['office']}  close={this.closeEditForm.bind(this)} /> : '';
        console.log(modal);

        return (


            <div>

                {modal}

                <div className="col-md-6 col-sm-6 col-xs-12">
                <div className="info-box">
                    <span className={parentClass}><i className={icon}></i></span>
                    <div className="info-box-content">
                        <span className="info-box-name">
                            {props['name']}
                        </span>
                        <span className="info-box-text">
                            {props['address']}
                        </span>
                        <span className="info-box-number">
                            {props['phone']}
                        </span>
                        <Link to={props['link']} className="small-box-footer">
                            View Departments
                        <i className="fa fa-arrow-circle-right"></i></Link>
                        <span>
                        <button className="small-box-top-right" onClick={this.showEditForm.bind(this)}>
                            <i className="fa fa-pencil-square-o fa-3x"></i></button>
                        </span>
                    </div>
                </div>
                </div>



            </div>
        )
    }
}
export default Tile;