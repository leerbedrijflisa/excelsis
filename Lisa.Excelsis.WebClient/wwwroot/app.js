export class App {
    configureRouter(config, router) {
        config.title = "Excelsis";
        config.map([
            { route: 'assessment', name: 'assessment', moduleId: 'assessment' },
            { route: ['', 'exam'], name: 'exam', moduleId: 'exam' }
        ]);
    }
}