import { observer } from "mobx-react-lite";
import { Header, Tab } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import ProfileDetailsEditForm from "./ProfileDetailsEditForm";
import ProfileDetailsContent from "./ProfileDetailsContent";

function ProfileDetails() {
    const {
        profileStore: { selectedProfile, editDetailsMode },
    } = useStore();

    return (
        <Tab.Pane>
            <Header
                icon="user"
                as="h3"
                content={`About ${selectedProfile.displayName}`}
                style={{ paddingBottom: "25px" }}
            />
            {editDetailsMode ? <ProfileDetailsEditForm /> : <ProfileDetailsContent />}
        </Tab.Pane>
    );
}

export default observer(ProfileDetails);
