import { Form, Formik } from "formik";
import React from "react";
import { Button } from "semantic-ui-react";
import ReactivitiesTextInput from "../../app/common/form/ReactivitiesTextInput";
import { useStore } from "../../app/stores/store";

export default function LoginForm() {
    const {userStore} = useStore();

    return (
        <Formik
            initialValues={{ email: "", password: "" }}
            onSubmit={(values) => userStore.login(values)}>
            {({ handleSubmit, isSubmitting }) => (
                <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                    <ReactivitiesTextInput name="email" placeholder="Email" />
                    <ReactivitiesTextInput name="password" placeholder="Password" type="password" />
                    <Button loading={isSubmitting} positive content="Login" type="submit" fluid />
                </Form>
            )}
        </Formik>
    );
}
