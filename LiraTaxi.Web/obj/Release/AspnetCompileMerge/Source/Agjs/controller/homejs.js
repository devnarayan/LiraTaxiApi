
(function () {
    'use strict';
    var controllerId = 'ContentController';
    angular.module('app').controller(controllerId,
        ['$scope','$http', '$q', 'LanguageService', ContentController]);
    function ContentController($scope,$http,$q,LanguageService) {
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var lid=  LanguageService.GetLanguageShort();
        $scope.LanguageShort = lid;
        var serverBaseUrl = "http://app.lirataxi.com";

        var eb = this;
        // Bindable properties and functions are placed on vm.
        eb.title = 'mainCtrl';
        eb.DriverProfileInit = DriverProfileInit;

        function DriverProfileInit() {
          //  var url = serverBaseUrl + "/api/values/Get";
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/En/Driver/GetDriverByUserName',
                params:{UserName:$("#UserName").val()},
               // headers: getHeaders(),
            }).success(function (data, status, headers, cfg) {
                console.log(data);
                eb.Profile = data;
                deferred.resolve(data);
            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });
           
        }
        function getHeaders() {
            if (accessToken) {
                return { "Authorization": "Bearer " + accessToken };
            }
        }
    }
})();