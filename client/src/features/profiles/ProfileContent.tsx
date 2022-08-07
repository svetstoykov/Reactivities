import { observer } from "mobx-react-lite";
import { Tab } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import ProfileDetails from "./details/ProfileDetails";
import ProfileFollowings from "./ProfileFollowings";

function ProfileContent() {
    const {
        profileStore: { setActiveTab },
    } = useStore();

    const panes = [
        { menuItem: "About", render: () => <ProfileDetails /> },
        { menuItem: "Events", render: () => <Tab.Pane>Events content</Tab.Pane> },
        { menuItem: "Followers", render: () => <ProfileFollowings /> },
        { menuItem: "Following", render: () => <ProfileFollowings /> },
    ];

    return (
        <Tab
            menu={{ fluid: true, vertical: true, tabular: "right" }}
            panes={panes}
            onTabChange={(e, data) => setActiveTab(+data.activeIndex!)}
        />
    );
}

export default observer(ProfileContent);
