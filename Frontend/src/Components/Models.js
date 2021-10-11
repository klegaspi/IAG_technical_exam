import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import ModelItem from './ModelItem';

export default function Models({ models }) {
  return (
    <Paper>
      <Table aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell>Years Available</TableCell>
          </TableRow>
        </TableHead>
        <TableBody data-testid="tableBody">
          {models.map((model, index) => (
            <ModelItem data-testid="modelItem" model={model} key={index} />
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}
