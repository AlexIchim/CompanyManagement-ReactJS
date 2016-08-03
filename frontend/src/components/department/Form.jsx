import React from 'react';
import ModalTemplate from './ModalTemplate';

export default class Form extends React.Component{

    constructor(){
        super();
        {/*this.state={
            office: {

            }
        }*/}
    }

    componentWillMount(){
        {/*this.setState({
            office: this.props.element
        })*/}
    }

    changeName(){
        {/*const value = this.refs.inputName.value;
        const newOffice = this.state.office;
        newOffice.Name = value;
        this.setState({
            office: newOffice
        })*/}
    }




    componentWillReceiveProps(props){
        console.log('props', props);
    }

    render(){
        return(

            <ModalTemplate close={this.props.close} store={function(){ console.log('haha') }}>

                <div className="form-group">
                    <label htmlFor="inputName" className="col-sm-2 control-label"> Name</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputName" placeholder="Name" value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputAddress" className="col-sm-2 control-label"> Address</label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputAddress" placeholder="Address" value="">
                        </input>
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="inputPhone" className="col-sm-2 control-label"> Phone </label>
                    <div className="col-sm-10">
                        <input type="text" className="form-control" ref="inputPhone" placeholder="Phone" value="">
                        </input>
                    </div>
                </div>

            </ModalTemplate>
        )
    }


}