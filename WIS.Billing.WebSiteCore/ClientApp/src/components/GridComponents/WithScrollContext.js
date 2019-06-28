import React, { Component } from 'react';
import Consumer from './ScrollContextProvider';

export default function withScrollContext(WrappedComponent) {
    return class WithScrollContext extends Component {
        render() {
            return (
                <Consumer>
                    {scrollContext => (
                        <WrappedComponent scrollContext={scrollContext} {...this.props} />
                    )}
                </Consumer>
            );
        }
    };
}