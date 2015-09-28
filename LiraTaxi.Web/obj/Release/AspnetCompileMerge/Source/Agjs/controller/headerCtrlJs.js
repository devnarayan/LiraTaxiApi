
(function () {
    'use strict';
    var controllerId = 'HeaderController';
    angular.module('app').controller(controllerId,
        ['$scope', '$http', '$q', 'LanguageService', HeaderController]);
    function HeaderController($scope,$http, $q, LanguageService) {
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var lid = LanguageService.GetLanguageShort();
        $scope.LanguageShort = lid;
        var vm = this;
        // Bindable properties and functions are placed on vm.
        $scope.ChangeLanguage = function (LanguageID) {
            localStorage.setItem("Lang", LanguageID);
            var basePath = localStorage.getItem("basePath");
            $scope.LanguageID = LanguageID;
            var path = window.location.pathname;
            path = path.replace(basePath, "");
            var lang = path.substr(0, 3);
            if (lang.toLowerCase() == "/ch" && LanguageID == 3) {
                path = path.replace("/Ch", "");
                path = basePath + "/En" + path;
                window.location = path;
            }
            else if (lang.toLowerCase() == "/en" && LanguageID == 4) {
                path = path.replace("/En", "");
                path = basePath + "/Ch" + path;
                window.location = path;
            }
        }

    }
})();


(function () {
    'use strict';
    var controllerId = 'FooterController';
    angular.module('app').controller(controllerId,
        ['LanguageService', FooterController]);
    function FooterController(LanguageService) {
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var vm = this;
        // Bindable properties and functions are placed on vm.
        vm.title = 'mainCtrl';
        vm.isRegistered = false;
        vm.isLoggedIn = false;
        vm.userData = {
            id: 0,
            email: "",
            userName: "",
            password: "",
            confirmPassword: "",
        };
        vm.postData = {
            value: "",
        };
       
    }
})();