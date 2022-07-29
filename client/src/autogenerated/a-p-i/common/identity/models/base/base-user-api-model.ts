import { BaseApiModel } from '../../../base-api-model';

export abstract class BaseUserApiModel extends BaseApiModel {
    displayName: string;
    username: string;
    image: string;
}
