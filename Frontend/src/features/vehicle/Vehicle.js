import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchVehicle, searchVehicleModel } from './vehicleSlice';
import Header from '../../Components/Header';
import Title from '../../Components/Title';
import Models from '../../Components/Models';
import SearchBar from '../../Components/SearchBar';
import searchModels from './searchModels';
import LoadingSpinner from '../../Components/LoadingSpinner';
import Container from '@material-ui/core/Container';
import Alert from '@material-ui/lab/Alert';

function Vehicle() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchVehicle());
  }, [dispatch]);

  const onSearchChangeHandler = (e) => {
    e.preventDefault();
    dispatch(searchVehicleModel(e.target.value));
  };

  const state = useSelector((state) => state.vehicleReducer);

  const models = searchModels(state);

  return (
    <Container>
      <Header text={'Vehicle List'} data-testid="header1" />
      <Title text={'Lotus'} />
      <SearchBar
        onSearchChangeHandler={onSearchChangeHandler}
        filter={state.filter}
      />

      {state.status === 'loading' ? (
        <LoadingSpinner />
      ) : (
        <Models models={models} />
      )}

      {state.status === 'failed' && (
        <Alert data-testid="alert" severity="error">
          Something went wrong
        </Alert>
      )}
    </Container>
  );
}

export default Vehicle;
