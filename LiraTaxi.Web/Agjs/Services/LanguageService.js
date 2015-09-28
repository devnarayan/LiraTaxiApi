(function () {
    'use strict';
    var serviceid = 'LanguageService';
    angular.module('app').factory(serviceid, ['$http', '$q', LanguageService]);
    function LanguageService($http, $q) {
        var Service = {
            GetLanguageShort: GetLanguageShort,
        }
        return Service;
        function GetLanguageShort() {
            var LanguageID = localStorage.getItem("Lang");
            if (LanguageID == null) {
                LanguageID = 3;// $('#DDlLanguage').checked;
                // localStorage.setItem("Lang", 3);
            }
          //  $scope.LanguageID = LanguageID;
            if (LanguageID == 3) {
             
                localStorage.setItem("Lang", 3);
                // $scope.LanguageShort = "En";
                return "En";
            }
            else {
                //$scope.LanguageShort = "Ch";
                localStorage.setItem("Lang", 4);
                return "Ch";
            }
           // return $scope.LanguageShort;
        }
    }
})();
