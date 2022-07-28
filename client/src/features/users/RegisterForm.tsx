import { Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header } from "semantic-ui-react";
import ReactivitiesTextInput from "../../app/common/form/ReactivitiesTextInput";
import { useStore } from "../../app/stores/store";
import * as Yup from "yup";

function RegisterForm() {
    const { userStore } = useStore();

    const validationSchema = Yup.object({
        displayName: Yup.string().required("Display name is required"),
        username: Yup.string().required("Username  is required"),
        email: Yup.string().required("Email is required").email("Invalid email format"),
        password: Yup.string().required("Password is required"),
    });

    return (
        <Formik
            initialValues={{ displayName: "", username: "", email: "", password: "" }}
            onSubmit={(values) => userStore.register(values)}
            validationSchema={validationSchema}>
            {({ handleSubmit, isSubmitting, isValid, dirty }) => (
                <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                    <Header as="h2" content="Sign up to Reactivities" color="teal" textAlign="center" />
                    <ReactivitiesTextInput name="displayName" placeholder="Display Name" />
                    <ReactivitiesTextInput name="username" placeholder="Username" />
                    <ReactivitiesTextInput name="email" placeholder="Email" />
                    <ReactivitiesTextInput name="password" placeholder="Password" type="password" />
                    <Button
                        disabled={isSubmitting || !dirty || !isValid}
                        loading={isSubmitting}
                        positive
                        content="Register"
                        type="submit"
                        fluid
                    />
                </Form>
            )}
        </Formik>
    );
}

export default observer(RegisterForm);
