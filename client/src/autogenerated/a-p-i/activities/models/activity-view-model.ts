import { BaseApiModel } from '../models/base-api-model';

export class ActivityViewModel extends BaseApiModel {
    id?: number;
    title: string;
    date: string;
    description: string;
    category: string;
    city: string;
    venue: string;
}
