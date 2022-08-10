import { BaseApiModel } from '../common/base-api-model';
import { ProfileApiModel } from '../profiles/profile-api-model';

export class ActivityApiModel extends BaseApiModel {
    id?: number;
    title: string;
    date: Date;
    description: string;
    categoryId: number;
    category: string;
    city: string;
    venue: string;
    host: ProfileApiModel;
    isCancelled: boolean;
    attendees: Array<ProfileApiModel>;
}
