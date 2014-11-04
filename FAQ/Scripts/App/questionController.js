/*var App = angular.module("faq", []);

App.controller("questionController", function ($scope, $http) {
    var url = 'api/Question/';
    $scope.showForm = false;
    $scope.showNewQuestionForm = function () {
        $scope.question = "";
        $scope.email = "";
        $scope.newquestionform.$setPristine();
        $scope.showForm = true;
    };

    $scope.saveNewQuestion = function () {
        var question = {
            question: $scope.question,
            category: $scope.category,
            email: $scope.email,
            asked: Date.now
        };

        $http.post(url, question)
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
*/