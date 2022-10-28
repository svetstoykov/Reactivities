import { BaseAppModel } from '../base/base-app-model';

export class PagingParams extends BaseAppModel {
    pageSize?: number;
    pageNumber?: number;
}
