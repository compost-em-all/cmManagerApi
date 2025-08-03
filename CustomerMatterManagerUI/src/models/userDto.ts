export interface UserSignUpDTO {
    email: string;
    firstName: string;
    lastName: string;
    password: string;
    firmName: string;
}

export interface UserDetailDTO {
    emailAddr: string;
    firstName: string;
    lastName: string;
    firmName: string;
}

export interface UserLoginDto {
    email: string;
    password: string;
}