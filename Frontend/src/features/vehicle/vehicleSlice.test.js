import vehicleReducer, { searchVehicleModel } from './vehicleSlice';

describe('Vehicle Slice reducer', () => {
  const initialState = {
    models: [],
    status: '',
    filter: '',
  };

  it('should set filter', () => {
    const actual = vehicleReducer(initialState, searchVehicleModel('hello'));
    expect(actual.filter).toEqual('hello');
  });
});
