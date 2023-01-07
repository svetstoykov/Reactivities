import { Formik, Form } from "formik";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Link } from "react-router-dom";
import { Segment, Header, Comment, Button } from "semantic-ui-react";
import ReactivitiesTextArea from "../../../app/common/form/ReactivitiesTextArea";
import { useStore } from "../../../app/stores/store";
import { AddCommentApiModel } from "../../../autogenerated/a-p-i/activities/comments/add-comment-api-model";
import * as Yup from "yup";
import { formatDistanceToNow } from "date-fns";

interface Props {
    activityId: number;
}

function ActivityDetailedChat({ activityId }: Props) {
    const { commentStore } = useStore();

    useEffect(() => {
        if (activityId) {
            commentStore.createHubConnection(activityId);
        }

        return () => {
            commentStore.clearComments();
        };
    }, [commentStore, activityId]);

    const initialCommentValues: AddCommentApiModel = {
        content: "",
        username: "",
        activityId: 0,
    };

    const validationSchema = Yup.object({
        content: Yup.string().required("Comment content is required"),
    });

    return (
        <>
            <Segment
                textAlign="center"
                attached="top"
                inverted
                color="teal"
                style={{ border: "none" }}>
                <Header>Chat about this event</Header>
            </Segment>
            <Segment attached clearing>
                <Formik
                    validationSchema={validationSchema}
                    initialValues={initialCommentValues}
                    onSubmit={(values, { resetForm }) =>
                        commentStore.addComment(values).then(() => resetForm())
                    }>
                    {({ isSubmitting, isValid }) => (
                        <Form className="ui form">
                            <ReactivitiesTextArea
                                placeholder="Add Comments"
                                name="content"
                                rows={2}
                            />
                            <Button
                                loading={isSubmitting}
                                disabled={isSubmitting || !isValid}
                                content="Add Reply"
                                labelPosition="left"
                                icon="edit"
                                primary
                                type="submit"
                            />
                        </Form>
                    )}
                </Formik>
                <Comment.Group>
                    {commentStore.comments.map((comment) => (
                        <Comment key={comment.id}>
                            <Comment.Avatar src={comment.profilePictureUrl || "/assets/user.png"} />
                            <Comment.Content>
                                <Comment.Author as={Link} to={`/profiles/${comment.username}`}>
                                    {comment.displayName}
                                </Comment.Author>
                                <Comment.Metadata
                                    content={`${formatDistanceToNow(
                                        comment.createdAt
                                    )} ago`}></Comment.Metadata>
                                <Comment.Text>{comment.content}</Comment.Text>
                            </Comment.Content>
                        </Comment>
                    ))}
                </Comment.Group>
            </Segment>
        </>
    );
}

export default observer(ActivityDetailedChat);
