var app = angular.module('app', ['ngRoute', 'toaster', 'ngAnimate']);
app.controller('RegistrationController', RegistrationController);
app.controller('LoginController', LoginController);
app.controller('CommonController', CommonController);
app.factory('RegistrationFactory', RegistrationFactory);
app.factory('LoginFactory', LoginFactory);


//var configFunction = function ($routeProvider) {
//    $routeProvider.
//   when('/register', {
//       templateUrl: '/Account/Register',
//       controller: 'RegisterController'
//   }).
//   when('/login', {
//       templateUrl: '/Account/Login',
//       controller: 'LoginController'
//   })
//}
//configFunction.$inject = ['$routeProvider'];

//app.config(configFunction);
