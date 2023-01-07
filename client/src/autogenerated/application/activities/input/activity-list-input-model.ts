import { PagingParams } from '../../common/pagination/paging-params';
import { ActivityAttendingFilterType } from '../enums/activity-attending-filter-type.enum';

export class ActivityListInputModel extends PagingParams {
    attending!: ActivityAttendingFilterType;
    startDate?: Date;
}
