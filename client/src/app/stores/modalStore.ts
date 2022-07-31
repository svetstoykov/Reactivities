import { makeAutoObservable } from "mobx";

interface Modal {
    open: boolean;
    body: JSX.Element | null;
    isLarge: boolean;
}

export default class ModalStore {
    modal: Modal = {
        open: false,
        body: null,
        isLarge: false,
    };

    constructor() {
        makeAutoObservable(this);
    }

    openModal = (content: JSX.Element, isLarge: boolean = false) => {
        this.modal.open = true;
        this.modal.body = content;
        this.modal.isLarge = isLarge;
    };

    closeModal = () => {
        this.modal.open = false;
        this.modal.body = null;
    };
}
