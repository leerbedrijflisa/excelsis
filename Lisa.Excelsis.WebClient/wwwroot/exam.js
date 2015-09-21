import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class Exam
{
    

    constructor() {
        this.heading = "Exam";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/api');      
            x.withHeader('Content-Type', 'application/json')});

        this.subjects = [ "Nederlands", "Applicatieontwikkelaar", "Rekenen" ];
        this.cohorts = [ "2015", "2014", "2013", "2012" ];
    }

    showExams() {

        var selectSubject = document.getElementById("selectExam");
        var subject = selectSubject.options[selectSubject.selectedIndex].value;

        var selectCohort = document.getElementById("selectCohort");
        var cohort = selectCohort.options[selectCohort.selectedIndex].value;

        return this.http.get("/Exam").then(response => {
            this.Exams = this.find_in_object(response.content, {Subject: subject, Cohort: cohort});
              console.log(response.content);
        });         
    }

    find_in_object(my_object, my_criteria){

        return my_object.filter(function(obj) {
            return Object.keys(my_criteria).every(function(c) {
                return obj[c] == my_criteria[c];
            });
        });

    }
}