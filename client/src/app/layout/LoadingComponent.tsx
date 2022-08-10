import { Dimmer, Loader } from "semantic-ui-react";

interface Props {
    inverted?: boolean;
    content?: string;
}

export default function LoadingComponent(props: Props) {
    return (
        <Dimmer active={true} inverted={props.inverted}>
            <Loader content={props.content} />
        </Dimmer>
    );
}
