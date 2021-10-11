import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';

export default function Header({ text }) {
  return (
    <AppBar data-testid="appBar" position="static">
      <Toolbar data-testid="toolBar" variant="dense">
        <Typography data-testid="headerText" variant="h6" color="inherit">
          {text}
        </Typography>
      </Toolbar>
    </AppBar>
  );
}
