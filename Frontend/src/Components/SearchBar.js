import React from 'react';
import TextField from '@material-ui/core/TextField';

function SearchBar({ onSearchChangeHandler, filter }) {
  return (
    <TextField
      onChange={onSearchChangeHandler}
      id="filled-search"
      type="search"
      variant="filled"
      fullWidth
      autoFocus
      value={filter}
      placeholder="search model"
    />
  );
}

export default SearchBar;
