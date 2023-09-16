import { BaseApiModel } from '../common/base-api-model';

export class LoginApiModel extends BaseApiModel {
    email!: string;
    password!: string;
}
