var App = angular.module("faq", ['ui.bootstrap']);

App.controller("controller", function ($scope, $http) {
    var categoryUrl = '/api/Category/';
    var questionUrl = '/api/Question/';
    var questionByCatUrl = '/api/Question/cat/';
    hideAll();
    $scope.questions = [];
    $scope.question = [];
    $scope.category = [];
    function getAllCategories() {
        $http.get(categoryUrl).
        success(function (allCategories) {
            $scope.showLoading = false; 

            $scope.rowhack = [];
            while (allCategories.length) {
                $scope.rowhack.push(allCategories.splice(0, 2))
            }
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };

    function getQuestionsByCategory(id){
        $http.get(questionByCatUrl + id).
        success(function (questionsByCat){
            $scope.showLoading=false,
            $scope.questions = questionsByCat,
            $scope.category.name = questionsByCat[0].categoryname,
            $scope.category.id = questionByCatUrl[0].categoryid
        }).
        error(function(data,status){
            console.log(status + data)
        });
    };
    

    
    
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
    $scope.showFAQFunction = function () {

        hideAll();
        $scope.showLoading = true;
        getAllCategories();

        $scope.showFAQ = true;


    }



    $scope.upVote = function (question) {
        if (sessionStorage.getItem(question.id) == "true")
            return; 
        sessionStorage.setItem(question.id, "true");
        $scope.question = question;
        $scope.question.upvotes = $scope.question.upvotes + 1;
        updateQuestion(question.id, question);
    }

    function updateQuestion(id, question) {
        $http.put(questionUrl + id, question).
            success(function (data) {
                // do nothing
            }).
            error(function (data, status) {
                console.log(status + data);
            });
    };
    function hideAll() {
        $scope.showForm = false;
        $scope.showThanks = false;
        $scope.showFAQ = false;
        $scope.showLoading = false;
        $scope.showQuestion = false;
        $scope.showCategory = false; 
    }

    function getQuestion(id) {
        $http.get(questionUrl + id)
            .success(function (data) {
                $scope.showLoading = false,
                $scope.question = data,
                console.log($scope.question)
            })
            .error(function (data, status) {
                console.log(status + data)
            });
    };



    $scope.showCategoryFunction = function (id) {
        $scope.oneAtATime = false;
        hideAll();
        $scope.showLoading = true;
        getQuestionsByCategory(id);
        $scope.status = {
            isFirstOpen: true,
            isFirstDisabled: false,
            isOpen: false
        };
        $scope.showCategory = true;
    };

    $scope.showQuestionFunction = function (question) {
        hideAll();
        $scope.showLoading = true;
        getQuestionsByCategory(question.categoryid);
        $scope.status = [];
        $scope.status.isOpen = [];
        $scope.status.isOpen[question.id] = true;
        $scope.showCategory = true;
    };
});
