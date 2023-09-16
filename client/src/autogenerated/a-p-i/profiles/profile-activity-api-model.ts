import { BaseApiModel } from '../common/base-api-model';

export class ProfileActivityApiModel extends BaseApiModel {
    id!: number;
    title!: string;
    date!: Date;
    category!: string;
}
