import React from "react";

import Button from "../UI/Button/Button";
import TodoItemForm from "./TodoItemForm";
import classes from "./SaveTodoItem.module.css";

const SaveTodoItem = (props) => {
  const createTodoItemDataHandler = (enteredTodoItemData) => {
    const todoItemData = {
      ...enteredTodoItemData,
    };
    props.onCreateTodoItem(todoItemData);
  };

  const updateTodoItemDataHandler = (enteredTodoItemData) => {
    const todoItemData = {
      ...enteredTodoItemData,
    };
    props.onUpdateTodoItem(todoItemData);
  };

  return (
    <div className={classes["save-todoItem"]}>
      {!props.isEditing && (
        <Button onClick={props.onOpenNewForm}>Add New To Do Item</Button>
      )}
      {props.isEditing && (
        <TodoItemForm
          todoItemInput={props.todoItemInput}
          todoItems={props.todoItems}
          onCreateTodoItemData={createTodoItemDataHandler}
          onUpdateTodoItemData={updateTodoItemDataHandler}
          onCancel={props.onCloseEditForm}
        />
      )}
    </div>
  );
};

export default SaveTodoItem;