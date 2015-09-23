import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class App {
    configureRouter(config, router) {
        config.title = "Excelsis";
        config.map([
            { route: 'assessment', name: 'assessment', moduleId: 'assessment' },
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam' }
        ]);
    }
}