var authDataService = new function () {
    var serviceBase = "/TrackAboutApi/";

    saveStudent = function (studentObj) {
        $.post(serviceBase + 'SaveStudent', { model: studentObj }, function (response) {
            bindStudentDetails(response);
        });
    },
        loginStudent = function (studentLoginObj) {
        $.post(serviceBase + 'LoginStudent', { model: studentLoginObj }, function (response) {
                bindLoginStudentDetails(response);
            });
        }

    return {
        saveStudent: saveStudent,
        loginStudent: loginStudent
    };
}();