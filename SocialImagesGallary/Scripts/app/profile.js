
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

//function reloadAva(data,urla) {
//    $.ajax({
//        url: profileUrls.loadAvatar,
//        data: data,
//        cache: false,
//        contentType: false,
//        processData: false,
//        type: 'POST',
//        success: function () {
//            alert("lol");
//            reloadAvatar(urla);
//        }
//    });
//    //sendAjaxRequest("GET", function() {}, profileUrls.loadAvatar,file);
//}
function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getProfile() {
    sendAjaxRequest("GET", insertData, profileUrls.getProfile);
}
function insertData(data) {
    viewModel.id(data.Id);
    viewModel.userName(data.UserName);
    viewModel.name(data.Name);
    viewModel.email(data.Email);
    viewModel.adress(data.Adress);
}
//function insertAvatar() {
//    console.log("load");
//    var src = $('#img').val();
//    $('#picture').attr("src", src);
//}

//function reloadAvatar(urla) {
//    alert("Hi");
//    var target = $('.avatar');
//    //target.empty();
//    //target.append("<img id=\"picture\" src=\"" + urla + "\" width=\"250\" height=\"280\"/>");
//    $.ajax({
//        url: "Profile/renderAvatar",
//        dataType:"xhr",
//        type: 'get',
//        success: function (data) {
//            alert("Hi");
//            target.attr("src", data);
//        }
//    });
//}