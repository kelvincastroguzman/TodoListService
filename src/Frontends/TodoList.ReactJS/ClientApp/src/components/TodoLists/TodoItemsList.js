import React from "react";

import TodoItem from "./TodoItem";
import classes from "./TodoItemsList.module.css";

const TodoItemsList = (props) => {
  if (props.todoItems.length === 0) {
    return (
      <h2 className={classes["todoItems-list__fallback"]}>
        Not found To Do Items.
      </h2>
    );
  }

  return (
    <ul className={classes["todoItems-list"]}>
      {props.todoItems.map((todoItem) => (
        <TodoItem
          key={todoItem.uniqueId}
          todoItemInput={todoItem}
          onUpdate={props.onOpenEditForm}
        />
      ))}
    </ul>
  );
};

export default TodoItemsList;