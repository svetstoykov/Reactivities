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
        profileStore: { loadProfile, loadingUser, selectedProfile, setActiveTab },
    } = useStore();

    useEffect(() => {
        loadProfile(username);

        return () => {
            setActiveTab(0)
        };
    }, [loadProfile, username, setActiveTab]);

    if (loadingUser) return <LoadingComponent content="Loading profile..." />;

    return (
        <Grid>
            <Grid.Column width={16}>
                {selectedProfile && (
                    <>
                        <ProfileHeader />
                        <ProfileContent />
                    </>
                )}
            </Grid.Column>
        </Grid>
    );
}

export default observer(ProfilePage);
