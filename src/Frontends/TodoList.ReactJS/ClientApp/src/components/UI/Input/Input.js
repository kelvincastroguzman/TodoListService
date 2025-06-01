import React from "react";
import classes from "./Input.module.css";

const Input = (props) => {
  return (
    <div
      className={`${classes.control} ${
        props.stateIsValid === false ? classes.invalid : ""
      }`}
    >
      <label htmlFor={props.id}>{props.labelName}</label>
      <input
        type={props.type}
        id={props.id}
        name={props.id}
        value={props.value}
        onChange={props.onChange}
        onBlur={props.onBlur}
        autoComplete={props.type === "password" ? "on" : "off"}
      />
    </div>
  );
};

export default Input;
