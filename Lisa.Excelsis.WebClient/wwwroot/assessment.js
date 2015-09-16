export class Welcome{
    constructor() {
        this.heading = "Form";
        this.firstName;
        this.lastName;
        this.studentNumber;

        this.currentDate = new Date();
        this.dd = this.currentDate.getDate();
        this.MM = this.currentDate.getMonth() + 1;
        this.yyyy = this.currentDate.getFullYear();
        this.newDate = this.dd + "-" + this.MM + "-" + this.yyyy;

        this.hh = this.currentDate.getHours();
        this.mm = this.currentDate.getMinutes();
        this.newTime = this.hh + ":" + this.mm;
    }


    submit(){
        alert(`Welcome, ${this.firstName} ${this.lastName} The date is ${this.newDate} and the time is ${this.newTime}!`);
  }
}