import { observer } from "mobx-react-lite";
import {
    Button,
    Divider,
    Grid,
    Header,
    Item,
    Popup,
    Reveal,
    Segment,
    Statistic,
    StatisticGroup,
} from "semantic-ui-react";
import PhotoUploadWidget from "../../app/common/imageUpload/PhotoUploadWidget";
import { useStore } from "../../app/stores/store";
import { ProfileApiModel } from "../../autogenerated/a-p-i/profiles/profile-api-model";

interface Props {
    profile: ProfileApiModel;
}

function ProfileHeader({ profile }: Props) {
    const { modalStore } = useStore();

    return (
        <Segment>
            <Grid>
                <Grid.Column width={12}>
                    <Item.Group>
                        <Item>
                            <Popup
                                position="bottom center"
                                content="Click to change profile picture"
                                inverted
                                trigger={
                                    <Item.Image
                                        avatar
                                        style={{ cursor: "pointer" }}
                                        size="small"
                                        src={profile.pictureUrl || "/assets/user.png"}
                                        onClick={() => modalStore.openModal(<PhotoUploadWidget />,  true)}
                                    />
                                }
                            />
                            <Item.Content verticalAlign="middle">
                                <Header as="h1" content={profile.displayName} />
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Grid.Column>
                <Grid.Column width={4}>
                    <StatisticGroup>
                        <Statistic label="Followers" value="42" />
                        <Statistic label="Following" value="5" />
                    </StatisticGroup>
                    <Divider />
                    <Reveal animated="move">
                        <Reveal.Content visible style={{ width: "100%" }}>
                            <Button fluid color="teal" content="Following" />
                        </Reveal.Content>
                        <Reveal.Content hidden style={{ width: "100%" }}>
                            <Button
                                fluid
                                basic
                                color={true ? "green" : "red"}
                                content={true ? "Follow" : "Unfollow"}
                            />
                        </Reveal.Content>
                    </Reveal>
                </Grid.Column>
            </Grid>
        </Segment>
    );
}

export default observer(ProfileHeader);
