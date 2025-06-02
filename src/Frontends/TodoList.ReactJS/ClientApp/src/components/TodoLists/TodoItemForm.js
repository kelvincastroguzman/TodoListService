import React, { useState } from "react";

import { splitUserMessage } from "../../helpers";
import ErrorModal from "../UI/ErrorModal/ErrorModal";
import Button from "../UI/Button/Button";
import classes from "./TodoItemForm.module.css";

const TodoItemForm = (props) => {
  const [enteredId, setEnteredId] = useState(props.todoItemInput.id);
  const [enteredTitle, setEnteredTitle] = useState(props.todoItemInput.title);
  const [enteredDescription, setEnteredDescription] = useState(props.todoItemInput.description);
  const [enteredCategoryId, setEnteredCategoryId] = useState(props.todoItemInput.category);
  const [error, setError] = useState();

  const closeErrorModalHandler = () => {
    setError(null);
  };

  const titleChangeHandler = (event) => {
    setEnteredTitle(event.target.value);
  };

  const descriptionChangeHandler = (event) => {
    setEnteredDescription(event.target.value);
  };

  const categoryIdChangeHandler = (event) => {
    setEnteredCategoryId(event.target.value);
  };

  const createTodoItemHandler = () => {
    if (!formValidations()) {
      return;
    }

    const todoItemData = {
      id: enteredId,
      title: enteredTitle,
      description: enteredDescription,
      category: enteredCategoryId,
    };

    props.onCreateTodoItemData(todoItemData);
    cleanForm();
  };

  const updateTodoItemHandler = () => {
    if (!formValidations()) {
      return;
    }

    const todoItemData = {
      id: enteredId,
      title: enteredTitle,
      description: enteredDescription,
      category: enteredCategoryId,
    };

    props.onUpdateTodoItemData(todoItemData);
    cleanForm();
  };

  const formValidations = () => {
    const headerModal = "Form validations";
    let isValid = true;
    let userMessage = "";

    if (enteredTitle === undefined || enteredTitle.length === 0) {
      userMessage += "* Title is required.\n";
      isValid = false;
    }

    if (enteredDescription === undefined || enteredDescription.length === 0) {
      userMessage += "* Description is required.\n";
      isValid = false;
    }

    if (!enteredCategoryId) {
      userMessage += "* Category is required.\n";
      isValid = false;
    }

    if (!isValid) {
      setError({
        title: headerModal,
        message: splitUserMessage(userMessage),
      });
    }

    return isValid;
  };

  const cancelActionHandler = () => {
    props.onCancel();
    cleanForm();
  };

  const cleanForm = () => {
    setEnteredId("");
    setEnteredTitle("");
    setEnteredDescription("");
    setEnteredCategoryId("");
  };

  return (
    <>
      {error && (
        <ErrorModal
          key="error-modal"
          title={error.title}
          message={error.message}
          onCloseErrorModal={closeErrorModalHandler}
        />
      )}
      <form>
        <div className={classes["save-todoItem__controls"]}>
          <div className={classes["save-todoItem__control"]}>
            <label htmlFor="title">Title</label>
            <input
              id="title"
              type="text"
              value={enteredTitle}
              onChange={titleChangeHandler}
            />
          </div>
          <div className={classes["save-todoItem__control"]}>
            <label htmlFor="category">Category</label>
            <select
              value={enteredCategoryId}
              onChange={categoryIdChangeHandler}
            >
              <option value="">Select one</option>
              <option key="1" value="Work">Work</option>
              <option key="2" value="Sport">Sport</option>
            </select>
          </div>
        </div>
        
        <div className={classes["save-todoItem__controls"]}>
          <div className={classes["save-todoItem__control"]}>
          <label htmlFor="description">Description</label>
            <textarea
              id="description"
              type="text"
              value={enteredDescription}
              onChange={descriptionChangeHandler}
            />
          </div>
        </div>
        
        <div className={classes["save-todoItem__actions"]}>
          <Button type="button" onClick={cancelActionHandler}>
            Cancel 
          </Button>
          {props.todoItemInput.id === "" && (
            <Button type="button" onClick={createTodoItemHandler}>
              Add To Do Item
            </Button>
          )}
          {props.todoItemInput.id !== "" && (
            <Button type="button" onClick={updateTodoItemHandler}>
              Update To Do Item
            </Button>
          )}
        </div>
      </form>
    </>
  );
};

export default TodoItemForm;
