import { Grid, GridColumn } from "semantic-ui-react";
import { ActivityResponse as Activity } from "../../../autogenerated/models/activities/response/activity-response";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

interface Props {
    activities: Activity[];
    selectedActivity: Activity | undefined;
    selectActivity: (id: number) => void;
    cancelSelectActivity: () => void;
    editMode: boolean;
    openForm: (id?: number) => void;
    closeForm: () => void;
    createOrEdit: (activity: Activity) => void;
    deleteActivity: (id: number) => void;
}

export default function ActivityDashboard(props: Props) {
    return (
        <Grid>
            <Grid.Column width='10'>
                <ActivityList 
                activities={props.activities} selectActivity={props.selectActivity}  deleteActivity={props.deleteActivity}/>
            </Grid.Column>
            <GridColumn width='6'>
                {props.selectedActivity && !props.editMode &&
                    <ActivityDetails
                        activity={props.selectedActivity}
                        cancelSelectActivity={props.cancelSelectActivity}
                        openForm={props.openForm}
                    />}
                {props.editMode && <ActivityForm
                    closeForm={props.closeForm}
                    activity={props.selectedActivity}
                    createOrEdit={props.createOrEdit}
                />}
            </GridColumn>
        </Grid>
    )
}