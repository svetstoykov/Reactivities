import { Fragment } from "react";
import "./App.css";
import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import { observer } from "mobx-react-lite";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { Route, Switch, useLocation } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";
import { ToastContainer, Flip } from "react-toastify";
import TestErrors from "../../features/errors/TestErrors";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import { useStore } from "../stores/store";
import { useEffect } from "react";
import LoadingComponent from "./LoadingComponent";
import ModalContainer from "../common/modals/ModalContainer";
import ProfilePage from "../../features/profiles/ProfilePage";
import PrivateRoute from "./PrivateRoute";

function App() {
    const location = useLocation();
    const { profileStore, commonStore } = useStore();

    useEffect(() => {
        if (commonStore.token) {
            profileStore.loadCurrentProfile().finally(() => commonStore.setAppLoaded());
            return;
        }
        commonStore.setAppLoaded();
    }, [commonStore, profileStore]);

    if(!commonStore.appLoaded) return <LoadingComponent content="Loading app..."/>

    return (
        <>
            <ToastContainer hideProgressBar theme="colored" transition={Flip} newestOnTop />
            <ModalContainer/>
            <Route exact path="/" component={HomePage} />
            <Route
                path="/(.+)"
                render={() => (
                    <Fragment>
                        <NavBar />
                        <Container style={{ marginTop: "7em" }}>
                            <Switch>
                                <PrivateRoute path="/activities/:id" component={ActivityDetails} />
                                <PrivateRoute exact path="/activities" component={ActivityDashboard} />
                                <PrivateRoute
                                    key={location.key}
                                    path={["/createActivity", "/manage/:id"]}
                                    component={ActivityForm}
                                />
                                <PrivateRoute path="/profile/:username" component={ProfilePage}/>
                                <PrivateRoute path="/errors" component={TestErrors} />
                                <Route path="/server-error" component={ServerError} />
                                <Route component={NotFound} />
                            </Switch>
                        </Container>
                    </Fragment>
                )}
            />
        </>
    );
}

export default observer(App);
