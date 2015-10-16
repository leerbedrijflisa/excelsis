import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';

export class Exam{
    
    static inject() {
        return [ Router, HttpClient ];
    }

    constructor(router, http){
        this.router = router;
        this.http = http;
    }

    activate() {
        this.message = "Een moment alstublieft...";
        this.heading = "Exam"; 
        this.http.get("/subjects?assessor=joostronkesagerbeek").then(response => {
            this.subjects = response.content;
            this.message = null;
        });        
        this.cohorts = [ "2015", "2014", "2013", "2012" ];
    }

    showExams() {       
        var subject = document.getElementById('subject').value;
        var cohort = document.getElementById('cohort').value;
        this.http.get("/exams/"+subject+"/"+cohort).then(response => {
            this.exams = response.content;
            this.message = null;
            document.getElementById("exams").style.display = "inline";
        }, response => {
            if(response.statusCode == 404){
                this.message = "Helaas er zijn geen examens gevonden.";
                document.getElementById("exams").style.display = "none";
            }
        });
    }

    startAssessment(name, subject, cohort) {
        this.router.navigateToRoute('assessment', { subject: subject, name: name,  cohort: cohort });
    }
}
