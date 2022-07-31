import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { Grid } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";
import ProfileContent from "./ProfileContent";
import ProfileHeader from "./ProfileHeader";

export default function ProfilePage() {
    const { username } = useParams<{ username: string }>();
    const {
        profileStore: { loadProfile, loadingUser, currentProfile },
    } = useStore();

    useEffect(() => {
        loadProfile(username);
    }, [loadProfile, username]);

    if (loadingUser) return <LoadingComponent content="Loading profile..." />;

    return (
        <Grid>
            <Grid.Column width={16}>
                <ProfileHeader profile={currentProfile!} />
                <ProfileContent profile={currentProfile!} />
            </Grid.Column>
        </Grid>
    );
}
