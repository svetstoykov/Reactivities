import { BaseApiModel } from '../common/base-api-model';

export class ProfileApiModel extends BaseApiModel {
    email: string;
    bio: string;
    displayName: string;
    followers: number;
    followings: number;
    isFollowing: boolean;
    username: string;
    pictureUrl: string;
}
