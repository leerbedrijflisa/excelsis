export class App {
    configureRouter(config, router) {
        this.router = router;

        config.title = "Excelsis";
        config.map([
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam', nav: true, title:'Home'  },
            { route: 'assessment', name: 'assessment', moduleId: 'assessment', nav: true, title:'Assessment' },
            { route: 'assessment/:urlId', name: 'assessmentId', moduleId: 'assessment' },
            { route: 'assessments', name: 'assessments', moduleId: 'assessments', nav: true, title:'Assessments'}, 
            { route: 'version', name: 'version', moduleId: 'version', nav: true, title:'Version' }
        ]);
    }
}