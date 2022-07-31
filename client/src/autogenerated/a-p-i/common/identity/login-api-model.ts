import { BaseApiModel } from '../base-api-model';

export class LoginApiModel extends BaseApiModel {
    email: string;
    password: string;
}
