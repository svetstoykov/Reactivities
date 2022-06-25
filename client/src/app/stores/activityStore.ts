import { makeAutoObservable } from "mobx";
import { ActivityViewModel } from "../../autogenerated/a-p-i/models/activities/activity-view-model";
import agent from "../api/agent";

export default class ActivityStore {
    activities: ActivityViewModel[] = [];
    selectedActivity: ActivityViewModel | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadActivities = async () => {
        this.setLoadingInitial(true);
        try {
            const activities = await agent.Activities.list();

            activities.forEach((a) => {
                a.date = a.date.split("T")[0];
                this.activities.push(a);
            });

            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    };

    setLoadingInitial = (state: boolean) =>{
        this.loadingInitial = state;
    }

    selectActivity = (id: number) => {
        this.selectedActivity = this.activities.find((a) => a.id === id);
    }

    cancelSelectedActivity = () => {
        this.selectedActivity = undefined;
    }

    openForm = (id?: number) => {
        id ? this.selectActivity(id) : this.cancelSelectedActivity();
        this.editMode = true;
    }

    closeForm = () => {
        this.editMode = false;
    }

    createActivity = (model: ActivityViewModel) => {

    }
}
