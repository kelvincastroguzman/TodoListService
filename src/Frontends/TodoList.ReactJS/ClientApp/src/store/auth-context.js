import React, { useState, useEffect } from "react";

import { authenticationService } from "../services";
import ErrorModal from "../components/UI/ErrorModal/ErrorModal";

const AuthContext = React.createContext({
  isLoggedIn: false,
  onLogout: () => {},
  onLogin: (email, password) => {},
});

export const AuthContextProvider = (props) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [error, setError] = useState();

  useEffect(() => {
    const storedUserLoggedInInfo = localStorage.getItem("isLoggedIn");
    if (
      storedUserLoggedInInfo !== null &&
      storedUserLoggedInInfo !== "" &&
      storedUserLoggedInInfo !== "undefined"
    ) {
      setIsLoggedIn(true);
    }
  }, []);

  const closeErrorModalHandler = () => {
    setError(null);
  };

  const logoutHandler = () => {
    localStorage.removeItem("isLoggedIn");
    setIsLoggedIn(false);
    window.location.href = "/login";
  };

  const loginHandler = async (email, password) => {
    const responseToken = await authenticationService.login(email, password);

    if (responseToken != null) {
      localStorage.setItem("isLoggedIn", responseToken);
      setIsLoggedIn(true);
      window.location.href = "/";
    } else {
      setError({
        title: "Invalid user authentication",
        message: "Please, verify your credentials to gain access.",
      });
    }
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
      <AuthContext.Provider
        value={{
          isLoggedIn: isLoggedIn,
          onLogout: logoutHandler,
          onLogin: loginHandler,
        }}
      >
        {props.children}
      </AuthContext.Provider>
    </>
  );
};

export default AuthContext;
