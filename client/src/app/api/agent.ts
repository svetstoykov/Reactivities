import axios, { AxiosResponse } from "axios";
import { CreateActivityRequestModel } from "../../autogenerated/a-p-i/models/activities/request/create-activity-request-model";
import { EditActivityRequestModel } from "../../autogenerated/a-p-i/models/activities/request/edit-activity-request-model";
import { ActivityViewModel } from "../../autogenerated/a-p-i/models/activities/response/activity-view-model";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = "http://localhost:51004/api";

// Added to assist in displaying loading screens.
axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (ex) {
        console.log(ex);
        return Promise.reject(ex)
    }
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Activities = {
    list: () => requests.get<ActivityViewModel[]>("/activities"),
    details: (id: number) => requests.get<ActivityViewModel>(`/activities/${id}`),
    create: (activity: CreateActivityRequestModel) => requests.post<number>('/activities', activity),
    update: (activity: EditActivityRequestModel) => requests.put(`/activities/`, activity),
    delete: (id: number) => requests.delete(`/activities/${id}`)
}

const agent = {
    Activities
}

export default agent;