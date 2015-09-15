export class Welcome{
    constructor() {
        this.heading = "Form";
        this.firstName = "John";
        this.lastName = "Doe";
        this.currentDate = new Date();
        //this.day = currentDate.getDay;
        //this.month = currentDate.getMonth;
        //this.year = currentDate.getYear;
        //this.hours= currentDate.getHours;
        //this.minutes = currentDate.getMinutes;
    }

    get fullName(){
        return `${this.firstName} ${this.lastName}`;
    }

    submit(){
        alert(`Welcome, ${this.fullName}!`);
    }
}