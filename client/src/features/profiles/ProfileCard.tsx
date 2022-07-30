import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Card, Image, Icon } from "semantic-ui-react";
import { ProfileApiModel } from "../../autogenerated/a-p-i/common/identity/models/profile-api-model";

interface Props {
    profile: ProfileApiModel;
}

function ProfileCard({ profile }: Props) {
    return (
        <Card as={Link} to={`/profiles/${profile.username}`}>
            <Image src={profile.image || "/assets/user.png"} wrapped ui={false} />
            <Card.Content>
                <Card.Header>{profile.displayName}</Card.Header>
                <Card.Description>{profile.bio}</Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Icon name="user" />
                22 Friends
            </Card.Content>
        </Card>
    );
}

export default observer(ProfileCard);