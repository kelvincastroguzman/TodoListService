import fetchRequest from "../helpers/fetch-request";

export const todoListCommandsService = {
  createTodoItem,
  updateTodoItem,
  removeTodoItem,
  registerProgression,
};

async function createTodoItem(todoItem) {
  return await fetchRequest("/TodoListCommands/CreateTodoItem", "POST", todoItem);
}

async function updateTodoItem(todoItem) {
  return await fetchRequest("/TodoListCommands/UpdateTodoItem", "PUT", todoItem);
}

async function removeTodoItem(id) {
  return await fetchRequest(`/TodoListCommands/RemoveTodoItem/${id}`, "DELETE");
}

async function registerProgression(registerProgression) {
  return await fetchRequest("/TodoListCommands/RegisterProgression", "POST", registerProgression);
}
