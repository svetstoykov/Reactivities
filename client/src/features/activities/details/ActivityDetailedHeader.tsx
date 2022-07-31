import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Button, Header, Item, Segment, Image, Label } from "semantic-ui-react";
import { ActivityApiModel } from "../../../autogenerated/a-p-i/activities/activity-api-model";
import { format } from "date-fns";
import { reactivitiesDateFormat } from "../../../app/common/constants/GlobalConstants";
import { useStore } from "../../../app/stores/store";

const activityImageStyle = {
    filter: "brightness(30%)",
};

const activityImageTextStyle = {
    position: "absolute",
    bottom: "5%",
    left: "5%",
    width: "100%",
    height: "auto",
    color: "white",
};

interface Props {
    activity: ActivityApiModel;
}

export default observer(function ActivityDetailedHeader({ activity }: Props) {
    const {
        activityStore: { updateAttendance, updateStatus, loading },
        profileStore
    } = useStore();

    return (
        <Segment.Group>
            <Segment basic attached="top" style={{ padding: "0" }}>
                {activity.isCancelled && (
                    <Label
                        content="Cancelled"
                        color="red"
                        style={{ position: "absolute", zIndex: 1000, left: -14, top: 20 }}
                        ribbon
                    />
                )}
                <Image
                    src={`/assets/categoryImages/${activity.category}.jpg`}
                    fluid
                    style={activityImageStyle}
                />
                <Segment style={activityImageTextStyle} basic>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size="huge"
                                    content={activity.title}
                                    style={{ color: "white" }}
                                />
                                <p>{format(activity.date, reactivitiesDateFormat)}</p>
                                <Label as={Link} to={`/profiles/${activity.host.username}`}>
                                    Hosted by <strong>{activity.host.displayName}</strong>
                                </Label>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached="bottom">
                {profileStore.isActivityHost(activity.host.username) ? (
                    <>
                        <Button
                            loading={loading}
                            onClick={updateStatus}
                            color={activity.isCancelled ? "green" : "red"}
                            content={
                                activity.isCancelled ? "Re-activate Activity" : "Cancel Activity"
                            }
                        />
                        <Button
                            as={Link}
                            to={`/manage/${activity.id}`}
                            color="orange"
                            floated="right">
                            Manage Event
                        </Button>
                    </>
                ) : profileStore.isGoingToActivity(activity.id!) ? (
                    <Button onClick={updateAttendance} loading={loading}>
                        Cancel attendance
                    </Button>
                ) : (
                    <Button onClick={updateAttendance} loading={loading} color="teal">
                        Join Activity
                    </Button>
                )}
            </Segment>
        </Segment.Group>
    );
});
