import { render } from '@testing-library/react';
import Header from './Header';

describe('Header Component', () => {
  it('Header Component is rendered', () => {
    const { getByTestId } = render(<Header text="some header" />);
    const headerText = getByTestId('headerText');
    const appBar = getByTestId('appBar');
    const toolBar = getByTestId('toolBar');
    expect(headerText).toBeTruthy();
    expect(appBar).toBeTruthy();
    expect(toolBar).toBeTruthy();
  });

  it('Check header text', () => {
    const { getByTestId } = render(<Header text="some header" />);
    expect(getByTestId('headerText')).toHaveTextContent('some header');
  });
});
