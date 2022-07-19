import { BaseApiModel } from '../../base-api-model';

export class ExceptionResponseModel extends BaseApiModel {
    statusCode: number;
    message: string;
    details: string;
}
