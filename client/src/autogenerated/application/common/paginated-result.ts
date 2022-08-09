import { Pagination } from './pagination';

export class PaginatedResult<TData> {
    data = new Array<TData>();
    pagination = new Pagination();
}
