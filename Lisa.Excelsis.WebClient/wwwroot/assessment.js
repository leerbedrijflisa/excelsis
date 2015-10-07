import {HttpClient} from 'aurelia-http-client';
export class Welcome{
    constructor() {

        function doubleDigit(digit)
        {
            if(digit < 10)
            {
                digit = "0"+digit;
            }

            return digit;
        }

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

        this.dd = doubleDigit(this.dd);
        this.mm = doubleDigit(this.mm);

        this.newDate = this.dd + "-" + this.MM + "-" + this.yyyy;

        this.hh = this.currentDate.getHours();
        this.mm = this.currentDate.getMinutes();

        this.hh = doubleDigit(this.hh);
        this.mm = doubleDigit(this.mm);

        this.newTime = this.hh + ":" + this.mm;
    }

    activate() {
        this.heading = "Assessment";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/');      
            x.withHeader('Content-Type', 'application/json')});
    }

    storeStudent() {

        this.subject = "Nederlands";
        this.examName = "Schrijven";
        this.cohort = "2015";

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
            this.assessment = response.content;
        });
        
        function formatDate(date, time){

            //splits the date and the time in seperate numbers.
            var splitDate = date.split("-");
            var splitTime = time.split(":");

            //If the month is below 10 it adds a 0 up front.
            splitDate[1] = doubleDigit(splitDate[1]);

            //If the day is below 10 it adds a 0 up front.
            splitDate[0] = doubleDigit(splitDate[0])

            //If the hours are below 10 it adds a 0 up front.
            splitTime[0] = doubleDigit(splitTime[0]);

            //If the minutes are below 10 it adds a 0 up front.
            splitTime[1] = doubleDigit(splitTime[1]);

            return splitDate[2]+"-"+splitDate[1]+"-"+splitDate[0]+"T"+splitTime[0]+":"+splitTime[1]+":00Z"
        }

        //input MUST be string!
        function doubleDigit(digit)
        {
            if(digit.length < 2)
            {
                digit = "0"+digit;
            }

            return digit;
        }
    }
}