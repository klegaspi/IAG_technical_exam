import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import fetchVehicleAsync from './vehicleAPI';

export const fetchVehicle = createAsyncThunk(
  'vehicle/fetchVehicle',
  fetchVehicleAsync,
);

const vehicleSlice = createSlice({
  name: 'vehicle',
  initialState: {
    models: [],
    status: '',
    filter: '',
  },

  reducers: {
    searchVehicleModel: (state, action) => {
      state.filter = action.payload;
    },
  },
  extraReducers: {
    [fetchVehicle.pending]: (state, action) => {
      state.status = 'loading';
      state.error = '';
    },
    [fetchVehicle.fulfilled]: (state, { payload }) => {
      state.models = payload;
      state.status = 'success';
      state.error = '';
    },
    [fetchVehicle.rejected]: (state, action) => {
      state.status = 'failed';
    },
  },
});

export const { searchVehicleModel } = vehicleSlice.actions;

export default vehicleSlice.reducer;
