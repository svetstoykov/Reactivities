import { BaseApiModel } from '../../common/base-api-model';

export class ActivityResponse extends BaseApiModel {
    id: number;
    title: string;
    date: string;
    description: string;
    category: string;
    city: string;
    venue: string;
}
