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
            "assessors": [{
                "userName": "joostronkesagerbeek"
            }],
            "assessed": formatDate(this.newDate, this.newTime)
        };

        this.http.post("assessments/"+this.subject+"/"+this.examName+"/"+this.cohort, Content).then(response => {
        //this.http.post("assessments/nederlands/schrijven/2015", Content).then(response => {
            this.assessment = response.content;
            alert(this.assessment);
        });

        function formatDate(date, time){

            var splitDate = date.split("-");
            var splitTime = time.split(":");

            //If the month is below 10 it adds a 0 up front.
            if(splitDate[1] < 10){
                splitDate[1] = "0"+splitDate[1];
            }

            //If the day is below 10 it adds a 0 up front.
            if(splitDate[0] < 10){
                splitDate[0] = "0"+splitDate[0];
            }

            //If the hours are below 10 it adds a 0 up front.
            if(splitTime[0] < 10){
                splitTime[0] = "0"+splitTime[0];
            }

            //If the minutes are below 10 it adds a 0 up front.
            if(splitTime[1] < 10){
                splitTime[1] = "0"+splitTime[1];
            }

            return splitDate[2]+"-"+splitDate[1]+"-"+splitDate[0]+"T"+splitTime[0]+":"+splitTime[1]+":00Z"
        }
    }
}