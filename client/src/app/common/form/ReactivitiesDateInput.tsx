import React from "react";
import { useField } from "formik";
import { Form, Label } from "semantic-ui-react";
import DatePicker ,{ReactDatePickerProps } from "react-datepicker";

export default function ReactivitiesDateInput(props: Partial<ReactDatePickerProps>) {
    const [field, meta, helpers] = useField(props.name!);

    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <DatePicker
                {...field}
                {...props}
                selected={(field.value && new Date(field.value)) || null}
                onChange={(value) => helpers.setValue(value)}
            />
            {meta.touched && meta.error ? (
                <Label pointing color="red" content={meta.error}></Label>
            ) : null}
        </Form.Field>
    );
}
