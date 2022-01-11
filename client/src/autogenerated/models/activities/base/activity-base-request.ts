import { BaseApiModel } from '../../common/base-api-model';

export abstract class ActivityBaseRequest extends BaseApiModel {
    title: string;
    date: string;
    description: string;
    category: string;
    city: string;
    venue: string;
}
