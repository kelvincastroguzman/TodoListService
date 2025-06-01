import { v4 as uuid } from 'uuid';

export const authenticationService = {
    login
};

async function login(username, password) {
    if (username.length > 0 && password.length > 0) {
        let jsonResponse = uuid();
        return jsonResponse;
    } else {
        return null;
    }
}
