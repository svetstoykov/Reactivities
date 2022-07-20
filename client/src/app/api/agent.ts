import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";
import { ActivityViewModel } from "../../autogenerated/a-p-i/activities/models/activity-view-model";
import { CategoryViewModel } from "../../autogenerated/a-p-i/activities/models/category-view-model";
import { store } from "../stores/store";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    });
};

axios.defaults.baseURL = "http://localhost:51004/api";

// Added to assist in displaying loading screens.
axios.interceptors.response.use(
    async (response) => {
        await sleep(1000);
        return response;
    },
    (error: AxiosError) => {
        const { data, status } = error.response!;

        switch (status) {
            case 400:
                if (data.errors) {
                    const modelStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modelStateErrors.push(data.errors[key]);
                        }
                    }

                    throw modelStateErrors;
                } else {
                    toast.error(data);
                }
                break;
            case 401:
                toast.error("Unauthorized");
                break;
            case 404:
                history.push("/not-found");
                break;
            case 500:
                store.commonStore.setServerError(data);
                history.push("/server-error");
                break;
        }

        return Promise.reject(error);
    }
);

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Activities = {
    list: () => requests.get<ActivityViewModel[]>("/activities"),
    details: (id: number) => requests.get<ActivityViewModel>(`/activities/${id}`),
    create: (activity: ActivityViewModel) => requests.post<number>("/activities", activity),
    update: (activity: ActivityViewModel) => requests.put(`/activities/`, activity),
    delete: (id: number) => requests.delete(`/activities/${id}`),
    categories: () => requests.get<CategoryViewModel[]>(`/activities/categories`),
};

const agent = {
    Activities,
};

export default agent;
