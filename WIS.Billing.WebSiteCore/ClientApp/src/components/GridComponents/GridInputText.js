import React from 'react';

const InputText = React.forwardRef((props, ref) => {
    return (
        <input
            ref={ref}
            defaultValue={props.value}
            onBlur={props.onBlur}
            onChange={props.onChange}
            onKeyUp={props.onKeyUp}
            style={props.style}
            className={props.className}
        />
    );
});

export default InputText;