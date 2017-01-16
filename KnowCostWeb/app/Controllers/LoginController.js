var LoginController = function ($scope, LoginFactory, $window,$controller) {
    $controller('CommonController', { $scope: $scope, $window: $window })
    $scope.UserLoginDetails = {
        Email: "",
        Password: "",
        RememberMe: false,
        returnUrl:""
    }

    $scope.SignIn = function () {
        var result = LoginFactory($scope.UserLoginDetails);
        result.then(function (result) {
            console.log(result)
            
            if (result.success) {
              
                $window.location.href = '/Chat/Home/Index';

            } else {
                switch (result.Id) {
                    case 2:
                        var notify = {
                            type: 'error',
                            title: 'Error',
                            content: 'Your account is lockedout please contact administrator',
                            timeout: 5000 //time in ms
                        };
                        $scope.$emit('notify', notify);
                        break;
                    case 3:
                        var notify = {
                            type: 'error',
                            title: 'Error',
                            content: 'Your account requires TwoFactorAuthentication please contact administrator',
                            timeout: 5000 //time in ms
                        };
                        $scope.$emit('notify', notify);
                        break;
                    case 0:
                        var notify = {
                            type: 'error',
                            title: 'Error',
                            content: 'Invalid username or password',
                            timeout: 5000 //time in ms
                         };
                         $scope.$emit('notify', notify);
                        break;
                    default:
                       var  notify = {
                            type: 'error',
                            title: 'Error',
                            content: 'Invalid login attempt.',
                            timeout: 5000 //time in ms
                       };
                       $scope.$emit('notify', notify);
                        break;
                      
                }
            }
        })
    }
}
LoginController.$inject = ['$scope', 'LoginFactory', '$window', '$controller'];