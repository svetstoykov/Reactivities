import { makeAutoObservable, runInAction } from "mobx";
import { ActivityApiModel } from "../../autogenerated/a-p-i/activities/activity-api-model";
import { CategoryApiModel } from "../../autogenerated/a-p-i/activities/category-api-model";
import agent from "../api/agent";
import { reactivitiesDateFormat } from "../common/constants/GlobalConstants";
import { format } from "date-fns";
import { store } from "./store";
import { ProfileApiModel } from "../../autogenerated/a-p-i/profiles/profile-api-model";

export default class ActivityStore {
    activitiesRegistry = new Map<number, ActivityApiModel>();
    categories = new Array<CategoryApiModel>();
    selectedActivity: ActivityApiModel | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);
    }

    get activitiesByDate() {
        return Array.from(this.activitiesRegistry.values()).sort(
            (a, b) => a.date.valueOf() - b.date.valueOf()
        );
    }

    get groupedActivities() {
        return Object.entries(
            this.activitiesByDate.reduce((activitiesMap, activity) => {
                const date = format(activity.date, reactivitiesDateFormat);

                activitiesMap[date] = activitiesMap[date]
                    ? [...activitiesMap[date], activity]
                    : [activity];

                return activitiesMap;
            }, {} as { [key: string]: ActivityApiModel[] })
        );
    }

    loadActivities = async () => {
        this.setLoadingInitial(true);
        try {
            const activities = await agent.Activities.list();
            activities.forEach((activity) => {
                this.setActivity(activity);
            });
        } catch (error) {
            console.log(error);
        }

        this.setLoadingInitial(false);
    };

    loadCategories = async () => {
        this.setLoadingInitial(true);
        try {
            const categories = await agent.Activities.categories();
            runInAction(() => {
                this.categories = categories;
            });

            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    };

    loadActivity = async (id: number) => {
        this.setLoadingInitial(true);
        try {
            let activity = this.activitiesRegistry.get(id);
            if (!activity) {
                activity = await agent.Activities.details(id);
                this.setActivity(activity!);
            }

            this.setSelectedActivity(activity);
            this.setLoadingInitial(false);

            return activity;
        } catch (ex) {
            console.log(ex);
            this.setLoadingInitial(false);
        }
    };

    createActivity = async (newActivity: ActivityApiModel) => {
        this.setLoading(true);
        try {
            var id = await agent.Activities.create(newActivity);
            newActivity.id = id;

            runInAction(() => {
                this.activitiesRegistry.set(id, newActivity);
            });

            this.closeLoadingAndSelectActivity(newActivity);
        } catch (ex) {
            this.setLoading(false);
            throw new Error();
        }
    };

    updateActivity = async (activity: ActivityApiModel) => {
        this.setLoading(true);
        try {
            await agent.Activities.update(activity);
            runInAction(() => {
                this.activitiesRegistry.delete(activity.id!);
            });

            this.closeLoadingAndSelectActivity(activity);
        } catch (ex) {
            this.setLoading(false);
            throw new Error();
        }
    };

    deleteActivity = async (id: number) => {
        this.setLoading(true);
        try {
            await agent.Activities.delete(id);
            runInAction(() => {
                this.activitiesRegistry.delete(id);
            });
            this.setLoading(false);
        } catch (ex) {
            this.logException(ex);
        }
    };

    updateAttendance = async () => {
        const profile = store.profileStore.currentProfile;
        const activityId = this.selectedActivity!.id!;
        this.setLoading(true);
        try {
            await agent.Activities.updateAttendance(activityId);
            if (store.profileStore.isGoingToActivity(this.selectedActivity?.id!)) {
                this.setAttendees(
                    this.selectedActivity!.attendees?.filter((a) => a.username !== profile.username)
                );

                return;
            }

            this.addAtendee(profile);
        } catch (error) {
            this.logException(error);
        } finally {
            this.setLoading(false);
        }
    };

    updateStatus = async () => {
        this.setLoading(true);
        try {
            await agent.Activities.updateStatus(this.selectedActivity?.id!);
            runInAction(
                () => (this.selectedActivity!.isCancelled = !this.selectedActivity?.isCancelled)
            );
        } catch (error) {
            this.logException(error);
        } finally {
            this.setLoading(false);
        }
    };

    clearSelectedActivity = () => {
        this.selectedActivity = undefined;
    };

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    };

    updateAttendeeFollowingStatus = (targetUser: string) => {
        this.activitiesRegistry.forEach((activity) => {
            activity.attendees.forEach((attendee) => {
                if (attendee.username === targetUser) {
                    attendee.following = !attendee.following;
                    attendee.following ? attendee.followersCount++ : attendee.followersCount--;
                }
            });
        });
    };

    private addAtendee = (attendee: ProfileApiModel) => {
        this.selectedActivity?.attendees.push(attendee);
    };

    private setAttendees = (attendees: ProfileApiModel[] | undefined) => {
        if (this.selectedActivity) {
            this.selectedActivity.attendees = attendees ?? new Array<ProfileApiModel>();
        }
    };

    private closeLoadingAndSelectActivity = (activity: ActivityApiModel) => {
        this.selectedActivity = activity;
        this.loading = false;
        this.editMode = false;
    };

    private logException = (ex: any) => {
        console.log(ex);
        this.loading = false;
    };

    private setLoading = (state: boolean) => {
        this.loading = state;
    };

    private setActivity(activity: ActivityApiModel) {
        activity.date = new Date(activity.date + "Z");
        this.activitiesRegistry.set(activity.id!, activity);
    }

    private setSelectedActivity(activity: ActivityApiModel) {
        this.selectedActivity = activity;
    }
}
