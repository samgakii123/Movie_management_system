var MovieController = angular.module("MovieController", []);
// this controller call the api method and display the list of employees  
// in list.html  
MovieController.controller("ListController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.title = "Movie List";
        $http.get('/api/Movie').then(function (data) {
            $scope.MovieList = data.data;
        });

        $scope.Activate = function (id) {
            if (confirm('Are you sure to deactivate this movie')) {
                $http.delete('api/Movie/' + id).then(function (data) {
                    $http.get('/api/Movie').then(function (data) {
                        $scope.MovieList = data.data;
                    });
                });
            }
        }
    }
]);

// this controller call the api method and display the record of selected employee  
// in edit.html and provide an option for create and modify the employee and save the employee record  
MovieController.controller("EditController", ['$scope', 'fileUpload', '$filter', '$http', '$routeParams', '$location',
    function ($scope,fileUpload, $filter, $http, $routeParams, $location) {

        $http.get('/api/ProducerApi').then(function (data) {
            $scope.producers = data.data;
        });

        $http.get('/api/ActorApi/').then(function (data) {
            $scope.actors = data.data;
        }).catch(function (ex) {

        });
        
        $scope.save = function () {
            debugger;

            var file = $scope.Poster;
            if (file) {
                var reader = new FileReader();
                reader.readAsDataURL(file);
                $scope.Poster = reader.result;
            }

            var obj = {
                MovieId: $scope.MovieId,
                MovieName: $scope.MovieName,
                ActorId:$scope.ActorId,
                ReleaseYear: $scope.ReleaseYear,
                Plot: $scope.Plot,
                Poster: reader.result,
                ProducerId: $scope.ProducerId,
                Active: true
            };
            if ($scope.MovieId == 0 || $scope.MovieId== undefined) {
                $http.post('/api/Movie/', obj).then(function (data) {
                    $location.path('/list');
                }).catch(function (data) {
                    $scope.error = "An error has occured while adding employee! " + data.ExceptionMessage;
                });
            }
            else {
                $http.post('/api/Movie/', obj).then(function (data) {
                    $location.path('/list');
                }).catch(function (data) {
                    console.log(data);
                    $scope.error = "An Error has occured while Saving customer! " + data.ExceptionMessage;
                });
            }
        }
        if ($routeParams.id) {
            debugger;
            $scope.id = $routeParams.id;
            $scope.title = "Edit Movie";
            $http.get('/api/Movie/' + $routeParams.id).then(function (data) {
                $scope.MovieId = data.data.MovieId;
                $scope.ActorId = data.data.ActorId.split('|');
                $scope.MovieName = data.data.MovieName;
                $scope.ReleaseYear = data.data.ReleaseYear;
                $scope.Plot = data.data.Plot;
                $scope.Poster = data.data.Poster;
                $scope.Active = data.data.Active;
                $scope.ProducerId = data.data.ProducerId;
            });
        }
        else {
            $scope.title = "Create New Movie";
        }
    }
]);