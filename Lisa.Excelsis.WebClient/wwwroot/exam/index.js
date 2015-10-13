import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';

export class Exam
{
    static inject() {
        return [ Router ];
    }
    constructor(router){
        this.router = router;
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858');      
            x.withHeader('Content-Type', 'application/json')});
    }
    activate() {
        this.heading = "Exam"; 
        this.http.get("/subjects").then(response => {
            this.subjects = response.content;            
        });        
        this.cohorts = [ "2015", "2014", "2013", "2012" ];
    }

    showExams() {
       
        var subject = document.getElementById('subject').value;
        var cohort = document.getElementById('cohort').value;
        this.http.get("/exams/"+subject+"/"+cohort).then(response => {
            this.exams = response.content;            
        });
    }

    startAssessment(name, subject, cohort) {
       this.router.navigateToRoute('assessment', {name: name, subject: subject, cohort: cohort });
    }
}