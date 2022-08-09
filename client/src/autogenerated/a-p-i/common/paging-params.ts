import { BaseApiModel } from './base-api-model';

export class PagingParams extends BaseApiModel {
    pageSize: number;
    pageNumber: number;
}
