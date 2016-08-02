import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';

class Tile extends React.Component<any, any>{
    render(){
        const props = this.props;
        const parentClass = classNames("info-box-icon", props['parentClass']);
        const icon = classNames("fa", "fa-"+props['icon']);
        return (

            <div className="col-md-6 col-sm-6 col-xs-12">
            <div className="info-box">
                <span className={parentClass}><i className={icon}></i></span>
                <div className="info-box-content">
                    <span className="info-box-text">
                        {props['address']}
                    </span>
                    <span className="info-box-number">
                        {props['phone']}
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