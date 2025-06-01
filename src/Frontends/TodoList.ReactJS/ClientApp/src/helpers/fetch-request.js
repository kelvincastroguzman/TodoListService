import { configConstants } from "../constants";

const fetchRequest = async (url, method, entity) => {
  tokenValidation(localStorage.getItem("isLoggedIn"));

  const requestUrl = `${configConstants.url.API_URL}${url}`;
  const requestOptions =
    entity !== null ? buildRequestBody(entity, method) : buildRequest(method);

  const response = await fetch(requestUrl, requestOptions);

  if (response.ok) {
    const jsonResponse =
      method === "PUT" ? await response.text() : await response.json();
    return jsonResponse;
  } else if (
    response.ok === false &&
    response.status === 401 &&
    response.statusText === "Unauthorized"
  ) {
    localStorage.removeItem("isLoggedIn");
    window.location.href = "/login";
  } else {
    return null;
  }
};

const buildRequestBody = (entity, method) => {
  const requestOptions = {
    method: method,
    headers: {
      "Content-Type": "application/json; charset=utf-8",
      "Access-Control-Allow-Origin": "*",
      Authorization: "Bearer " + localStorage.getItem("isLoggedIn"),
    },
    body: JSON.stringify(entity),
  };
  return requestOptions;
};

const buildRequest = (method) => {
  const requestOptions = {
    method: method,
    headers: {
      "Content-Type": "application/json; charset=utf-8",
      "Access-Control-Allow-Origin": "*",
      Authorization: "Bearer " + localStorage.getItem("isLoggedIn"),
    },
  };
  return requestOptions;
};

const tokenValidation = (token) => {
  if (token !== null && token !== "" && token !== "undefined") {
    return true;
  } else {
    window.location.href = "/login";
  }
};

export default fetchRequest;
