import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";
import { ActivityApiModel } from "../../autogenerated/a-p-i/activities/models/activity-api-model";
import { CategoryApiModel } from "../../autogenerated/a-p-i/activities/models/category-api-model";
import { LoginApiModel } from "../../autogenerated/a-p-i/common/identity/models/login-api-model";
import { RegisterApiModel } from "../../autogenerated/a-p-i/common/identity/models/register-api-model";
import { UserApiModel } from "../../autogenerated/a-p-i/common/identity/models/user-api-model";
import { store } from "../stores/store";


const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    });
};

axios.defaults.baseURL = "http://localhost:51004/api";

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if(token){
        config.headers!.Authorization = `Bearer ${token}`;
    }

    return config;
})

// Added to assist in displaying loading screens.
axios.interceptors.response.use(
    async (response) => {
        await sleep(1000);
        return response;
    },
    (error: AxiosError) => {
        const { data, status } = error.response!;

        showToasterErrorNotification(status, data);

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
    list: () => requests.get<ActivityApiModel[]>("/activities"),
    details: (id: number) => requests.get<ActivityApiModel>(`/activities/${id}`),
    create: (activity: ActivityApiModel) => requests.post<number>("/activities", activity),
    update: (activity: ActivityApiModel) => requests.put(`/activities/`, activity),
    delete: (id: number) => requests.delete(`/activities/${id}`),
    categories: () => requests.get<CategoryApiModel[]>(`/activities/categories`),
    updateAttendance: (id:number) => requests.post(`/activities/updateAttendance/${id}`, {}),
    updateStatus: (id: number) => requests.post(`/activities/updateStatus/${id}`, {})
};

const Accounts = {
    currentUser: () => requests.get<UserApiModel>("/accounts"),
    login: (creds: LoginApiModel) => requests.post<UserApiModel>("/accounts/login", creds),
    register: (details: RegisterApiModel) => requests.post<UserApiModel>("/accounts/register", details)
}


const showToasterErrorNotification = (status: number, data: any) => {
    switch (status) {
        case 400:
            if (data.errors) {
                const modelStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modelStateErrors.push(data.errors[key]);
                    }
                }

                toast.error(modelStateErrors.join(", "));
            } else {
                toast.error(data.message || data);
            }
            break;
        case 401:
            toast.error(data || "Unauthorized");
            break;
        case 404:
            history.push("/not-found");
            break;
        case 500:
            store.commonStore.setServerError(data);
            history.push("/server-error");
            break;
        default:
            toast.warning("Unable to perform action.")
    }
}

const agent = {
    Activities,
    Accounts
};

export default agent;
