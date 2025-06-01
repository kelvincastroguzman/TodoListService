import fetchRequest from "../helpers/fetch-request";

export const todoListQueriesService = {
  getPrintItems
};

async function getPrintItems() {
  return await fetchRequest("/TodoListQueries/GetPrintItems", "GET", null);
}
