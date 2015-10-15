import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';

export class Welcome
{
    static inject() {
        return [ Router, HttpClient ];
    }

    constructor(router, http){
        this.router = router;
        this.http = http;
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
            this.error = null;
            document.getElementById("exams").style.display = "";
        }, response => {
            if(response.statusCode == 404){
                this.error = "Helaas er zijn geen examens gevonden.";
                document.getElementById("exams").style.display = "none";
            }
        });
    }

    startAssessment(name, subject, cohort) {
        this.router.navigateToRoute('assessment', { subject: subject, name: name,  cohort: cohort });
    }
}