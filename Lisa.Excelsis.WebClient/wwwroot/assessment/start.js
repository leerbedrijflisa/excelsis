import {HttpClient} from 'aurelia-http-client';
import {Utils} from "utils";

export class Start{

    static inject() {
        return [ Utils, HttpClient ];
    }

    constructor(utils, http) {
        this.utils = utils;
        this.http = http;
    }

    activate(params) {
        this.heading = "Assessment";
       
        if(params.subject != null){
            this.exam = {
                "subject": params.subject,
                "name": params.name,
                "cohort": params.cohort
            }
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
            this.newDate = this.assessment.assessed.date;
            this.newTime = this.assessment.assessed.time;
        });
    }   
}