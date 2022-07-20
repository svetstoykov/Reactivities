import { BaseApiModel } from '../../common/base-api-model';

export class ActivityViewModel extends BaseApiModel {
    id?: number;
    title: string;
    date = new Date();
    description: string;
    categoryId: number;
    category: string;
    city: string;
    venue: string;
}
