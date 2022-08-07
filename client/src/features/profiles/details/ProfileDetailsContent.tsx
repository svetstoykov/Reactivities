import { observer } from "mobx-react-lite";
import { Button, Container, Divider, Header } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

function ProfileDetailsContent() {
    const {
        profileStore: { selectedProfile, setEditDetailsMode, isCurrentProfile },
    } = useStore();

    return (
        <div>
            <Container>
                <Header as="h4" content="Email" />
                <Divider />
                <p>{selectedProfile.email}</p>
                <Header as="h4" content="Bio" />
                <Divider />
                <p
                    style={{
                        fontStyle: selectedProfile.bio ? "normal" : "italic",
                    }}>
                    {selectedProfile.bio || "Tell us something about yourself...."}
                </p>
            </Container>
            {isCurrentProfile && (
                <Button
                    style={{ marginTop: "20px" }}
                    content="Edit Profile"
                    onClick={() => setEditDetailsMode(true)}
                />
            )}
        </div>
    );
}

export default observer(ProfileDetailsContent);
