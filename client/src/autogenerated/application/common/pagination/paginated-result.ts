import { BaseAppModel } from '../base-app-model';
import { Pagination } from './pagination';

export class PaginatedResult<TData> extends BaseAppModel {
    data: Array<TData>;
    pagination: Pagination;
}
