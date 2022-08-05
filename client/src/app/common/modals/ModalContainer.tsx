import { useStore } from "../../stores/store";
import { Modal, ModalContent } from "semantic-ui-react";
import { observer } from "mobx-react-lite";

function ModalContainer() {
    const { modalStore } = useStore();
    return (
        <Modal
            open={modalStore.modal.open}
            onClose={modalStore.closeModal}
            size={modalStore.modal.isLarge ? "large" : "mini" }>
            <ModalContent>{modalStore.modal.body}</ModalContent>
        </Modal>
    );
}

export default observer(ModalContainer);
