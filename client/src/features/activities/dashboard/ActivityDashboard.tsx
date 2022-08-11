import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Grid, GridColumn, Loader } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import ActivityFilters from "./ActivityFilters";
import ActivityList from "./list/ActivityList";
import InfiniteScroll from "react-infinite-scroller";
import ActivityListItemPlaceholder from "./list/ActivityListItemPlaceholder";

function ActivityDashboard() {
    const { activityStore } = useStore();
    const {
        loadActivities,
        activitiesRegistry,
        setActivityListParams,
        activityListInputModel,
        pagination,
    } = activityStore;
    const [loadingNext, setLoadingNext] = useState(false);

    function handleGetNext() {
        setLoadingNext(true);

        setActivityListParams({ ...activityListInputModel, pageNumber: pagination.pageNumber + 1 });
        loadActivities().then(() => setLoadingNext(false));
    }

    useEffect(() => {
        if (activitiesRegistry.size <= 1) loadActivities();
    }, [loadActivities, activitiesRegistry.size]);

    return (
        <Grid>
            <Grid.Column width="10">
                {activityStore.loadingInitial && !loadingNext ? (
                    <>
                        <ActivityListItemPlaceholder />
                        <ActivityListItemPlaceholder />
                    </>
                ) : (
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
                )}
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
