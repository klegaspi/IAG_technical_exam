import React from 'react';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';

function ModelItem(props) {
  const { name, yearsAvailable } = props.model;
  return (
    <TableRow>
      <TableCell data-testid="name" component="th" scope="row">
        {name}
      </TableCell>
      <TableCell data-testid="yearsAvailable">{yearsAvailable}</TableCell>
    </TableRow>
  );
}

export default ModelItem;
