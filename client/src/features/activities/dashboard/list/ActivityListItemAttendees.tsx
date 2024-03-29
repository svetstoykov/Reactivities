import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { List, Image, Label, Popup } from "semantic-ui-react";
import { ProfileApiModel } from "../../../../autogenerated/a-p-i/profiles/profile-api-model";
import ProfileCard from "../../../profiles/ProfileCard";

interface Props {
    attendees: ProfileApiModel[];
}

function ActivityListItemAttendees({ attendees }: Props) {
    const styles = {
        borderColor: "orange",
        borderWidth: 3,
    };

    if (attendees.length > 0) {
        return (
            <List horizontal>
                {attendees.map((attendee) => (
                    <Popup
                        hoverable
                        key={attendee.username}
                        trigger={
                            <List.Item key={attendee.username} as={Link} to={`/profile/${attendee.username}`}>
                                <Image
                                    size="mini"
                                    circular
                                    src={attendee.pictureUrl || "/assets/user.png"}
                                    bordered
                                    style ={attendee.following ? styles : null}
                                />
                            </List.Item>
                        }>
                        <Popup.Content>
                            <ProfileCard profile={attendee} />
                        </Popup.Content>
                    </Popup>
                ))}
            </List>
        );
    }

    return <Label>No attendees at this time.</Label>;
}

export default observer(ActivityListItemAttendees);
