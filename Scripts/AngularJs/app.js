var MovieApp = angular.module('MovieApp', ['ngRoute', 'moment-picker', 'MovieController', 'ActorController', 'ProducerController']);

MovieApp.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
                  
            element.bind('change', function(){
                scope.$apply(function(){
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);
      
MovieApp.service('fileUpload', ['$http', function ($http) {
   this.uploadFileToUrl = function(file, uploadUrl){
       var fd = new FormData();
       fd.append('file', file);
       $http.post(uploadUrl, fd, {
           transformRequest: angular.identity,
           headers: {'Content-Type': undefined}
       })
       .then(function(){
       })
       .then(function(){
       });
   }
}]);

MovieApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/list',
    {
        templateUrl: 'Movie/MovieList.html',
        controller: 'ListController'
    }).
    when('/create',
    {
        templateUrl: 'Movie/AddMovie.html',
        controller: 'EditController'
        
    }).
    when('/edit/:id',
    {
        templateUrl: 'Movie/AddMovie.html',
        controller: 'EditController'
    }).
    when('/actorlist',
    {
        templateUrl: 'Movie/ActorList.html',
        controller: 'ActorListController'
    }).
    when('/AddActor',
    {
        templateUrl: 'Movie/AddActor.html',
        controller: 'EditActorController'
    }).
    when('/actoredit/:id',
    {
        templateUrl: 'Movie/AddActor.html',
        controller: 'EditActorController'
    }).
    when('/ProducerList',
    {
        templateUrl: 'Movie/ProducerList.html',
        controller: 'ProducerListController'
    }).
    when('/AddProducer',
    {
        templateUrl: 'Movie/AddProducer.html',
        controller: 'EditProducerController'
    }).
    when('/produceredit/:id',
    {
        templateUrl: 'Movie/AddProducer.html',
        controller: 'EditProducerController'
    }).
    otherwise(
    {
        redirectTo: '/list'
    });
}]);

