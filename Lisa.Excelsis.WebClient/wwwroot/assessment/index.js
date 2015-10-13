import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';

export class Index{

    static inject() {
        return [ Router ];
    }
    constructor(router) {
        this.router = router;
        this.currentDate = new Date();

        this.newDate = doubleDigit(this.currentDate.getDate()) + 
                 "-" + doubleDigit(this.currentDate.getMonth() + 1) + 
                 "-" + this.currentDate.getFullYear();

        this.newTime = doubleDigit(this.currentDate.getHours()) + 
                 ":" + doubleDigit(this.currentDate.getMinutes());

        function doubleDigit(digit){
            if (digit.length < 2)
            { 
                digit = "0"+digit;
            }
            return digit;
        }
    }

    activate(params) {
        this.heading = "Assessment";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/');      
            x.withHeader('Content-Type', 'application/json')});
        this.exam = {
            "subject": params.subject,
            "name": params.name,
            "cohort": params.cohort
        }
    }

    startAssessment() { 
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

        this.http.post("assessments/"+this.exam.subject+"/"+this.exam.name+"/"+this.exam.cohort, Content).then(response => {
            this.assessment = response.content;
            this.router.navigateToRoute('assessmentId', {subject: this.exam.subject, name: this.exam.name, cohort: this.exam.cohort, assessmentid: this.assessment.id });
        });
        
        function formatDate(date, time){
            var splitDate = date.split("-");
            var splitTime = time.split(":");

            splitDate[1] = doubleDigit(splitDate[1]);
            splitDate[0] = doubleDigit(splitDate[0])
            splitTime[0] = doubleDigit(splitTime[0]);
            splitTime[1] = doubleDigit(splitTime[1]);

            return splitDate[2]+"-"+splitDate[1]+"-"+splitDate[0]+"T"+splitTime[0]+":"+splitTime[1]+":00Z"
        }
        function doubleDigit(digit){
            if (digit.length < 2)
            { 
                digit = "0"+digit;
            }
            return digit;
        }
    }  
}