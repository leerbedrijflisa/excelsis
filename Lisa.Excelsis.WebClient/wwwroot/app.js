import {inject} from 'aurelia-framework';
//import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';

//@inject(Router, HttpClient)
export class App{

    //private http: HttpClient = null;

    configureRouter(config, router){
        config.title = "Excelsis";
        config.map([
            { route: 'assessment', name: 'assessment', moduleId: 'assessment', nav: true },
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam', nav: true }
        ]);

        this.router = router;
    }

    constructor(){

        this.http = new HttpClient().configure(x => {
            x.withBaseUrl('http://localhost:5858/api');      
            x.withHeader('accept', 'application/json')
        });
    }
}