import { observer } from "mobx-react-lite";
import { Link, NavLink } from "react-router-dom";
import { Button, Container, Menu, Image, Dropdown, DropdownMenu } from "semantic-ui-react";
import { useStore } from "../stores/store";

function NavBar() {
    const {
        userStore,
        profileStore: { currentProfile },
    } = useStore();

    return (
        <Menu inverted fixed="top">
            <Container>
                <Menu.Item as={NavLink} to="/" exact header name="Reactivities">
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: "10px" }} />
                    Reactivities
                </Menu.Item>
                <Menu.Item as={NavLink} to="/activities" name="Activities" />
                <Menu.Item as={NavLink} to="/errors" name="Errors" />
                <Menu.Item>
                    <Button
                        as={NavLink}
                        to="/createActivity"
                        exact
                        positive
                        content="Create Activity"
                    />
                </Menu.Item>
                <Menu.Item position="right">
                    <Image src={currentProfile.pictureUrl || "/assets/user.png"} avatar spaced="right" />
                    <Dropdown pointing="top left" text={currentProfile.displayName}>
                        <DropdownMenu>
                            <Dropdown.Item
                                as={Link}
                                to={`/profile/${currentProfile.username}`}
                                text="My Profile"
                                icon="user"
                            />
                            <Dropdown.Item onClick={userStore.logout} text="Logout" icon="power" />
                        </DropdownMenu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    );
}

export default observer(NavBar);
