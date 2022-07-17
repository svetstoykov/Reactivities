import { BaseApiModel } from '../../models/base-api-model';

export abstract class CreateEditActivityBaseRequestModel extends BaseApiModel {
    title: string;
    date: string;
    description: string;
    category: string;
    city: string;
    venue: string;
}
