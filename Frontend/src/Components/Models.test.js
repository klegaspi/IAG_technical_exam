import { render } from '@testing-library/react';
import Models from './Models';

describe('Models Component', () => {
  it('Check Model Items', () => {
    const { getByTestId } = render(
      <Models
        models={[
          { name: 'Model X', yearsAvailable: 5 },
          { name: 'Model S', yearsAvailable: 5 },
        ]}
      />,
    );
    expect(getByTestId('tableBody')).toHaveTextContent('Model X');
    expect(getByTestId('tableBody')).toHaveTextContent('Model S');
  });
});
