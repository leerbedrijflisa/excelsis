import {HttpClient} from 'aurelia-http-client';

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

    activate() {
        this.heading = "Exam";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/');      
            x.withHeader('Content-Type', 'application/json')});
    }

    storeStudent() {
        alert(`Welcome, ${this.firstName} ${this.lastName} The date is ${this.newDate} and the time is ${this.newTime}!`);
        
        //this.http.get("/exams/1").then(response => {
        //    this.exams = response.content;  
        //    console.log(response.content);
        //});

        var Content = {
            "TeacherId": 1,
            "ExamId": 1,
            "Examinee": "Keespietjandirksenzoon-van-ders-Lagerwaard"        
        };

        this.http.post("assessments", Content).then(response => {
            this.assessment = response.content;
            alert(this.assessment);
        });
    }
}