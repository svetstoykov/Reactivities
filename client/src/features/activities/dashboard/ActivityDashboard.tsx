import { observer } from "mobx-react-lite";
import { Grid, GridColumn } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { ActivityViewModel } from "../../../autogenerated/a-p-i/models/activities/activity-view-model";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

interface Props {
    activities: ActivityViewModel[];
    createOrEdit: (activity: ActivityViewModel) => void;
    deleteActivity: (id: number) => void;
    submitting: boolean;
}

function ActivityDashboard(props: Props) {
    const { activityStore } = useStore();
    const { selectedActivity, editMode } = activityStore;

    return (
        <Grid>
            <Grid.Column width="10">
                <ActivityList
                    activities={props.activities}
                    deleteActivity={props.deleteActivity}
                    submitting={props.submitting}
                />
            </Grid.Column>
            <GridColumn width="6">
                {selectedActivity && !editMode && <ActivityDetails />}
                {editMode && (
                    <ActivityForm
                        createOrEdit={props.createOrEdit}
                        submitting={props.submitting}
                    />
                )}
            </GridColumn>
        </Grid>
    );
}

export default observer(ActivityDashboard);
