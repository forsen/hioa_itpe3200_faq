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
        $http.get(categoryUrl, { params: { unanswered: false } }).
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
        $http.get(categoryUrl, { params: { unanswered: false } }).
        success(function (allCategories) {
            $scope.showLoading = false,
            $scope.rowhack = []
            while (allCategories.length) {
                $scope.rowhack.push(allCategories.splice(0, 2))
            }
            $scope.showFAQ = true
        }).
        error(function (data, status) {
            console.log(status + data);
        });

        


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
    $scope.showAdminFunction = function () {
        hideAll();
        $scope.showLoading = true;
        $http({
            url: categoryUrl,
            method: 'GET',
            params: { unanswered: true }
        })
            .success(function (data) {
                $scope.showLoading = false,
                $scope.categories = data,
                $scope.showAdmin = true
            })
            .error(function (data, status) {
                console.log(status + data)
            });
        
    };

    $scope.answerQuestion = function (question) {
        hideAll();
        $scope.question = question; 
        $scope.answerFormQuestion = question.question;
        $scope.answerFormEmail = question.email;
        $scope.answerFormDate = question.asked;
        $scope.answerFormAnswer = ""; 
        $scope.answerform.$setPristine();
        $http.get(categoryUrl, {params: {unanswered : true}}).
        success(function (allCategories) {
            $scope.showLoading = false,
            $scope.categories = allCategories,
            $scope.showAnswerForm = true,
            $scope.category = $scope.categories[question.categoryid-1]
        }).
        error(function (data, status) {
            console.log(status + data);
        });
    };
    function putAnswer(answer) {
        $http.put(questionUrl + "ans/" + $scope.question.id, answer).
            success(function (data) {
                $scope.showLoading = false,
                $scope.showAdminFunction()
            }).
            error(function (data, status) {
                console.log(status + data)
            });
    };
    $scope.saveAnswer = function (dontshowinfaq) {
        hideAll();
        $scope.showLoading = true;
        console.log("dontshowinfaq: " + dontshowinfaq);
        var answer = {
            answer: $scope.answerFormAnswer,
            userid: 5
        };
        if (!$scope.answerform.answerFormQuestion.$pristine || !$scope.answerform.answerFormCategory.$pristine || dontshowinfaq == true) {
            $scope.question.question = $scope.answerFormQuestion;
            $scope.question.categoryid = $scope.category.id;
            $scope.question.dontshowinfaq = 1;
            console.log("huoaesnt");
            $http.put(questionUrl + $scope.question.id, $scope.question).
                success(function (data) {
                    putAnswer(answer); 
                }).
                error(function (data, status) {
                    console.log(status + data);
                });
        }
        else
            putAnswer(answer);

    };
    function hideAll() {
        $scope.showForm = false;
        $scope.showThanks = false;
        $scope.showFAQ = false;
        $scope.showLoading = false;
        $scope.showQuestion = false;
        $scope.showCategory = false;
        $scope.showAdmin = false;
        $scope.showAnswerForm = false;
    };
});
