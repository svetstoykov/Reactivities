import { makeAutoObservable, reaction, runInAction } from "mobx";
import { ProfileApiModel } from "../../autogenerated/a-p-i/profiles/profile-api-model";
import agent from "../api/agent";
import { store } from "./store";

export default class ProfileStore {
    private profile: ProfileApiModel | null = null;
    selectedProfile: ProfileApiModel;
    followings: ProfileApiModel[];
    loadingUser = false;
    loading = false;
    loadingFollowings = false;
    editDetailsMode = false;
    activeTab = 0;

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.activeTab,
            (activeTab) => {
                if (activeTab === 2 || activeTab === 3) {
                    const returnFollowersInsteadOfFollowings = activeTab === 2;
                    this.loadFollowings(returnFollowersInsteadOfFollowings);
                } else {
                    this.followings = [];
                }
            }
        );
    }

    get currentProfile() {
        return this.profile!;
    }

    get isLoggedIn() {
        return !!this.profile;
    }

    get isCurrentProfile() {
        if (this.profile && this.selectedProfile) {
            return this.profile.username === this.selectedProfile.username;
        }

        return false;
    }

    setActiveTab = (activeTab: number) => {
        this.activeTab = activeTab;
    };

    loadProfile = async (username: string) => {
        this.loadingUser = true;
        try {
            const profile = await agent.Profiles.get(username);

            this.setSelectedProfile(profile);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingUser(false);
        }
    };

    loadCurrentProfile = async () => {
        this.loadingUser = true;
        try {
            const profile = await agent.Profiles.getCurrent();

            this.setCurrentProfile(profile);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingUser(false);
        }
    };

    uploadProfilePicture = async (file: Blob) => {
        this.setLoading(true);
        try {
            const response = await agent.Profiles.uploadPhoto(file);
            const pictureUrl = response.data;

            this.setProfilePicture(pictureUrl);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoading(false);
        }
    };

    deleteProfilePicture = async () => {
        this.setLoading(true);
        try {
            await agent.Profiles.deletePhoto();

            this.setProfilePicture("");
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoading(false);
        }
    };

    updateDetails = async (profile: ProfileApiModel) => {
        this.setLoading(true);
        try {
            await agent.Profiles.updateDetails(profile);

            this.setSelectedProfile(profile);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoading(false);
        }
    };

    updateFollowing = async (targetUser: string) => {
        this.setLoading(true);
        try {
            const isNowFollowed = await agent.Profiles.updateFollowing(targetUser);

            store.activityStore.updateAttendeeFollowingStatus(targetUser);
            runInAction(() => {
                if (
                    this.selectedProfile.username !== this.currentProfile.username &&
                    this.selectedProfile.username === targetUser
                ) {
                    isNowFollowed
                        ? this.selectedProfile.followersCount++
                        : this.selectedProfile.followersCount--;
                    this.selectedProfile.following = !this.selectedProfile.following;
                }
                if (this.selectedProfile.username === this.currentProfile.username) {
                    isNowFollowed
                        ? this.selectedProfile.followingsCount++
                        : this.selectedProfile.followingsCount--;
                }
                this.followings.forEach((profile) => {
                    if (profile.username === targetUser) {
                        profile.following ? profile.followersCount-- : profile.followersCount++;
                        profile.following = !profile.following;
                    }
                });
            });
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoading(false);
        }
    };

    loadFollowings = async (returnFollowersInsteadOfFollowings: boolean) => {
        this.setLoadingFollowings(true);
        try {
            const followings = await agent.Profiles.getFollowings(
                this.selectedProfile.username,
                returnFollowersInsteadOfFollowings
            );

            this.setFollowings(followings);
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingFollowings(false);
        }
    };

    setEditDetailsMode = (state: boolean) => {
        this.editDetailsMode = state;
    };

    isActivityHost = (activityHostUsername: string | undefined) => {
        return this.profile?.username === activityHostUsername;
    };

    isGoingToActivity = (activityId: number) => {
        return store.activityStore.activitiesRegistry
            .get(activityId)
            ?.attendees.some((a) => a.username === this.profile?.username);
    };

    setCurrentProfile = (profile: ProfileApiModel | null) => {
        this.profile = profile;
    };

    private setFollowings = (followings: ProfileApiModel[]) => {
        this.followings = followings;
    };

    private setSelectedProfile = (selectedProfile: ProfileApiModel) => {
        this.selectedProfile = selectedProfile;
    };

    private setProfilePicture = (pictureUrl: string) => {
        this.currentProfile.pictureUrl = pictureUrl;
    };

    private setLoading = (state: boolean) => {
        this.loading = state;
    };

    private setLoadingUser = (state: boolean) => {
        this.loadingUser = state;
    };

    private setLoadingFollowings = (state: boolean) => {
        this.loadingFollowings = state;
    };
}
