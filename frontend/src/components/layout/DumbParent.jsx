import React from 'react';

export default class DumbParent extends React.Component {

    componentWillMount() {
    }

    render() {
        return (
            <div>
                    {this.props.children}
            </div>
        );
    }
}
