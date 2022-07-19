import React from "react";
import { useField } from "formik";
import { Form, Label } from "semantic-ui-react";

interface Props {
    placeholder: string;
    name: string;
    label?: string;
}

export default function ReactivitiesTextInput(props: Props) {
    const [field, meta] = useField(props.name);

    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{props.label}</label>
            <input {...field} {...props} />
            {meta.touched && meta.error ? (
                <Label pointing color="red" content={meta.error}></Label>
            ) : null}
        </Form.Field>
    );
}
