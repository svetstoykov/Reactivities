import { CommentApiModel } from "../../autogenerated/a-p-i/activities/comments/comment-api-model";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { makeAutoObservable } from "mobx";
import { store } from "./store";
import { AddCommentRequestModel } from "../../autogenerated/a-p-i/activities/comments/add-comment-request-model";

export default class CommentStore {
    comments: CommentApiModel[] = [];
    hubConnection: HubConnection | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createHubConnection = (activityId: number) => {
        if (store.activityStore.selectedActivity) {
            this.hubConnection = new HubConnectionBuilder()
                .withUrl(`${process.env.REACT_APP_COMMENTS_URL}?activityId=${activityId}`, {
                    accessTokenFactory: () => store.commonStore.token!,
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();

            this.hubConnection
                .start()
                .catch((err) => console.log("SignalR error while starting connection: " + err));

            this.hubConnection.on("LoadComments", (comments: CommentApiModel[]) => {
                this.setComments(comments);
            });

            this.hubConnection.on("ReceiveComment", (comment: CommentApiModel) => {
                this.insertComment(comment);
            });
        }
    };

    clearComments = () => {
        this.comments = [];
        this.stopHubConnection();
    };

    addComment = async (request: AddCommentRequestModel) => {
        request.activityId = store.activityStore.selectedActivity?.id!;
        request.username = store.profileStore.currentProfile.username;
        try {
            await this.hubConnection?.invoke("SendComment", request);
        } catch (error) {
            console.log(error);
        }
    };

    private stopHubConnection = () => {
        this.hubConnection
            ?.stop()
            .catch((err) => console.log("SignalR error while stopping connection: " + err));
    };

    private setComments = (comments: CommentApiModel[]) => {
        comments.forEach((c) => {
            c.createdAt = new Date(c.createdAt + "Z");
        });
        this.comments = comments;
    };

    private insertComment = (comment: CommentApiModel) => {
        comment.createdAt = new Date(comment.createdAt);
        this.comments.unshift(comment);
    };
}
