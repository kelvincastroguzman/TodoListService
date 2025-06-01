
export const splitUserMessage = (text) => {
  return text.split('\n').map(str => <li style={{ listStyleType: "none" }} key={Math.random()}>{str}</li>);
};
