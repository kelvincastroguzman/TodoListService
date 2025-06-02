import React from "react";

import Button from "../UI/Button/Button";
import Card from "../UI/Card/Card";
import classes from "./TodoItem.module.css";
import ProgressBar from "../UI/ProgressBar/ProgressBar";
import { todoListCommandsService } from "../../services";

const TodoItem = (props) => {
  const deleteHandler = async () => {
    await todoListCommandsService.removeTodoItem(props.todoItemInput.id);
  };

  const updateHandler = () => {
    const todoItemData = {
      id: props.todoItemInput.id,
      title: props.todoItemInput.title,
      description: props.todoItemInput.description,
      category: props.todoItemInput.category,
      isCompleted: props.todoItemInput.isCompleted,
      progressions: props.todoItemInput.progressions.map((progression) => ({
        date: progression.date,
        percent: progression.percent,
      })),
    };

    props.onUpdate(todoItemData);
  };

  let accumulated = 0;

  return (
    <li>
      <Card className={classes["todo-item"]}>
        <div className={classes["todo-item__description"]}>
          <h2>{props.todoItemInput.id}) {props.todoItemInput.title} - {props.todoItemInput.description} ({props.todoItemInput.category}) Completed: {props.todoItemInput.isCompleted.toString()}.<br /></h2>
          <Button onClick={updateHandler} className={classes["todo-item__action-button"]}>Edit</Button>
          <Button onClick={deleteHandler} className={classes["todo-item__action-button"]}>Delete</Button>
        </div>
      </Card>
      <Card className={classes["todo-item"]}>
        <div className={classes["todo-item__description"]}>
          <ul>
            {props.todoItemInput.progressions.map((progression, index) => (
              accumulated += progression.percent,
              <h2>
                <div key={index}>
                  {new Date(progression.date).toLocaleString("en-US")} - {accumulated}% <ProgressBar percentage={accumulated} />
                </div>
              </h2>
            ))}
          </ul>
        </div>
      </Card>
    </li>
  );
};

export default TodoItem;