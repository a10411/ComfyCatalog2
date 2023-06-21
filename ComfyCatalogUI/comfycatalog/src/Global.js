let token = null;
let UserID = null;
let AdminID = null;

export function setToken(newToken) {
  localStorage.setItem('token', newToken);
}

export function getToken() {
  const token = localStorage.getItem('token');
  return token;
}

export function setUserID(newUserID) {
  localStorage.setItem('UserID', newUserID);
}

export function getUserID() {
  const userID = localStorage.getItem('UserID');
  return userID;
}

export function setAdminID(newAdminID) {
  localStorage.setItem('AdminID', newAdminID);
}

export function getAdminID() {
  const adminID = localStorage.getItem('AdminID');
  return adminID;
}
