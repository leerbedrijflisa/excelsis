//export class App {
//    constructor() {
//        this.message = "Gloria In Excelsis Aurelia";
//    }

//    configureRouter(config, router) {
//        config.map([
//            { route: "date", name: "date", moduleId: "date", nav: true }
//        ]);
//    }
//}

export class App{
    configureRouter(config, router){
        config.title = "Excelsis";
        config.map([
            { route: ['', 'assessment'], name: 'assessment', moduleId: 'assessment', nav: true }           
        ]);

        this.router = router;
    }
}