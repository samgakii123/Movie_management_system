var ProducerController = angular.module("ProducerController", []);
ActorController.controller("ProducerListController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.title = "Producer List";
        $http.get('/api/ProducerApi').then(function (data) {
            $scope.ProducerList = data.data;
        });

        $scope.Activate = function (id) {
            if (confirm('Are you sure to deactivate this producer')) {
                $http.delete('api/ProducerApi/' + id).then(function (data) {
                    $http.get('/api/ProducerApi').then(function (data) {
                        $scope.ProducerList = data.data;
                    });
                });
            }
        }
    }
]);
ProducerController.controller("EditProducerController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {
        $scope.save = function () {
            debugger;
            var obj = {
                ProducerId: $scope.ProducerId,
                Name: $scope.Name,
                Sex: $scope.Sex,
                DOB: $scope.DOB,
                Bio: $scope.Bio,
                Active: true
            };
            if ($scope.ActorId == 0 || $scope.ActorId == undefined) {
                $http.post('/api/ProducerApi/', obj).then(function (data) {
                    $location.path('/ProducerList');
                }).catch(function (data) {
                    $scope.error = "An error has occured while adding Producer! " + data.ExceptionMessage;
                });
            }
            else {
                $http.post('/api/ProducerApi/', obj).then(function (data) {
                    $location.path('/ProducerList');
                }).catch(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving Producer! " + data.ExceptionMessage;
                });
            }
        }
        if ($routeParams.id) {
            debugger;
            $scope.id = $routeParams.id;
            $scope.title = "Edit Producer";
            $http.get('/api/ProducerApi/' + $routeParams.id).then(function (data) {
                $scope.ProducerId = data.data.ProducerId;
                $scope.Name = data.data.Name;
                $scope.DOB = data.data.DOB;
                $scope.Sex = data.data.Sex;
                $scope.Bio = data.data.Bio;
                $scope.Active = data.data.Active;
            });
        }
        else {
            $scope.title = "Create New Producer";
        }
    }
]);