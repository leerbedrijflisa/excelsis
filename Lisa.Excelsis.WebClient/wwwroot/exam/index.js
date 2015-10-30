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
        
        this.http.get("/assessors").then(response => {
            this.assessors = response.content;
            this.message = null;
        });        
        
        this.cohorts = [ "2015", "2014", "2013", "2012" ];
    }

    selectAssessor()
    {
        this.assessor = document.getElementById('assessor').value;
        this.message = "Een moment alstublieft...";
        this.http.get("/subjects?assessor="+this.assessor).then(response => {
            this.subjects = response.content;
            this.message = null;
        });
    }

    showExams() {       
        var subject = document.getElementById('subject').value;
        var cohort = document.getElementById('cohort').value;
        this.http.get("/exams/"+subject+"/"+cohort).then(response => {
            this.exams = response.content;
            this.messageExam = null;
            document.getElementById("exams").style.display = "inline";
        }, response => {
            if(response.statusCode == 404){
                this.messageExam = "Helaas er zijn geen examens gevonden.";
                document.getElementById("exams").style.display = "none";
            }
        });
    }

    startAssessment(name, subject, cohort) {
        this.router.navigateToRoute('assessment', { subject: subject, name: name,  cohort: cohort, assessor: this.assessor });
    }
}
