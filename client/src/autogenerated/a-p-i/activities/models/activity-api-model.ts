import { BaseApiModel } from '../../common/base-api-model';
import { ProfileApiModel } from '../../profiles/models/profile-api-model';

export class ActivityApiModel extends BaseApiModel {
    id?: number;
    title: string;
    date = new Date();
    description: string;
    categoryId: number;
    category: string;
    city: string;
    venue: string;
    host = new ProfileApiModel();
    isCancelled: boolean;
    attendees = new Array<ProfileApiModel>();
}
