
var viewModel = {
    id: ko.observable(),
    userName: ko.observable(),
    name: ko.observable(),
    adress: ko.observable(),
    email: ko.observable()
};
var profileUrls = {
    getProfile: "/Profile/GetProfile",
    loadAvatar: "/Profile/UploadUserImage"
};
function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getProfile(userNam) {
    sendAjaxRequest("GET", insertData, profileUrls.getProfile, {userName:userNam});
}
function insertData(data) {
    viewModel.id(data.Id);
    viewModel.userName(data.UserName);
    viewModel.name(data.Name);
    viewModel.email(data.Email);
    viewModel.adress(data.Adress);
}
