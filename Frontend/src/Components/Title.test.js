import { render } from '@testing-library/react';
import Title from './Title';

describe('Header Component', () => {
  it('Title Component is rendered', () => {
    const { getByTestId } = render(<Title text="some title" />);
    const title = getByTestId('title');
    expect(title).toBeTruthy();
  });

  it('Check title text', () => {
    const { getByTestId } = render(<Title text="some title" />);
    expect(getByTestId('title')).toHaveTextContent('some title');
  });
});
