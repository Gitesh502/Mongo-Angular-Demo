var chatapp = angular.module('chatapp', ['ngDialog', 'toaster']);
chatapp.controller('ChatController', ChatController);
chatapp.directive('userdirective', userdirective);