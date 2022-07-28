import { makeAutoObservable } from "mobx";
import { history } from "../..";
import { LoginApiModel } from "../../autogenerated/a-p-i/common/identity/models/login-api-model";
import { RegisterApiModel } from "../../autogenerated/a-p-i/common/identity/models/register-api-model";
import { UserApiModel } from "../../autogenerated/a-p-i/common/identity/models/user-api-model";
import agent from "../api/agent";
import { store } from "./store";

export default class UserStore {
    user: UserApiModel | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get IsLoggedIn() {
        return !!this.user;
    }

    login = async (creds: LoginApiModel) => {
        try {
            const user = await agent.Accounts.login(creds);
            
            this.setUserAndCloseModal(user);
        } catch (error) {
            console.log(error);
        }
    };

    register = async (details: RegisterApiModel) => {
        try {
            const user = await agent.Accounts.register(details);

            this.setUserAndCloseModal(user);
        } catch (error) {
            console.log(error);
        }
    };

    setUser = (user: UserApiModel | null) => {
        this.user = user;
    };

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem("userToken");
        this.setUser(null);
        history.push("/");
    };

    getUser = async () => {
        try {
            const user = await agent.Accounts.currentUser();
            this.setUser(user);
        } catch (error) {
            console.log(error);
        }
    };

    
    private setUserAndCloseModal = (user: UserApiModel) => {
        if (user) {
            store.commonStore.setToken(user.token);
            this.setUser(user);
        }

        history.push("/activities");
        store.modalStore.closeModal();
    };
}