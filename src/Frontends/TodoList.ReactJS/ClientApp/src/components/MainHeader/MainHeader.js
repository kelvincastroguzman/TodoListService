import React from 'react';

import Navigation from './Navigation';
import classes from './MainHeader.module.css';

const MainHeader = () => {
  return (
    <header className={classes['main-header']}>
      <h2>TodoList App (ReactJS)</h2>
      <Navigation />
    </header>
  );
};

export default MainHeader;