import React, { useContext } from "react";
import { Routes, Route } from 'react-router-dom';

import Login from "./components/Login/Login";
import Home from "./components/Home/Home";
import TodoItems from "./components/TodoLists/TodoItems";

import MainHeader from "./components/MainHeader/MainHeader";
import AuthContext from "./store/auth-context";

function App() {
  const authContext = useContext(AuthContext);

  return (
    <React.Fragment>
      <MainHeader />
      <main>
        <Routes>
          {!authContext.isLoggedIn && <Route path='/login' element={<Login/>} />}
          {authContext.isLoggedIn && <Route exact path='/' element={<Home/>} />}
          {authContext.isLoggedIn && <Route exact path='/todoLists' element={<TodoItems/>} />}
        </Routes>
      </main>
    </React.Fragment>
  );
}

export default App;
