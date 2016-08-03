import React from 'react';

export default class DumbParent extends React.Component {

    componentWillMount() {
    }

    render() {
        return (
            <div  className="wrapper">
                    {this.props.children}
            </div>
        );
    }
}
