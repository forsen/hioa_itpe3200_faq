var App = angular.module("faq", []);

App.controller("controller", function ($scope, $http) {
    var categoryUrl = '/api/Category/';
    var questionUrl = '/api/Question/';
    var questionByCatUrl = '/api/Question/cat/';
    hideAll();
    function getAllCategories() {
        $http.get(categoryUrl).
        success(function (allCategories) {
            $scope.categories = allCategories;
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };
    getAllCategories();

    
    
    $scope.showNewQuestionForm = function () {
        $scope.question = "";
        $scope.email = "";
        $scope.newquestionform.$setPristine();
        hideAll();
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
                hideAll();
                $scope.showThanks = true;

            })
            .error(function (data, status) {
                console.log(status + data);
            });
    };

    $scope.showCategory = function (id) {
        hideAll();
        $http.get(questionByCatUrl + id)
        .success(function (questionsByCat) {
            $scope.questions = questionsByCat;
            $scope.showFAQ = true;
        })
        .error(function (data, status) {
            console.log(status + data);
        });
    };

    function hideAll() {
        $scope.showForm = false;
        $scope.showThanks = false;
        $scope.showFAQ = false;
        $scope.showLoading = false;
    }
});