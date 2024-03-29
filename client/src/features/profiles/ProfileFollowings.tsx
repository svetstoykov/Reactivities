import { observer } from "mobx-react-lite";
import { Card, Grid, Header, Icon, Tab } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import ProfileCard from "./ProfileCard";

function ProfileFollowings() {
    const { profileStore } = useStore();
    const { selectedProfile, followings, loadingFollowings, activeTab } = profileStore;

    return (
        <Tab.Pane loading={loadingFollowings}>
            <Grid>
                <Grid.Column width="16">
                    <Header
                        floated="left"
                        icon="user"
                        content={
                            activeTab === 2
                                ? `People following ${selectedProfile!.displayName}`
                                : `People ${selectedProfile?.displayName} is following`
                        }
                    />
                </Grid.Column>
                <Grid.Column width="16">
                    {followings && followings.length > 0 ? (
                        <Card.Group itemsPerRow={4}>
                            {followings?.map((profile) => (
                                <ProfileCard key={profile.username} profile={profile} />
                            ))}
                        </Card.Group>
                    ) : (
                        <div>
                            <Header as="h2" icon textAlign="center">
                                <Icon name="users" circular />
                                <Header.Content>{`No ${activeTab === 2 ? 'followers' : 'followings'} available`}</Header.Content>
                            </Header>
                        </div>
                    )}
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    );
}

export default observer(ProfileFollowings);
