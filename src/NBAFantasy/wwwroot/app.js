export class App {
    configureRouter(config, router) {
        config.title = 'Aurelia';
        config.map([
            { route: [''], name: 'index', moduleId: './index', nav: true, title: 'Home' }
        ]);
        this.router = router;
    }
}