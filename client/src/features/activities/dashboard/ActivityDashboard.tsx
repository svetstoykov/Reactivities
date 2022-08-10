import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Grid, GridColumn, Loader } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityFilters from "./ActivityFilters";
import ActivityList from "./ActivityList";
import InfiniteScroll from "react-infinite-scroller";

function ActivityDashboard() {
    const { activityStore } = useStore();
    const { loadActivities, activitiesRegistry, setPagingParams, pagination } = activityStore;
    const [loadingNext, setLoadingNext] = useState(false);

    function handleGetNext() {
        setLoadingNext(true);

        setPagingParams({
            pageSize: 5,
            pageNumber: pagination.pageNumber + 1,
        });
        loadActivities().then(() => setLoadingNext(false));
    }

    useEffect(() => {
        if (activitiesRegistry.size <= 1) loadActivities();
        
    }, [loadActivities, activitiesRegistry.size]);

    if (activityStore.loadingInitial && !loadingNext) return <LoadingComponent content="Loading Activities" />;

    return (
        <Grid>
            <Grid.Column width="10">
                <InfiniteScroll
                    pageStart={0}
                    hasMore={
                        !loadingNext &&
                        !!pagination &&
                        pagination.pageNumber < pagination.totalPages
                    }
                    initialLoad={false}
                    loadMore={handleGetNext}>
                    <ActivityList />
                </InfiniteScroll>
            </Grid.Column>
            <GridColumn width="6">
                <ActivityFilters />
            </GridColumn>
            <GridColumn width={10}>
                <Loader active={loadingNext} />
            </GridColumn>
        </Grid>
    );
}

export default observer(ActivityDashboard);
