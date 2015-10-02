import {HttpClient} from 'aurelia-http-client';

export class Exam
{
    activate() {
        this.heading = "Exam";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858');      
            x.withHeader('Content-Type', 'application/json')});

        this.subjects = [ "Nederlands", "Applicatieontwikkelaar", "Rekenen" ];
        this.cohorts = [ "2015", "2014", "2013", "2012" ];
    }

    showExams() {
       
        var subject = document.getElementById('subject').value;
        var cohort = document.getElementById('cohort').value;
        this.http.get("/exams/"+subject+"/"+cohort).then(response => {
            this.exams = response.content;
            
        });
    }
}