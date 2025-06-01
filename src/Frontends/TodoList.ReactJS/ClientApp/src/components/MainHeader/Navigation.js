import React, { useContext } from "react";
import { Link } from "react-router-dom";

import Button from "../UI/Button/Button";
import AuthContext from "../../store/auth-context";
import classes from "./Navigation.module.css";

const Navigation = () => {
  const authContext = useContext(AuthContext);

  return (
    <nav className={classes.nav}>
      <ul>
        {authContext.isLoggedIn && (
          <li>
            <Button tag={Link} to="/">
              Home
            </Button>
          </li>
        )}
        {authContext.isLoggedIn && (
          <li>
            <Button tag={Link} to="/todoLists">
              To Do Lists
            </Button>
          </li>
        )}
        {authContext.isLoggedIn && (
          <li>
            <Button tag={Link} onClick={authContext.onLogout} to="/login">
              Logout
            </Button>
          </li>
        )}
      </ul>
    </nav>
  );
};

export default Navigation;
