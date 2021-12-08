import { BaseApiModel } from '../../common/base-api-model';

export abstract class ActivityBaseRequest extends BaseApiModel {
    title: string;
    date = new Date();
    description: string;
    category: string;
    city: string;
    venue: string;
}
