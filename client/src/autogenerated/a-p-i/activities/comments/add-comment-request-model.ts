import { BaseApiModel } from '../../common/base-api-model';

export class AddCommentRequestModel extends BaseApiModel {
    content: string;
    activityId: number;
    username: string;
}
