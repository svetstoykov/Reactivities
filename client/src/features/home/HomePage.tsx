import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button, Icon } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import LoginForm from "../users/LoginForm";
import RegisterForm from "../users/RegisterForm";

function HomePage() {
    const { userStore, modalStore } = useStore();

    return (
        <Segment inverted textAlign="center" vertical className="masthead">
            <Container text>
                <Header as="h1" inverted>
                    <Image
                        size="massive"
                        src="/assets/logo.png"
                        alt="logo"
                        style={{ marginBottom: 12 }}
                    />
                    Reactivities
                </Header>
                {userStore.IsLoggedIn ? (
                    <>
                        <Header as="h2" inverted content="Welcome to Reactivities" />
                        <Button as={Link} to="/activities" size="huge" inverted>
                            Go to Activities!
                        </Button>
                    </>
                ) : (
                    <>
                        <Button
                            onClick={() => modalStore.openModal(<LoginForm />)}
                            size="huge"
                            icon
                            labelPosition="left">
                            <Icon name="sign in" />
                            Login
                        </Button>
                        <Button
                            onClick={() => modalStore.openModal(<RegisterForm />)}
                            size="huge"
                            icon
                            labelPosition="right">
                            Register
                            <Icon name="signup" />
                        </Button>
                    </>
                )}
            </Container>
        </Segment>
    );
}

export default observer(HomePage);
