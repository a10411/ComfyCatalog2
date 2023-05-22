let token = null;

export function setToken(newToken) {
  localStorage.setItem('token', newToken);;
}

export function getToken() {
  return token;
}