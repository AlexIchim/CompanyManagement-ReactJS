import * as React from 'react';
import classNames from 'classnames';
import {Link} from 'react-router';

class Tile extends React.Component<any, any>{

    submit(){
        console.log("Hallo");

        var callback=this.props['onEditButtonClick'];
        callback(this.props['index']);
    }

    render(){
        const props = this.props;
        const parentClass = classNames("info-box-icon", props['parentClass']);
        const icon = "data:image/jpg;base64,"+props['icon'];

        return (
            <div className="col-md-6 col-sm-6 col-xs-12">
            <div className="info-box">
                
                <span className={parentClass}><img src={icon}/></span>
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
                <button className="btn btn-success"
                        onClick={this.submit.bind(this)}>
                    Edit
                </button>
            </div>
            </div>
            
            
            


        )
    }
}
export default Tile;