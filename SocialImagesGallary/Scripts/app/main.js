$(document).ready(function () {
    ko.applyBindings(viewModel);
    getAllUsers();
});
var apiUserUrls = {
    common: "/Admin/",
    getAllUsers: "/api/User/",
    postUser: "/Admin/Create",
    deleteUser: "/Admin/Delete"
};
var viewModel = {
    users: ko.observableArray(),
    profileHandler: function(data) {
        sendAjaxRequest("POST", reloadPage, "Profile/LoadUsersProfile", { userNam: data.UserName });
    },
    galleryHandler: function(data) {
        sendAjaxRequest("POST", reloadPage, "Image/LoadUsersGallery", { userNam: data.UserName });
    }
};

function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getAllUsers() {
    sendAjaxRequest("GET", insertData, apiUserUrls.getAllUsers);
}

function insertData(data) {
    viewModel.users.removeAll();
    for (var i = 0; i < data.length; i++) {
        viewModel.users.push(data[i]);
    }
}

function reloadPage(response) {
    window.location=response.url;
}


