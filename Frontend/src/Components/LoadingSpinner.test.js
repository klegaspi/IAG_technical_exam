import { render } from '@testing-library/react';
import LoadingSpinner from './LoadingSpinner';

describe('Loading Spinner Component', () => {
  it('Loading Spinner is rendered', () => {
    const { getByTestId } = render(<LoadingSpinner text="hello world" />);
    const loadingSpinner = getByTestId('loadingSpinner');
    expect(loadingSpinner).toBeTruthy();
  });
});
