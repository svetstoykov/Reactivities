import { Fragment } from "react";
import "./App.css";
import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import { observer } from "mobx-react-lite";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { Route, Switch } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";

function App() {
    return (
        <Fragment>
            <NavBar />
            <Container style={{ marginTop: "7em" }}>
                <Switch>
                    <Route exact path="/" component={HomePage} />
                    <Route path="/activities/:id" component={ActivityDetails} />
                    <Route exact path="/activities" component={ActivityDashboard}/>
                    <Route path="/createActivity" component={ActivityForm} />
                </Switch>
            </Container>
        </Fragment>
    );
}

export default observer(App);
