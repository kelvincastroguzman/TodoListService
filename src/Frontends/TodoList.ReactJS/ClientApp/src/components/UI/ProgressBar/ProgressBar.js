import React from "react";

import Filler from "./Filler";
import classes from "./ProgressBar.module.css";

const ProgressBar = (props) => {
    return (
        <div className={classes["progress-bar"]}>
            <Filler percentage={props.percentage} />
        </div>
    );
};

export default ProgressBar;