import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import { Button, Card, Grid, GridColumn, Image } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityDetailedInfo from "./ActivityDetailedInfo";
import ActivityDetaledHeader from "./ActivityDetaledHeader";
import ActivityDetailedChat from "./ActivityDetailedChat";
import ActivityDetailedSidebar from "./ActivityDetailedSidebar";

function ActivityDetails() {
    const { activityStore } = useStore();
    const { selectedActivity, loadActivity, loadingInitial } = activityStore;
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        if (id) {
            loadActivity(+id);
        }
    }, [id, loadActivity]);

    if (loadingInitial || !selectedActivity) return <LoadingComponent />;

    return (
        <Grid>
            <GridColumn width={10}>
                <ActivityDetaledHeader activity={selectedActivity} />
                <ActivityDetailedInfo activity={selectedActivity} />
                <ActivityDetailedChat />
            </GridColumn>
            <GridColumn width={6}>
                <ActivityDetailedSidebar/>
            </GridColumn>
        </Grid>
    );
}

export default observer(ActivityDetails);
