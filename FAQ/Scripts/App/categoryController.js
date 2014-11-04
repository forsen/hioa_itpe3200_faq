var App = angular.module("faq", []);

App.controller("controller", function ($scope, $http) {
    var categoryUrl = '/api/Category';

    $scope.showForm = false;
    function getAllCategories() {
        $http.get(categoryUrl).
        success(function (allCategories) {
            $scope.categories = allCategories;
            $scope.loading = false;
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };
    getAllCategories();

    var questionUrl = '/api/Question/';
    
    $scope.showNewQuestionForm = function () {
        $scope.question = "";
        $scope.email = "";
        $scope.newquestionform.$setPristine();
        $scope.showForm = true;
    };

    $scope.saveNewQuestion = function () {
        var question = {
            question: $scope.question,
            categoryid: $scope.category.id,
            email: $scope.email
        };

        $http.post(questionUrl, question)
            .success(function (data) {
                // noe 
                $scope.showForm = false;
                $scope.showThanks = true;

            })
            .error(function (data, status) {
                console.log(status + data);
            });
    };
});