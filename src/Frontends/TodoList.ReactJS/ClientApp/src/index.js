import React from "react";
import {StrictMode} from 'react';
import {createRoot} from 'react-dom/client';
import { BrowserRouter } from "react-router-dom";

import 'bootstrap/dist/css/bootstrap.css';
import "./index.css";
import App from "./App";

import { AuthContextProvider } from "./store/auth-context";
import { configureStore } from '@reduxjs/toolkit';
import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

const reducers = { 
};

const rootReducer = combineReducers({
  ...reducers,
  routing: routerReducer
});

const store = configureStore({ 
  reducer: {
    rootReducer,
  }
});

root.render(
  <StrictMode>
    <BrowserRouter basename={baseUrl}>
      <AuthContextProvider store={store}>
        <App />
      </AuthContextProvider>
    </BrowserRouter>
  </StrictMode>,
);
