import { BaseApiModel } from '../../common/base-api-model';

export class AddCommentApiModel extends BaseApiModel {
    content!: string;
    activityId!: number;
    username!: string;
}
