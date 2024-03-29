import { BaseApiModel } from '../common/base-api-model';

export class ProfileApiModel extends BaseApiModel {
    email!: string;
    bio!: string;
    displayName!: string;
    followersCount!: number;
    followingsCount!: number;
    following!: boolean;
    username!: string;
    pictureUrl!: string;
}
