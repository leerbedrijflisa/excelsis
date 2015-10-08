export class App {
    configureRouter(config, router) {
        config.title = "Excelsis";
        config.map([
            { route: 'assessment', name: 'assessment', moduleId: 'assessment' },
            { route: 'assessment/:urlId', name: 'assessmentId', moduleId: 'assessment' },
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam' },
            { route: 'version', name: 'version', moduleId: 'version'}
        ]);
    }
}