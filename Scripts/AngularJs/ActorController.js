var ActorController = angular.module("ActorController", []);
ActorController.controller("ActorListController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.title = "Actor List";
        $http.get('/api/ActorApi').then(function (data) {
            $scope.ActorList = data.data;
        });

        $scope.Activate = function (id) {
            if (confirm('Are you sure to deactivate this actor')) {
                $http.delete('api/ActorApi/' + id).then(function (data) {
                    $http.get('/api/ActorApi').then(function (data) {
                        $scope.ActorList = data.data;
                    });
                });
            }
        }
    }
]);


ActorController.controller("EditActorController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
        $scope.save = function () {
            var obj = {
                ActorId: $scope.ActorId,
                Name: $scope.Name,
                Sex: $scope.Sex,
                DOB: $scope.DOB,
                Bio: $scope.Bio,
                Active: true
            };
            if ($scope.ActorId == 0 || $scope.ActorId == undefined) {
                $http.post('/api/ActorApi/', obj).then(function (data) {
                    $location.path('/actorlist');
                }).catch(function (data) {
                    $scope.error = "An error has occured while adding actor! " + data.ExceptionMessage;
                });
            }
            else {
                $http.post('/api/ActorApi/', obj).then(function (data) {
                    $location.path('/actorlist');
                }).catch(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving Actor! " + data.ExceptionMessage;
                });
            }
        }
        if ($routeParams.id) {
            $scope.id = $routeParams.id;
            $scope.title = "Edit Actor";
            $http.get('/api/ActorApi/' + $routeParams.id).then(function (data) {
                $scope.ActorId = data.data.ActorId;
                $scope.Name = data.data.Name;
                $scope.DOB = data.data.DOB;
                $scope.Sex = data.data.Sex;
                $scope.Bio = data.data.Bio;
                $scope.Active = data.data.Active;
            });
        }
        else {
            $scope.title = "Create New Actor";
        }
    }
]);