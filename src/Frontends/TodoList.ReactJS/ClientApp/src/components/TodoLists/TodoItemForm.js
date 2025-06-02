import React, { useState, useEffect } from "react";

import { todoListQueriesService } from "../../services";
import { splitUserMessage } from "../../helpers";
import ErrorModal from "../UI/ErrorModal/ErrorModal";
import Button from "../UI/Button/Button";
import classes from "./TodoItemForm.module.css";

const TodoItemForm = (props) => {
  const [categories, setCategories] = useState([]);
  const [enteredId, setEnteredId] = useState(props.todoItemInput.id);
  const [enteredNextTodoItemId, setEnteredNextTodoItemId] = useState(0);
  const [enteredTitle, setEnteredTitle] = useState(props.todoItemInput.title);
  const [enteredDescription, setEnteredDescription] = useState(props.todoItemInput.description);
  const [enteredCategoryValue, setEnteredCategoryValue] = useState(props.todoItemInput.category);
  const [error, setError] = useState();

    useEffect(() => {
    const fetchData = async () => {
      const nextTodoItemId = await todoListQueriesService.getNextTodoItemId();
      setEnteredNextTodoItemId(nextTodoItemId);
      const categoryList = await todoListQueriesService.getAllCategories();
      if (categoryList !== null) {
        const categoriesUpdated = categoryList.map((auxCategory) => {
          return auxCategory;
        });
        setCategories(categoriesUpdated);
      }
    };

    fetchData();
    return () => {};
  }, []);

  const closeErrorModalHandler = () => {
    setError(null);
  };

  const titleChangeHandler = (event) => {
    setEnteredTitle(event.target.value);
  };

  const descriptionChangeHandler = (event) => {
    setEnteredDescription(event.target.value);
  };

  const categoryValueChangeHandler = (event) => {
    setEnteredCategoryValue(event.target.value);
  };

  const createTodoItemHandler = () => {
    if (!formValidations()) {
      return;
    }

    const todoItemData = {
      id: enteredNextTodoItemId,
      title: enteredTitle,
      description: enteredDescription,
      category: enteredCategoryValue,
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
      category: enteredCategoryValue,
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

    if (!enteredCategoryValue) {
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
    setEnteredCategoryValue("");
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
              value={enteredCategoryValue}
              onChange={categoryValueChangeHandler}
            >
              <option value="">Select one</option>
              {categories &&
                categories.map((category) => (
                  <option key={category} value={category}>
                    {category}
                  </option>
                ))}
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
