import {HttpClient} from 'aurelia-http-client';

export class Welcome{
    constructor() {
        this.exam = {
            "subject": "Nederlands",
            "name": "Gesprekken voeren"
        }

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

    activate() {
        this.heading = "Exam";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/');      
            x.withHeader('Content-Type', 'application/json')});
    }

    storeStudent() {
        alert(`Welcome, ${this.name} The date is ${this.newDate} and the time is ${this.newTime}!`);

        this.subject = "Nederlands";
        this.examName = "Schrijven";
        this.cohort = "2083";

        var Content = {
            "student": {
                "name": this.name,
                "number": this.number
            },
            "assessor": [{
                "userName": "joostronkesagerbeek"
            }],
            "assessed": this.newDate+"T"+this.newTime+":00Z"
        };

        this.http.post("assessments/"+this.subject+"/"+this.examName+"/"+this.cohort, Content).then(response => {
        //this.http.post("assessments/nederlands/schrijven/2015", Content).then(response => {
            this.assessment = response.content;
            alert(this.assessment);
        });
    }
}