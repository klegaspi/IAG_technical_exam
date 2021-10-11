import React from 'react';
import Typography from '@material-ui/core/Typography';

export default function Title({ text }) {
  return (
    <Typography data-testid="title" variant="h5" component="h2">
      {text}
    </Typography>
  );
}
