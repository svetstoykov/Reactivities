import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Card, Grid, Tab } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { reactivitiesDateFormat } from "../../app/common/constants/GlobalConstants";

function ProfileEventsCardGrid() {
    const {
        profileStore: { activities, loadingActivities },
    } = useStore();

    return (
        <Tab.Pane loading={loadingActivities} attached={false}>
            <Grid>
                <Grid.Column width={16}>
                    <Card.Group itemsPerRow={4}>
                        {activities?.map((activity) => (
                            <Card
                                as={Link}
                                to="nowhere"
                                image={`/assets/categoryImages/${activity.category}.jpg`}
                                header={activity.title}
                                meta={format(activity.date, reactivitiesDateFormat)}></Card>
                        ))}
                    </Card.Group>
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    );
}

export default observer(ProfileEventsCardGrid);
