import { BaseApiModel } from '../../common/base-api-model';

export class ProfileApiModel extends BaseApiModel {
    email: string;
    bio: string;
    displayName: string;
    username: string;
    profilePictureUrl: string;
}
