import { BaseApiModel } from '../../common/base-api-model';

export class CommentApiModel extends BaseApiModel {
    content: string;
    createdAt = new Date();
    username: string;
    displayName: string;
    profilePictureUrl: string;
}
