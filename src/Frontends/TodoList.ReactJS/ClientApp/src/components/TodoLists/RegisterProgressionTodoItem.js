import React from "react";

import Button from "../UI/Button/Button";
import RegisterProgressionForm from "./RegisterProgressionForm";
import classes from "./RegisterProgressionTodoItem.module.css";

const RegisterProgressionTodoItem = (props) => {
  const registerProgressionDataHandler = (enteredRegisterProgressionData) => {
    const registerProgressionData = {
      ...enteredRegisterProgressionData,
    };
    props.onRegisterProgression(registerProgressionData);
  };

  return (
    <div className={classes["registerProgression-todoItem"]}>
      {!props.isEditing && (
        <Button onClick={props.onOpenNewForm}>Register progression</Button>
      )}
      {props.isEditing && (
        <RegisterProgressionForm
          todoItems={props.todoItems}
          onRegisterProgressionData={registerProgressionDataHandler}
          onCancel={props.onCloseEditForm}
        />
      )}
    </div>
  );
};

export default RegisterProgressionTodoItem;