import React from "react";
import { Segment, List, Label, Item, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { ProfileApiModel } from "../../../autogenerated/a-p-i/profiles/profile-api-model";

interface Props {
    attendees: ProfileApiModel[];
    host: ProfileApiModel;
}

export default observer(function ActivityDetailedSidebar({ attendees, host }: Props) {
    return (
        <>
            <Segment
                textAlign="center"
                style={{ border: "none" }}
                attached="top"
                secondary
                inverted
                color="teal">
                3 People Going
            </Segment>
            <Segment attached>
                <List relaxed divided>
                    <Item key={host.username} style={{ position: "relative" }}>
                        <Label style={{ position: "absolute" }} color="orange" ribbon="right">
                            Host
                        </Label>
                        <Image size="tiny" src={host.pictureUrl || "/assets/user.png"} />
                        <Item.Content verticalAlign="middle">
                            <Item.Header as="h3">
                                <Link to={`/profiles/${host.username}`}>{host.displayName}</Link>
                            </Item.Header>
                            <Item.Extra style={{ color: "orange" }}>Following</Item.Extra>
                        </Item.Content>
                    </Item>

                    {attendees.map((a) => (
                        <Item key={a.username} style={{ position: "relative" }}>
                            <Image size="tiny" src={a.pictureUrl || "/assets/user.png"} />
                            <Item.Content verticalAlign="middle">
                                <Item.Header as="h3">
                                <Link to={`/profiles/${a.username}`}>{a.displayName}</Link>
                                </Item.Header>
                                <Item.Extra style={{ color: "orange" }}>Following</Item.Extra>
                            </Item.Content>
                        </Item>
                    ))}
                </List>
            </Segment>
        </>
    );
});
