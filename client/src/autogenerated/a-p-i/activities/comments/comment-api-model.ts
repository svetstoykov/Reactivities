import { BaseApiModel } from '../../common/base-api-model';

export class CommentApiModel extends BaseApiModel {
    id!: number;
    content!: string;
    createdAt!: Date;
    username!: string;
    displayName!: string;
    profilePictureUrl!: string;
}
