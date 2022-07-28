import { BaseApiModel } from '../../base-api-model';

export class UserApiModel extends BaseApiModel {
    displayName: string;
    token: string;
    username: string;
    image: string;
}
