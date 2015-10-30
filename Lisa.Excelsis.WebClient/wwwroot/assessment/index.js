import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';
import {Utils} from "utils";

export class Index{

    static inject() {
        return [ Router, Utils, HttpClient ];
    }

    constructor(router, utils, http) {
        this.router = router;
        this.currentDate = new Date();
        this.utils = utils;
        this.http = http;

        this.newDate = this.utils.doubleDigit(this.currentDate.getDate()) + 
                 "/" + this.utils.doubleDigit(this.currentDate.getMonth() + 1) + 
                 "/" + this.currentDate.getFullYear();

        this.newTime = this.utils.doubleDigit(this.currentDate.getHours()) + 
                 ":" + this.utils.doubleDigit(this.currentDate.getMinutes());       
    }

    activate(params) {
        this.heading = "Assessment";
       
        this.exam = {
            "subject": params.subject,
            "name": params.name,
            "cohort": params.cohort
        }

        this.assessor = params.assessor;
    }

    startAssessment() { 
        var Content = {
            "student": {
                "name": this.name,
                "number": this.number
            },
            "assessors": [{
                "userName": this.assessor
            }],
            "assessed" : this.utils.formatDate(this.newDate, this.newTime)       
        };

        this.http.post("assessments/"+this.exam.subject+"/"+this.exam.name+"/"+this.exam.cohort, Content).then(response => {
            this.assessment = response.content;
            this.router.navigateToRoute('assessmentId', {subject: this.exam.subject, name: this.exam.name, cohort: this.exam.cohort, assessmentid: this.assessment.id });
        });
    }  
}