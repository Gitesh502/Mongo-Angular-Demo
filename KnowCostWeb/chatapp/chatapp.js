var chatapp = angular.module('chatapp', ['ngDialog', 'toaster', 'ui.filters']);
chatapp.controller('ChatController', ChatController);
chatapp.directive('userdirective', userdirective);