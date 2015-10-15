import {HttpClient} from 'aurelia-http-client';

export class List
{
    activate() {

        this.heading = "Assessments";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858');      
            x.withHeader('Content-Type', 'application/json')});

        this.http.get("/assessments/").then(response => {
            this.assessments = response.content;
        }, response => {
            if(response.statusCode == 404){
                this.error = "Helaas er zijn geen beoordelingen gevonden.";
            }
        });
    }
}