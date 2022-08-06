import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { Grid } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";
import ProfileContent from "./ProfileContent";
import ProfileHeader from "./ProfileHeader";

function ProfilePage() {
    const { username } = useParams<{ username: string }>();
    const {
        profileStore: { loadProfile, loadingUser, selectedProfile },
    } = useStore();

    useEffect(() => {
        loadProfile(username);
    }, [loadProfile, username]);

    if (loadingUser) return <LoadingComponent content="Loading profile..." />;

    return (
        <Grid>
            <Grid.Column width={16}>
                {selectedProfile && (
                    <>
                        <ProfileHeader profile={selectedProfile!} />
                        <ProfileContent profile={selectedProfile!} />
                    </>
                )}
            </Grid.Column>
        </Grid>
    );
}

export default observer(ProfilePage);
