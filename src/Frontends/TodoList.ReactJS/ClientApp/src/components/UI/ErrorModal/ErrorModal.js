import React from "react";
import ReactDom from "react-dom";
import Card from "../Card/Card";
import Button from "../Button/Button";
import styles from "./ErrorModal.module.css";

const Backdrop = (props) => {
  return <div className={styles.backdrop} onClick={props.onCloseErrorModal} />;
};

const ModalOverlay = (props) => {
  return (
    <Card className={styles.modal}>
      <header className={styles.header}>
        <h4>{props.title}</h4>
      </header>
      <div className={styles.content}>
        <p>{props.message}</p>
      </div>
      <footer className={styles.actions}>
        <Button onClick={props.onCloseErrorModal}>Okey</Button>
      </footer>
    </Card>
  );
};

const ErrorModal = (props) => {
  return (
    <>
      {ReactDom.createPortal(
        <Backdrop onCloseErrorModal={props.onCloseErrorModal} />,
        document.getElementById("backdrop-root")
      )}
      {ReactDom.createPortal(
        <ModalOverlay
          title={props.title}
          message={props.message}
          onCloseErrorModal={props.onCloseErrorModal}
        />,
        document.getElementById("overlay-root")
      )}
    </>
  );
};

export default ErrorModal;
