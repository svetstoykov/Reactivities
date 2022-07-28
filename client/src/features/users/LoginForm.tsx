import { Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header } from "semantic-ui-react";
import ReactivitiesTextInput from "../../app/common/form/ReactivitiesTextInput";
import { useStore } from "../../app/stores/store";

function LoginForm() {
    const { userStore } = useStore();

    return (
        <Formik
            initialValues={{ email: "", password: "" }}
            onSubmit={(values) => userStore.login(values)}>
            {({ handleSubmit, isSubmitting }) => (
                <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                    <Header as="h2" content="Login" color="teal" textAlign="center" />
                    <ReactivitiesTextInput name="email" placeholder="Email" />
                    <ReactivitiesTextInput name="password" placeholder="Password" type="password" />
                    <Button loading={isSubmitting} positive content="Login" type="submit" fluid />
                </Form>
            )}
        </Formik>
    );
}

export default observer(LoginForm);
