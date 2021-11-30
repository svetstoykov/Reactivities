import { Fragment, useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { Activity } from '../../autogenerated/domain/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);

  useEffect(() => {
    axios.get("http://localhost:51004/api/Activities")
      .then(response => {
        setActivities(response.data);
      });
  }, []);

  function handleSelectActivity(id: number){
    setSelectedActivity(activities.find(a => a.id === id));
  }

  function handleCancelSelectedActivity(){
    setSelectedActivity(undefined);
  }

  return (
    <Fragment >
      <NavBar />
      <Container style={{marginTop: '7em'}}>
       <ActivityDashboard 
        activities={activities}
        selectedActivity={selectedActivity}
        selectActivity={handleSelectActivity}
        cancelSelectActivity={handleCancelSelectedActivity}/>
      </Container>
    </Fragment>
  );
}

export default App;
