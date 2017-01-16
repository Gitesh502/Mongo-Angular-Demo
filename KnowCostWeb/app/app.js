var app = angular.module('app', ['ngRoute', 'angularNotify', 'ngAnimate']);
app.controller('RegistrationController', RegistrationController);
app.controller('LoginController', LoginController);
app.controller('CommonController', CommonController);
app.factory('RegistrationFactory', RegistrationFactory);
app.factory('LoginFactory', LoginFactory);



