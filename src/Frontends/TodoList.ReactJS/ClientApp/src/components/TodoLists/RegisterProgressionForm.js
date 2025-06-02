import React, { useState, useEffect } from "react";

import { todoListQueriesService } from "../../services";
import { splitUserMessage } from "../../helpers";
import ErrorModal from "../UI/ErrorModal/ErrorModal";
import Button from "../UI/Button/Button";
import classes from "./TodoItemForm.module.css";

const RegisterProgressionForm = (props) => {
  const [todoItems, setTodoItems] = useState(props.todoItems || []);
  const [enteredDate, setEnteredDate] = useState("");
  const [enteredPercent, setEnteredPercent] = useState("");
  const [enteredTodoItemId, setEnteredTodoItemId] = useState("");
  const [error, setError] = useState();

  
    useEffect(() => {
    const fetchData = async () => {
      const todoItemList = await todoListQueriesService.getPrintItems();
      if (todoItemList !== null) {
        const todoItemsUpdated = todoItemList.map((auxTodoItemId) => {
          return auxTodoItemId;
        });
        setTodoItems(todoItemsUpdated);
      }
    };

    fetchData();
    return () => {};
  }, []);


  const closeErrorModalHandler = () => {
    setError(null);
  };

  const dateChangeHandler = (event) => {
    setEnteredDate(event.target.value);
  };

  const percentChangeHandler = (event) => {
    setEnteredPercent(event.target.value);
  };

  const todoItemIdChangeHandler = (event) => {
    setEnteredTodoItemId(event.target.value);
  };

  const registerProgressionHandler = () => {
    if (!formValidations()) {
      return;
    }

    const registerProgressionData = {
      date: enteredDate,
      percent: enteredPercent,
      todoItemId: enteredTodoItemId,
    };

    props.onRegisterProgressionData(registerProgressionData);
    cleanForm();
  };

  const formValidations = () => {
    const headerModal = "Form validations";
    let isValid = true;
    let userMessage = "";

    if (enteredDate === undefined || enteredDate.length === 0) {
      userMessage += "* Date is required.\n";
      isValid = false;
    }

    if (enteredPercent === undefined || enteredPercent.length === 0) {
      userMessage += "* Percent is required.\n";
      isValid = false;
    }

    if (!enteredTodoItemId) {
      userMessage += "* Todo-item is required.\n";
      isValid = false;
    }

    if (!isValid) {
      setError({
        date: headerModal,
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
    setEnteredDate("");
    setEnteredPercent("");
    setEnteredTodoItemId("");
  };

  return (
    <>
      {error && (
        <ErrorModal
          key="error-modal"
          date={error.date}
          message={error.message}
          onCloseErrorModal={closeErrorModalHandler}
        />
      )}
      <form>
        <div className={classes["registerProgression__controls"]}>
          <div className={classes["registerProgression__control"]}>
            <label htmlFor="date">Date</label>
            <input
              id="date"
              type="text"
              value={enteredDate}
              onChange={dateChangeHandler}
            />
          </div>
          <div className={classes["registerProgression__control"]}>
            <label htmlFor="todoItem">To-do Item</label>
            <select
              value={enteredTodoItemId}
              onChange={todoItemIdChangeHandler}
            >
              <option value="">Select one</option>
              {todoItems &&
                todoItems.map((todoItem) => (
                  <option key={todoItem.id} value={todoItem.title}>
                    {todoItem.title}
                  </option>
                ))}
            </select>
          </div>
        </div>
        
        <div className={classes["registerProgression__controls"]}>
          <div className={classes["registerProgression__control"]}>
          <label htmlFor="percent">Percent</label>
            <input
              id="percent"
              type="text"
              value={enteredPercent}
              onChange={percentChangeHandler}
            />
          </div>
        </div>
        
        <div className={classes["registerProgression__actions"]}>
          <Button type="button" onClick={cancelActionHandler}>
            Cancel 
          </Button>
          <Button type="button" onClick={registerProgressionHandler}>
            Register progression
          </Button>
        </div>
      </form>
    </>
  );
};

export default RegisterProgressionForm;
