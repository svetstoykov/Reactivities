import { BaseAppModel } from '../base-app-model';

export class Pagination extends BaseAppModel {
    pageSize: number;
    pageNumber: number;
    totalCount: number;
    totalPages: number;
}
