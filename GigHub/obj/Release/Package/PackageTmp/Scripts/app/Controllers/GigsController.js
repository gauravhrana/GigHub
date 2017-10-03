
var GigsController = function (attendanceService) {
    var btnGoing;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {

        btnGoing = $(e.target);

        var gigId = btnGoing.attr("data-gig-id");

        if (btnGoing.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var done = function () {
        var text = (btnGoing.text() == "Going") ? "Going?" : "Going";

        btnGoing.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        bootbox.alert("Something Failed!");
    };

    return {
        init: init
    }

}(AttendanceService);