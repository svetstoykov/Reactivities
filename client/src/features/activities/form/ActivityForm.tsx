import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Link, useHistory, useParams } from "react-router-dom";
import { Button, Segment } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import { ActivityViewModel } from "../../../autogenerated/a-p-i/activities/models/activity-view-model";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import ReactivitiesTextInput from "../../../app/common/form/ReactivitiesTextInput";
import ReactivitiesTextArea from "../../../app/common/form/ReactivitiesTextArea";
import ReactivitiesSelectInput from "../../../app/common/form/ReactivitiesSelectInput";

function ActivityForm() {
    const { activityStore } = useStore();
    const history = useHistory();
    const { id } = useParams<{ id: string }>();

    const {
        loadActivity,
        createActivity,
        updateActivity,
        setLoadingInitial,
        loadCategories,
        loading,
        loadingInitial,
        categories,
    } = activityStore;

    const initialState: ActivityViewModel = {
        id: undefined,
        title: "",
        date: "",
        description: "",
        categoryId: 0,
        category: "",
        city: "",
        venue: "",
    };

    const [activity, setActivity] = useState(initialState);

    const validationSchema = Yup.object({
        title: Yup.string().required("Title is required"),
        description: Yup.string().required("Description is required"),
        venue: Yup.string().required("Venue is required"),
        city: Yup.string().required("City is required"),
        categoryId: Yup.string().required("Category is required"),
        date: Yup.string().required("Date is required"),
    });

    useEffect(() => {
        if (id) {
               loadActivity(+id).then((activity) => setActivity(activity!));
            return;
        }
        setLoadingInitial(false);
    }, [id, loadActivity, setLoadingInitial]);

    useEffect(() => {
        if (categories.length <= 0) {
            loadCategories();
        }
    }, [loadCategories, categories.length]);

    // function handleSubmit() {
    //     const createUpdateAction = activity.id
    //         ? updateActivity(activity)
    //         : createActivity(activity);

    //     createUpdateAction.then(() =>
    //         history.push(`/activities/${activity.id}`)
    //     );
    // }

    // function handleChange(
    //     event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    // ) {
    //     const { name, value } = event.target;
    //     setActivity({ ...activity, [name]: value });
    // }

    if (loadingInitial) return <LoadingComponent content="Loading Activity..." />;

    return (
        <Segment clearing>
            <Formik
                validationSchema={validationSchema}
                enableReinitialize
                initialValues={activity}
                onSubmit={(values) => console.log(values)}>
                {({ handleSubmit }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <ReactivitiesTextInput placeholder="Title" name="title" />
                        <ReactivitiesSelectInput
                            options={categories.map((c) => ({
                                key: c.id,
                                value: c.id,
                                text: c.name,
                            }))}
                            placeholder="Category"
                            name="categoryId"
                        />
                        <ReactivitiesTextInput placeholder="Date" name="date" />
                        <ReactivitiesTextInput placeholder="City" name="city" />
                        <ReactivitiesTextInput placeholder="Venue" name="venue" />
                        <Button loading={loading} floated="right" positive type="submit" content="Submit" />
                        <Button as={Link} to="/activities" floated="right" type="submit" color="red" content="Cancel" />
                    </Form>
                )}
            </Formik>
        </Segment>
    );
}

export default observer(ActivityForm);
