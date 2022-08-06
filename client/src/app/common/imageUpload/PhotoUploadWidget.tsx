import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Button, Grid, Header, Item } from "semantic-ui-react";
import { useStore } from "../../stores/store";
import PhotoWidgetCropper from "./PhotoWidgetCropper";
import PhotoWidgetDropzone from "./PhotoWidgetDropzone";

function PhotoUploadWidget() {
    const [files, setFiles] = useState<any>([]);
    const [cropper, setCropper] = useState<Cropper>();
    const { profileStore, modalStore: {closeModal}} = useStore();

    function onCrop() {
        if (cropper) {
            cropper.getCroppedCanvas().toBlob((blob) => {
                profileStore.uploadProfilePicture(blob!).then(() => closeModal());
            });
        }
    }

    function handleDeleteProfilePicture() {
        profileStore.deleteProfilePicture().then(() => closeModal());
    }

    useEffect(() => {
        return () => {
            files.forEach((file: any) => URL.revokeObjectURL(file.preview));
        };
    }, [files]);

    return (
        <Grid columns={15} divided padded="horizontally" textAlign="center">
            <Grid.Row>
                <Grid.Column width={5}>
                    <Header sub color="teal" content="Step 1 - Add Picture"></Header>
                    <PhotoWidgetDropzone setFiles={setFiles} />
                </Grid.Column>
                <Grid.Column width={5}>
                    <Header sub color="teal" content="Step 2 - Resize image" />
                    {files && files.length > 0 && (
                        <PhotoWidgetCropper
                            setCropper={setCropper}
                            imagePreview={files[0].preview}
                        />
                    )}
                </Grid.Column>
                <Grid.Column width={5}>
                    <Header sub color="teal" content="Step 3 - Preview & Upload" />
                    {files && files.length > 0 && (
                        <Item
                            className="img-preview"
                            style={{ minHeight: 200, marginLeft: "4.5em", overflow: "hidden" }}
                        />
                    )}
                </Grid.Column>
            </Grid.Row>
            {!files.length && profileStore.currentProfile!.pictureUrl && (
                <>
                    <Grid.Row>
                        <Grid.Column width={5}></Grid.Column>
                        <Grid.Column width={5}>
                            <Button
                                fluid
                                negative
                                loading={profileStore.loading}
                                onClick={handleDeleteProfilePicture}>
                                Delete current profile picture
                            </Button>
                        </Grid.Column>
                        <Grid.Column width={5}></Grid.Column>
                    </Grid.Row>
                </>
            )}
            {files && files.length > 0 && (
                <>
                    <Grid.Row>
                        <Grid.Column width={5}></Grid.Column>
                        <Grid.Column width={5}>
                            <Button.Group fluid>
                                <Button disabled={profileStore.loading} onClick={() => setFiles([])}>
                                    Cancel
                                </Button>
                                <Button.Or />
                                <Button loading={profileStore.loading} onClick={onCrop} positive>
                                    Save
                                </Button>
                            </Button.Group>
                        </Grid.Column>
                        <Grid.Column width={5}></Grid.Column>
                    </Grid.Row>
                </>
            )}
        </Grid>
    );
}

export default observer(PhotoUploadWidget);
