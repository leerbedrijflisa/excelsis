export class App {
    configureRouter(config, router) {
        this.router = router;

        config.title = "Excelsis";
        config.map([
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam', nav: true, title:'Home'  },
            { route: 'assessment/:name/:subject/:cohort', name: 'assessment', moduleId: 'assessment', title:'Assessment' },
            { route: 'assessment/:urlId', name: 'assessmentId', moduleId: 'assessment' },           
            { route: 'version', name: 'version', moduleId: 'version', nav: true, title:'Version' }
        ]);
    }
}