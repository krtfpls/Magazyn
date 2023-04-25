export interface User {
    username: string,
    token: string;
}

export interface Email {
    email: string,
    token: string
}

export interface userProfile {
    userName: string,
    email: string,
    firstName: string,
    lastName: string
}

export interface passwordChangeModel {
    oldPassword: string,
    password: string,
}

export interface passwordResetModel {
    password: string,
    email: string,
    token: string
}