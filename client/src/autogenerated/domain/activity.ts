import { DomainModel } from './base/domain-model';

export class Activity extends DomainModel {
    title: string;
    date = new Date();
    description: string;
    category: string;
    city: string;
    venue: string;
}
