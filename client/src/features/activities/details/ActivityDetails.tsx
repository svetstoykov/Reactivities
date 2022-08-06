import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { Grid, GridColumn } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityDetailedInfo from "./ActivityDetailedInfo";
import ActivityDetaledHeader from "./ActivityDetailedHeader";
import ActivityDetailedChat from "./ActivityDetailedChat";
import ActivityDetailedSidebar from "./ActivityDetailedSidebar";

function ActivityDetails() {
    const { activityStore } = useStore();
    const { selectedActivity, loadActivity, loadingInitial, clearSelectedActivity } = activityStore;
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        if (id) {
            loadActivity(+id);
        }

        return () => clearSelectedActivity();
    }, [id, loadActivity, clearSelectedActivity]);

    if (loadingInitial || !selectedActivity) return <LoadingComponent />;

    return (
        <Grid>
            <GridColumn width={10}>
                <ActivityDetaledHeader activity={selectedActivity} />
                <ActivityDetailedInfo activity={selectedActivity} />
                <ActivityDetailedChat activityId={selectedActivity.id!} />
            </GridColumn>
            <GridColumn width={6}>
                <ActivityDetailedSidebar
                    attendees={selectedActivity.attendees}
                    host={selectedActivity.host}
                />
            </GridColumn>
        </Grid>
    );
}

export default observer(ActivityDetails);
