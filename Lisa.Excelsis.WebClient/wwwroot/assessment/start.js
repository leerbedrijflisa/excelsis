import {HttpClient} from 'aurelia-http-client';
import {Utils} from "utils";

export class Start{

    static inject() {
        return [ Utils, HttpClient ];
    }

    constructor(utils, http) {
        this.utils = utils;
        this.http = http;
        this.done = false;
        this.notDone = false;
        this.rated = false;

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

    criteriumAnswerButton(name){
        switch(name) {
            case "done":
                if(this.done){
                    this.done = false;
                    this.rated = false;
                }else{
                    this.done = true;
                    this.notDone = false;
                    this.rated = true;
                }
                break;
            case "notDone":
                if(this.notDone){
                    this.notDone = false;
                    this.rated = false;
                }else{
                    this.notDone = true;
                    this.done = false;
                    this.rated = true;
                }
                break;
        }   
    }
}