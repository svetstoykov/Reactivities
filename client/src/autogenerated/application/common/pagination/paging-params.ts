import { BaseAppModel } from '../base-app-model';

export class PagingParams extends BaseAppModel {
    pageSize?: number;
    pageNumber?: number;
}
