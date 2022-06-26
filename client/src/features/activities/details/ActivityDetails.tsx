import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { Button, Card, Image } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";

function ActivityDetails() {
    const { activityStore } = useStore();
    const { selectedActivity, loadActivity, loadingInitial} = activityStore;
    const {id} = useParams<{id: string}>();

    useEffect(() => {
        if(id){
            loadActivity(+id)
        }
    },[id, loadActivity])

    if (loadingInitial || !selectedActivity) return <LoadingComponent />;

    return (
        <Card>
            <Image
                src={`/assets/categoryImages/${selectedActivity.category}.jpg`}
            />
            <Card.Content>
                <Card.Header>{selectedActivity.title}</Card.Header>
                <Card.Meta>
                    <span>{selectedActivity.date}</span>
                </Card.Meta>
                <Card.Description>
                    {selectedActivity.description}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Button.Group widths="2">
                    <Button
                        basic
                        color="blue"
                        content="Edit"
                    />
                    <Button
                        basic
                        color="grey"
                        content="Cancel"
                    />
                </Button.Group>
            </Card.Content>
        </Card>
    );
}

export default observer(ActivityDetails);
