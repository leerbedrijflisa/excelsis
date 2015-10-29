import {HttpClient} from 'aurelia-http-client';

export class List
{
    static inject() {
        return [ HttpClient ];
    }

    constructor(http){
        this.http = http;
    }

    activate() {
        this.message = "Beoordelingen worden geladen. Een moment geduld astublieft...";

        this.heading = "Assessments";
       
        this.http.get("/assessments/").then(response => {
            this.assessments = response.content;
            this.message = null;
        }, response => {
            if(response.statusCode == 404){
                this.message = "Helaas er zijn geen beoordelingen gevonden.";
            }
        });
    }
}