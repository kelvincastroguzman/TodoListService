import React from "react";

import Card from "../Card/Card";
import classes from "./PageContainer.module.css";

const PageContainer = (props) => {
  return (
    <Card className={classes.pageContainer}>
      {props.children}
    </Card>
  );
};

export default PageContainer;