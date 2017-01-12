var chatapp = angular.module('chatapp', ['ngDialog', 'toaster', 'ngAnimate', 'angular-web-notification']).filter('unique', function () {

    return function (items, filterOn) {

        if (filterOn === false) {
            return items;
        }

        if ((filterOn || angular.isUndefined(filterOn)) && angular.isArray(items)) {
            var hashCheck = {}, newItems = [];

            var extractValueToCompare = function (item) {
                if (angular.isObject(item) && angular.isString(filterOn)) {
                    return item[filterOn];
                } else {
                    return item;
                }
            };

            angular.forEach(items, function (item) {
                var valueToCheck, isDuplicate = false;

                for (var i = 0; i < newItems.length; i++) {
                    if (angular.equals(extractValueToCompare(newItems[i]), extractValueToCompare(item))) {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate) {
                    newItems.push(item);
                }

            });
            items = newItems;
        }
        return items;
    };
})
//.animation('.slide', function () {
//    var NG_HIDE_CLASS = 'ng-hide';
//    return {
//        beforeAddClass: function (element, className, done) {
//            if (className === NG_HIDE_CLASS) {
//                element.slideUp(done);
//            }
//        },
//        removeClass: function (element, className, done) {
//            if (className === NG_HIDE_CLASS) {
//                element.hide().slideDown(done);
//            }
//        }
//    }
//});
chatapp.controller('ChatController', ChatController);
chatapp.directive('ShowNotification', ShowNotification);
//chatapp.directive('ngSlideDown', ngSlideDown);

