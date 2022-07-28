import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Grid, GridColumn } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityFilters from "./ActivityFilters";
import ActivityList from "./ActivityList";

function ActivityDashboard() {
    const { activityStore } = useStore();
    const { loadActivities, activitiesRegistry } = activityStore;

    useEffect(() => {
        if (activitiesRegistry.size <= 1) {
            loadActivities();
        }
    }, [loadActivities, activitiesRegistry.size]);

    if (activityStore.loadingInitial) return <LoadingComponent content="Loading Activities" />;

    return (
        <Grid>
            <Grid.Column width="10">
                <ActivityList />
            </Grid.Column>
            <GridColumn width="6">
                <ActivityFilters />
            </GridColumn>
        </Grid>
    );
}

export default observer(ActivityDashboard);
