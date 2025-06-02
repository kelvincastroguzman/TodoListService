import fetchRequest from "../helpers/fetch-request";

export const todoListQueriesService = {
  getNextTodoItemId,
  getAllCategories,
  getPrintItems,
};

async function getNextTodoItemId() {
  return await fetchRequest("/TodoListQueries/GetNextTodoItemId", "GET", null);
}

async function getAllCategories() {
  return await fetchRequest("/TodoListQueries/GetAllCategories", "GET", null);
}

async function getPrintItems() {
  return await fetchRequest("/TodoListQueries/GetPrintItems", "GET", null);
}
