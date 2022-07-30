import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Button, Header, Item, Segment, Image, Label } from "semantic-ui-react";
import { ActivityApiModel } from "../../../autogenerated/a-p-i/activities/models/activity-api-model";
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
    const { userStore, activityStore: {updateAttendance, loading} } = useStore();


    return (
        <Segment.Group>
            <Segment basic attached="top" style={{ padding: "0" }}>
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
                {userStore.isUserActivityHost(activity.host.username) ? (
                    <Button as={Link} to={`/manage/${activity.id}`} color="orange" floated="right">
                        Manage Event
                    </Button>
                ) : userStore.isUserGoingToActivity(activity.attendees) ? (
                    <Button onClick={updateAttendance} loading={loading}>Cancel attendance</Button>
                ) : (
                    <Button onClick={updateAttendance} loading={loading} color="teal">Join Activity</Button>
                )}
            </Segment>
        </Segment.Group>
    );
});
