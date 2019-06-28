import React, { Component } from 'react';
import ScrollbarSize from 'react-scrollbar-size';

const { Provider, Consumer } = React.createContext();

//TODO: Ver de separar en distintos componentes

class ScrollContextProvider extends Component {
    constructor(props) {
        super(props);

        this.state = {
            scrollbarWidth: 0,
            scrollbarHeight: 0,
            isScrolling: false
        };
    }

    scrollbarSizeLoad = (measurements) => {
        this.setState({
            scrollbarHeight: measurements.scrollbarHeight,
            scrollbarWidth: measurements.scrollbarWidth
        });
    }
    scrollbarSizeChange = (measurements) => {
        this.setState({
            scrollbarHeight: measurements.scrollbarHeight,
            scrollbarWidth: measurements.scrollbarWidth
        });
    }

    onScrollBegin = () => {
        if (this.timer)
            clearTimeout(this.timer);
        
        if (!this.state.isScrolling) {            
            this.setState({
                isScrolling: true
            });
        }        

        this.timer = setTimeout(() => this.setState({ isScrolling: false }), 100);
    }

    render() {
        return (
            <Provider
                value={{
                    scrollbarWidth: this.state.scrollbarWidth,
                    scrollbarHeight: this.state.scrollbarHeight,
                    isScrolling: this.state.isScrolling,
                    perfScrollbarUpdate: this.perfScrollbarUpdate,
                    onScrollBegin: this.onScrollBegin
                }}
            >
                <ScrollbarSize onLoad={this.scrollbarSizeLoad} onChange={this.scrollbarSizeChange} />
                {this.props.children}
            </Provider>
        );
    }
}

export { ScrollContextProvider };
export default Consumer;