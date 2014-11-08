var App = angular.module("faq", ['ui.bootstrap']);

App.controller("controller", function ($scope, $http) {
    var categoryUrl = '/api/Category/';
    var questionUrl = '/api/Question/';
    var questionByCatUrl = '/api/Question/cat/';

    hideAll();
    $scope.questions = [];
    $scope.question = [];
    $scope.category = [];
    var rowhack;

    function getAllCategories() {
        $http.get(categoryUrl).
        success(function (allCategories) {
            $scope.showLoading = false,
            $scope.rowhack = []
            while (allCategories.length) {
                $scope.rowhack.push(allCategories.splice(0, 2))
            }
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };
    getAllCategories();


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
        hideAll();
        $scope.showLoading = true;
        $scope.formQuestion = "";
        $scope.formEmail = "";
        $scope.newquestionform.$setPristine();
        $http.get(categoryUrl).
        success(function (allCategories) {
            $scope.showLoading = false,
            $scope.categories = allCategories,
            $scope.showForm = true
        }).
        error(function (data, status) {
            console.log(status + data);
        });
       
        

    };

    $scope.saveNewQuestion = function () {
        var question = {
            question: $scope.formQuestion,
            categoryid: $scope.category.id,
            email: $scope.formEmail
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

        $http.get(categoryUrl).
            success(function (allCategories) {
                $scope.showLoading = false,
                $scope.categories = allCategories
            }).
            error(function (data, status) {
                console.log(status + data);
            });


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
        $scope.showAdmin = false; 
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

    function getUnAnsweredQuestions() {
        
        $http({
            url: questionUrl,
            method: 'GET',
            params: {unanswered : true}
        })
            .success(function (data) {
                $scope.showLoading = false;
                $scope.questions = data
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
    $scope.showAdminFunction = function () {
        hideAll();
        $scope.showLoading = true;
        getUnAnsweredQuestions();
        $scope.showAdmin = true;
    }
});
