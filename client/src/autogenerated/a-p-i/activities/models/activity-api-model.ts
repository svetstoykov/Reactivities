import { BaseApiModel } from '../../common/base-api-model';
import { ProfileApiModel } from '../../common/identity/models/profile-api-model';

export class ActivityApiModel extends BaseApiModel {
    id?: number;
    title: string;
    date = new Date();
    description: string;
    categoryId: number;
    category: string;
    city: string;
    venue: string;
    hostName: string;
    attendees = new Array<ProfileApiModel>();
}
