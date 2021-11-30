import { Grid, GridColumn } from "semantic-ui-react";
import { Activity } from "../../../autogenerated/domain/activity";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

interface Props{
    activities: Activity[];
    selectedActivity: Activity | undefined;
    selectActivity: (id: number) => void;
    cancelSelectActivity: () => void;
}

export default function ActivityDashboard(props: Props) {
    return (
        <Grid>
            <Grid.Column width='10'>
               <ActivityList activities={props.activities} selectActivity={props.selectActivity}/>
            </Grid.Column>
            <GridColumn width='6'>
                {props.selectedActivity && <ActivityDetails activity={props.selectedActivity} cancelSelectActivity={props.cancelSelectActivity}/>}
                <ActivityForm/>
            </GridColumn>
        </Grid>
    )
}