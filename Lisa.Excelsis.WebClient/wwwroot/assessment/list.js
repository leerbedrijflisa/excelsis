import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';

export class List
{
    static inject() {
        return [ Router, HttpClient ];
    }

    constructor(router, http){
        this.http = http;
        this.router = router;
    }

    activate() {
        this.message = "Studenten worden geladen. Een moment geduld astublieft...";

        this.heading = "Assessments";
       
        this.http.get("/students/").then(response => {
            this.students = response.content;
            this.message = null;
        }, response => {
            if(response.statusCode == 404){
                this.message = "Helaas er zijn geen studenten gevonden.";
            }
        });
    }
    
    showAssessments(student){
        var Student = document.getElementById('student').value;
        var url = (student == "student")?  "/assessments/student/"+Student : "/assessments";
        this.http.get(url).then(response => {
            this.assessments = response.content;
            this.message = null;
            document.getElementById("assessments").style.display = "inline";
        }, response => {
            if(response.statusCode == 404){
                this.message = "Helaas er zijn geen assessments gevonden.";
                document.getElementById("assessments").style.display = "none";
            }
        });
    }
    openAssessment(name, subject, cohort, id) {
        this.router.navigateToRoute('assessmentId', { subject: subject, name: name,  cohort: cohort, assessmentid: id });
    }
}