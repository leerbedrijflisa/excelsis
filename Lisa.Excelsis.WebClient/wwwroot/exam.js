import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class Exam
{
    constructor() {
        this.heading = "Exam";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858');      
            x.withHeader('Content-Type', 'application/json')});

        this.subjects = [ "Nederlands", "Applicatieontwikkelaar", "Rekenen" ];
        this.cohorts = [ "2015", "2014", "2013", "2012" ];
    }

    showExams() {
        
        var selectSubject = document.getElementById("selectExam");
        var subject = selectSubject.options[selectSubject.selectedIndex].value;

        var selectCohort = document.getElementById("selectCohort");
        var cohort = selectCohort.options[selectCohort.selectedIndex].value;     
        
        this.http.get("/exams").then(response => {
            this.exams = this.findInObject(response.content, {subject: subject, cohort: cohort});
              console.log(response.content);
        });
    }

    findInObject(myObject, myCriteria){

        return myObject.filter(function(obj) {
            return Object.keys(myCriteria).every(function(c) {
                return obj[c] == myCriteria[c];
            });
        });

    }
}