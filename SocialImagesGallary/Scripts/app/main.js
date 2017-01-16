$(document).ready(function () {
    ko.applyBindings(viewModel);
    getAllUsers();
    $('#head').on('click', 'th', function () {
        getSortedUsers($(this).text());
    });
});
var apiUserUrls = {
    common: "/Admin/",
    getAllUsers: "/Admin/GetAllUsers",
    //getUser: common,
    postUser: "/Admin/Create",
    //updateUser: "",
    deleteUser: "/Admin/Delete"
    //getAllUser: ""
};
var apiSortUrls = {
    common: "/api/Sort/"
};
var viewModel = {
    users: ko.observableArray(),
    editor: {
        userName: ko.observable(""),
        firstName: ko.observable(""),
        lastName: ko.observable(""),
        email: ko.observable(""),
        password: ko.observable(""),
    }
};

function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getAllUsers() {
    sendAjaxRequest("GET", insertData, apiUserUrls.getAllUsers);
}
function deleteUser(user) {
    sendAjaxRequest("GET", deleteUserHelper, apiUserUrls.deleteUser, { userId: user.Id });
}
function getSortedUsers(by) {
    sendAjaxRequest("GET", insertData, apiSortUrls.common, { by: by });
}
function insertData(data) {
    viewModel.users.removeAll();
    for (var i = 0; i < data.length; i++) {
        viewModel.users.push(data[i]);
    }
}
function deleteUserHelper(userId) {
    for (var i = 0; i < viewModel.users().length; i++) {
        if (viewModel.users()[i].Id == userId) {
            viewModel.users.remove(viewModel.users()[i]);
            break;
        }
    }
}
function createUser() {
    var newUser = getUserDTO();
    console.log(newUser);
    sendAjaxRequest("POST", function (id) {
        newUser.Id = id;
        viewModel.users.push(newUser);
    }, apiUserUrls.postUser, { model: newUser });
}

function getUserDTO() {
    var user = {};
    user.UserName = viewModel.editor.userName();
    user.FirstName = viewModel.editor.firstName();
    user.LastName = viewModel.editor.lastName();
    user.Email = viewModel.editor.email();
    user.Password = viewModel.editor.password();
    return user;
}

