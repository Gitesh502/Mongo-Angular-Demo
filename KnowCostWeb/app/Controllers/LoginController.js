var LoginController = function ($scope, LoginFactory, $window, toaster,$controller) {
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
                $window.location.href = '/Chat/Home/Index' ;

            } else {
                switch (result.Id) {
                    case 2:
                        toaster.pop('danger', "Error", "Your account is lockedout please contact administrator");
                        break;
                    case 3:
                        toaster.pop('danger', "Error", "Your account requires TwoFactorAuthentication please contact administrator");
                        break;
                    case 0:
                        toaster.pop({type: 'error',title:  "Error",body: "Invalid username or password"});
                        break;
                    default:
                        toaster.pop('danger', "Error", "Invalid login attempt.");
                        break;

                }
            }
        })
    }
}
LoginController.$inject = ['$scope', 'LoginFactory', '$window', 'toaster', '$controller'];