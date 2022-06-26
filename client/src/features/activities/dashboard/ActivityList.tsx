import { observer } from "mobx-react-lite";
import { SyntheticEvent, useState } from "react";
import { Button, Item, Label, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

function ActivityList(){
    const [target, setTarget] = useState('');
    const {activityStore} = useStore();

    const {deleteActivity, loading, activitiesByDate} = activityStore;

    function handleDeleteActivity(e: SyntheticEvent<HTMLButtonElement>, id:number){
        setTarget(e.currentTarget.name);
        deleteActivity(id);
    }

    return (
        <Segment>
            <Item.Group divided>
                {activitiesByDate.map(activity => (
                    <Item key={activity.id}>
                        <Item.Content>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Meta>{activity.date}</Item.Meta>
                            <Item.Description>
                                <div>{activity.description}</div>
                                <div>{activity.city}, {activity.venue}</div>
                            </Item.Description>
                            <Item.Extra>
                                <Button 
                                    onClick={() => activityStore.selectActivity(activity.id!)} 
                                    floated='right' 
                                    content='View' 
                                    color='blue'/>
                                <Button 
                                    name = {activity.id}
                                    loading={loading && target === activity.id!.toString()} 
                                    onClick={(e) => handleDeleteActivity(e, activity.id!)} 
                                    floated='right' 
                                    content='Delete' 
                                    color='red'/>
                                <Label basic content={activity.category}/>
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    )
}

export default observer(ActivityList);