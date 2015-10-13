export class App {
    configureRouter(config, router) {
        this.router = router;

        config.title = "Excelsis";
        config.map([
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam/index', nav: true, title:'Home' },
            { route: 'assessment/:name/:subject/:cohort', name: 'assessment', moduleId: 'assessment/index' },
            { route: 'assessment/:urlId', name: 'assessmentId', moduleId: 'assessment/start' },           
            { route: 'assessments', name: 'assessments', moduleId: 'assessment/list', nav: true, title:'Assessments' }, 
            { route: 'version', name: 'version', moduleId: 'version', nav: true, title:'Version' }
        ]);
    }
}