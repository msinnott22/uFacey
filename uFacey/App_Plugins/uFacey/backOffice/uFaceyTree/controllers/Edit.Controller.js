angular.module("umbraco").controller("uFacey.EditController",
    function($scope, $routeParams) {
        var viewName = $routeParams.id;
        viewName = viewName.replace('%20', '-').replace(' ', '-');

        $scope.templatePartialURL = '../App_Plugins/uFacey/backOffice/uFaceyTree/partials/' + viewName + '.html';
        $scope.sectionName = $routeParams.id;
    });