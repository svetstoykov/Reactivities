import { makeObservable, observable, observe } from "mobx"

export default class ActivityStore {
    title = "Welcome from MobX"

    constructor() {
        makeObservable(this, {
            title: observable
        })        
    }
}