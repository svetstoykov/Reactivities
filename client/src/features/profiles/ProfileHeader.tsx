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

function ProfileHeader() {
    const {
        modalStore,
        profileStore: { currentProfile, selectedProfile ,isCurrentProfile },
    } = useStore();

    return (
        <Segment>
            <Grid>
                <Grid.Column width={12}>
                    <Item.Group>
                        <Item>
                            {isCurrentProfile ? (
                                <Popup
                                    position="bottom center"
                                    content="Click to change profile picture"
                                    inverted
                                    trigger={
                                        <Item.Image
                                            avatar
                                            style={{ cursor: "pointer" }}
                                            size="small"
                                            src={currentProfile.pictureUrl || "/assets/user.png"}
                                            onClick={() =>
                                                modalStore.openModal(<PhotoUploadWidget />, true)
                                            }
                                        />
                                    }
                                />
                            ) : (
                                <Item.Image
                                    avatar
                                    size="small"
                                    src={selectedProfile?.pictureUrl || "/assets/user.png"}
                                />
                            )}
                            <Item.Content verticalAlign="middle">
                                <Header as="h1" content={selectedProfile?.displayName} />
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
