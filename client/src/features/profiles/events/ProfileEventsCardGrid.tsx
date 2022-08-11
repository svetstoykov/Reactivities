import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Card, Grid, Header, Icon, Tab } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { reactivitiesDateFormat } from "../../../app/common/constants/GlobalConstants";

function ProfileEventsCardGrid() {
    const {
        profileStore: { activities, loadingActivities },
    } = useStore();

    return (
        <Tab.Pane loading={loadingActivities} attached={false}>
            {activities?.length > 0 ? (
                <Grid>
                    <Grid.Column width={16}>
                        <Card.Group itemsPerRow={4}>
                            {activities?.map((activity) => (
                                <Card
                                    as={Link}
                                    to={`/activities/${activity.id}`}
                                    image={`/assets/categoryImages/${activity.category}.jpg`}
                                    header={activity.title}
                                    meta={format(activity.date, reactivitiesDateFormat)}></Card>
                            ))}
                        </Card.Group>
                    </Grid.Column>
                </Grid>
            ) : (
                <div>
                    <Header as="h2" icon textAlign="center">
                        <Icon name="calendar alternate outline" circular />
                        <Header.Content>No events available</Header.Content>
                    </Header>
                </div>
            )}
        </Tab.Pane>
    );
}

export default observer(ProfileEventsCardGrid);
