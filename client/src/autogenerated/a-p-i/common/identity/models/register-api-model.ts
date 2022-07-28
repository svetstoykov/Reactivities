import { BaseApiModel } from '../../base-api-model';

export class RegisterApiModel extends BaseApiModel {
    displayName: string;
    username: string;
    password: string;
    email: string;
}
