let token = null;
let UserID = null;
export function setToken(newToken) {
  localStorage.setItem('token', newToken);
}

export function getToken() {
  const token = localStorage.getItem('token');
  return token;
}

export function setUserID(newUserID){
  localStorage.setItem('UserID', newUserID);
}

export function getUserID(){
  const UserID = localStorage.getItem('UserID');
  return UserID;
}