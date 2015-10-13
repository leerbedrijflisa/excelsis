import {HttpClient} from 'aurelia-http-client';

export class Start{

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
        if (Number.isInteger(parseInt(params.assessmentid))) {
            this.getAssessment(params.assessmentid);            
        }      
    }

    getAssessment(id){
        this.http.get("assessments/"+id).then(response => {
            this.assessment = response.content; 
            this.name = this.assessment.student.name;
            this.number = this.assessment.student.number;
            // dd - mm - yyyy
            this.newDate = this.assessment.assessed.date;
            // HH-mm
            this.newTime = this.assessment.assessed.time;
        });
    }   
}