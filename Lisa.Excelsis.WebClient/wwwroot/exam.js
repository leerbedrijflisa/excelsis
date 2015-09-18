import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class Exam
{
    

    constructor() {
        this.heading = "Exam";
        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/api');      
            x.withHeader('Content-Type', 'application/json')});
    }

    //constructor() {

    //    
    //    this.firstName;
    //    this.lastName;
    //    this.studentNumber;
    //}

    activate() {
        return this.http.get("/Exam").then(response => {
              this.Exams = response.content;
              console.log(response.content);
          });
    }

    //Mee bezig :

    //activate(params) {
    //    return promise.all([
    //        this.http.get('/api/jobs/' + params.id).then(http => {
    //            this.job = json.parse(http.response);
    //        }),
    //        this.http.get('/api/employees').then(http => {
    //            this.employees = json.parse(http.response);
    //        })
    //    ]);
    //}

    
    //import {HttpClient} from 'aurelia-http-client';

    //@inject(HttpClient)
    //export class Contact {
    //    searchEntry = '';
    //    contacts = [];
    //    contactId = '';
    //    contact = '';
    //    currentPage = 1;
    //    textShowAll = 'Show All';

    //    constructor(http) {
    //        this.http = http;
    //    }

    //    updateContacts() {
    //        return this.http.createRequest("/contacts/?page=" + this.currentPage + "&pageSize=100&query=" + this.searchEntry)
    //          .asGet().send().then(response => {
    //              this.contacts = response.content.contacts;
    //          });
    //    }

    //    get canSearch() {
    //        return (this.searchEntry != '' ? true : false);
    //    }

    //    activate() {
    //        return this.updateContacts();
    //    }

    //    displayAllContacts() {
    //        this.searchEntry = '';
    //        this.activate();
    //    }
    //}
}