export class Welcome{
    constructor() {
        this.heading = "Form";
        this.firstName = "John";
        this.lastName = "Doe";

        this.currentDate = new Date();
        this.dd = this.currentDate.getDate();
        this.MM = this.currentDate.getMonth() + 1;
        this.yyyy = this.currentDate.getFullYear();
        this.newDate = this.dd + "-" + this.MM + "-" + this.yyyy;

        this.hh = this.currentDate.getHours();
        this.mm = this.currentDate.getMinutes();
        this.newTime = this.hh + ":" + this.mm;
    }

    get fullName(){
        return `${this.firstName} ${this.lastName}`;
    }

    submit(){
        alert(`Welcome, ${this.fullName}!`);
    }
}