import { action, makeObservable, observable } from "mobx"

export default class ActivityStore {
    title = "Welcome from MobX"

    constructor() {
        makeObservable(this, {
            title: observable,
            setTitle: action
        })        
    }

    setTitle = () => {
        this.title = this.title + "!";
    }
}