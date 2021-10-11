import searchModels from './searchModels';

describe('SearchModels', () => {
  let reducer = {
    models: [
      { name: 'Model S', yearsAvailable: 5 },
      { name: 'Model T', yearsAvailable: 4 },
    ],
    filter: '',
  };

  it('filter returns correct models', () => {
    // Arrange
    reducer.filter = 'Model T';
    // Act
    var result = searchModels(reducer);
    //Assert
    expect(result.length).toBe(1);
  });

  it('empty filter returns all models', () => {
    // Arrange
    reducer.filter = '';
    // Act
    var result = searchModels(reducer);
    //Assert
    expect(result.length).toBe(2);
  });

  it('filter has no match', () => {
    // Arrange
    reducer.filter = 'bla';
    // Act
    var result = searchModels(reducer);
    //Assert
    expect(result.length).toBe(0);
  });
});
