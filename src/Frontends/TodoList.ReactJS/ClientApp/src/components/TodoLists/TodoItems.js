import React, { useState, useEffect } from "react";

import { todoListQueriesService, todoListCommandsService } from "../../services";
import SaveTodoItem from "./SaveTodoItem";
import RegisterProgressionTodoItem from "./RegisterProgressionTodoItem";
import TodoItemsList from "./TodoItemsList";
import ErrorModal from "../UI/ErrorModal/ErrorModal";
import Card from "../UI/Card/Card";
import PageContainer from "../UI/PageContainer/PageContainer";
import classes from "./TodoItems.module.css";

const TodoItems = () => {
  const [todoItems, setTodoItems] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [todoItemInput, setTodoItemInput] = useState({
    id: "",
    title: "",
    description: "",
    category: "",
    isCompleted: "",
    progressions: [],
  });
  const [isEditingRegisterProgression, setIsEditingRegisterProgression] = useState(false);
  const [registerProgressionInput, setRegisterProgressionInput] = useState({
    todoItemId: "",
    date: "",
    percent: "",
  });
  const [error, setError] = useState();

  useEffect(() => {
    const fetchData = async () => {
      getPrintItems();
    };

    fetchData();
    return () => {};
  }, []);

  const getPrintItems = async () => {
    const todoItemList = await todoListQueriesService.getPrintItems();
    if (todoItemList !== null) {
      const todoItemsUpdated = todoItemList.map((auxTodoItem) => {
        const updatedItem = {
          id: auxTodoItem.id,
          title: auxTodoItem.title,
          description: auxTodoItem.description,
          category: auxTodoItem.category,
          isCompleted: auxTodoItem.isCompleted,
          progressions: auxTodoItem.progressions.map((progression) => ({
            date: progression.date,
            percent: progression.percent,
          })),
        };
        return updatedItem;
      });
  
      setTodoItems(todoItemsUpdated);
    }
  }

  const closeErrorModalHandler = () => {
    setError(null);
  };

  const createTodoItemHandler = async (todoItem) => {
    setIsEditing(false);
    await todoListCommandsService.createTodoItem(todoItem);
    getPrintItems();
  };

  const updateTodoItemHandler = async (todoItem) => {
    setIsEditing(false);
    await todoListCommandsService.updateTodoItem(todoItem);
    getPrintItems();
  };

  const openNewFormHandler = () => {
    setIsEditing(true);
    setTodoItemInput({
      id: "",
      title: "",
      description: "",
      category: "",
    });
  };

  const openUpdateFormHandler = (todoItem) => {
    setIsEditing(true);
    setTodoItemInput({
      ...todoItem,
    });
  };

  const stopEditingHandler = () => {
    setIsEditing(false);
  };

  

  const registerProgressionHandler = async (registerProgression) => {
    setIsEditingRegisterProgression(false);
    await todoListCommandsService.registerProgression(registerProgression);
    getPrintItems();
  };

  const openRegisterProgressionFormHandler = () => {
    setIsEditingRegisterProgression(true);
    setRegisterProgressionInput({
      todoItemId: "",
      date: "",
      percent: "",
    });
  };

  const stopEditingRegisterProgressionHandler = () => {
    setIsEditingRegisterProgression(false);
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
    <PageContainer>
      <h1>Manage To Do Lists</h1>
      <SaveTodoItem
        isEditing={isEditing}
        todoItemInput={todoItemInput}
        todoItems={todoItems}
        onCloseEditForm={stopEditingHandler}
        onOpenNewForm={openNewFormHandler}
        onCreateTodoItem={createTodoItemHandler}
        onUpdateTodoItem={updateTodoItemHandler}
      />
      <RegisterProgressionTodoItem
        isEditingRegisterProgression={isEditingRegisterProgression}
        todoItems={todoItems}
        onCloseEditForm={stopEditingRegisterProgressionHandler}
        onOpenNewForm={openRegisterProgressionFormHandler}
        onRegisterProgression={registerProgressionHandler}
      />
      <Card className={classes.todoItems}>
        <TodoItemsList
          todoItems={todoItems}
          onOpenEditForm={openUpdateFormHandler}
        />
      </Card>
    </PageContainer>
    </>
  );
}

export default TodoItems;