import React from 'react';

import { Button as ButtonStrap } from "reactstrap";
import classes from './Button.module.css';

const Button = (props) => {
  return (
    <ButtonStrap
      type={props.type || 'button'}
      className={`${classes.button} ${props.className}`}
      onClick={props.onClick}
      disabled={props.disabled}
      tag={props.tag}
      to={props.to}
    >
      {props.children}
    </ButtonStrap>
  );
};

export default Button;