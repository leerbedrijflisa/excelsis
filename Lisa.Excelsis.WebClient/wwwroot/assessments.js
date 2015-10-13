import {HttpClient} from 'aurelia-http-client';

export class Assessments
{
    activate() {

        this.heading = "Assessments";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858');      
            x.withHeader('Content-Type', 'application/json')});

        this.http.get("/assessments/").then(response => {
            this.assessments = response.content;
        });
    }
}