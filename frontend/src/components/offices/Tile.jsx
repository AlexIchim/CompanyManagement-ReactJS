import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';
import "./../../assets/less/index.less";

class Tile extends React.Component<any, any>{
    render(){
        const props = this.props;
        const parentClass = classNames("info-box-icon", props['parentClass']);
        return (

            <div className="col-md-6 col-sm-6 col-xs-12">
            <div className="info-box">
                <span className={parentClass}><img className="uploadImage img-thumbnail" src={'data:image/jpg;base64,'+props['icon']}></img></span>
                <button className="editIcon" onClick={props['onEdit']}><i className={props['editIcon']}></i></button>
                <div className="info-box-content">
                    <span className="info-box-text">
                        {props['name']}
                    </span>
                    <span className="info-box-text">
                        {props['address']}
                    </span>
                    <span className="info-box-number">
                        T: {props['phone']}
                    </span>
                    <Link to={props['link']} className="small-box-footer">
                        View Departments
                    <i className="fa fa-arrow-circle-right"></i></Link>
                </div>
            </div>
            </div>

        )
    }
}
export default Tile;