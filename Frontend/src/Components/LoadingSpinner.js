import React from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import Box from '@material-ui/core/Box';

function LoadingSpinner() {
  return (
    <Box display="flex" justifyContent="center" alignContent="center">
      <CircularProgress data-testid="loadingSpinner" />
    </Box>
  );
}

export default LoadingSpinner;
