import { observer } from "mobx-react-lite";
import { ChangeEvent, useEffect, useState } from "react";
import { Link, useHistory, useParams } from "react-router-dom";
import { Button, Form, Segment } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import { ActivityViewModel } from "../../../autogenerated/a-p-i/activities/models/activity-view-model";

function ActivityForm() {
    const { activityStore } = useStore();
    const history = useHistory();
    const { id } = useParams<{ id: string }>();

    const {
        loadActivity,
        createActivity,
        updateActivity,
        setLoadingInitial,
        loading,
        loadingInitial,
    } = activityStore;

    const initialState: ActivityViewModel = {
        id: undefined,
        title: "",
        date: "",
        description: "",
        category: "",
        city: "",
        venue: "",
    };

    const [activity, setActivity] = useState(initialState);

    useEffect(() => {
        if (id) {
            loadActivity(+id).then((activity) => setActivity(activity!));
            return;
        }
        setLoadingInitial(false);
    }, [id, loadActivity, setLoadingInitial]);

    function handleSubmit() {
        const createUpdateAction = activity.id
            ? updateActivity(activity)
            : createActivity(activity);

        createUpdateAction.then(() =>
            history.push(`/activities/${activity.id}`)
        );
    }

    function handleInputChange(
        event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) {
        const { name, value } = event.target;
        setActivity({ ...activity, [name]: value });
    }

    if (loadingInitial)
        return <LoadingComponent content="Loading Activity..." />;

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete="off">
                <Form.Input
                    placeholder="Title"
                    value={activity?.title}
                    name="title"
                    onChange={handleInputChange}
                />
                <Form.TextArea
                    placeholder="Description"
                    value={activity?.description}
                    name="description"
                    onChange={handleInputChange}
                />
                <Form.Input
                    placeholder="Category"
                    value={activity?.category}
                    name="category"
                    onChange={handleInputChange}
                />
                <Form.Input
                    type="date"
                    placeholder="Date"
                    value={activity?.date}
                    name="date"
                    onChange={handleInputChange}
                />
                <Form.Input
                    placeholder="City"
                    value={activity?.city}
                    name="city"
                    onChange={handleInputChange}
                />
                <Form.Input
                    placeholder="Venue"
                    value={activity?.venue}
                    name="venue"
                    onChange={handleInputChange}
                />
                <Button
                    loading={loading}
                    floated="right"
                    positive
                    type="submit"
                    content="Submit"
                />
                <Button
                    as={Link}
                    to="/activities"
                    floated="right"
                    type="submit"
                    color="red"
                    content="Cancel"
                />
            </Form>
        </Segment>
    );
}

export default observer(ActivityForm);
