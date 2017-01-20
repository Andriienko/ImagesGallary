var viewModel = {
    messages: ko.observableArray()
};
function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getImagePartial(userNik, index) {
    var ob = {
        userName: userNik,
        id:index
    };
    sendAjaxRequest("GET", insertData, "PartialPhoto", ob);
}
function getAllMessages(id) {
    sendAjaxRequest("GET", insertMsgs, "GetAllMessages", {imgId:id});
}
function insertData(data) {
    var target = $('#partial');
    target.empty();
    target.append(data);
    var imgId = $('#imgId').val();
    var count = $('#count').val();
    console.log(count);
    if(count!==0)
        getAllMessages(imgId);
}

function addMessage(msgDto) {
    sendAjaxRequest("POST", function(data) {
        viewModel.messages.push(data);
    }, "AddMessage", {newMessage: msgDto });
}
function insertMsgs(data) {
    viewModel.messages.removeAll();
    for (var i = 0; i < data.length; i++) {
        viewModel.messages.push(data[i]);
    }
}

function isFriends(flag) {
    console.log(flag);
    if (flag === 0) {
        $('.typing').hide();
        $('.coments').hide();
    }
}
function hideBtnsIfNotOwner() {
    var owner = $('#accountOwner').val();
    var userName = $('#userName').val();
    if (owner !== userName) {
        $('#addPhoto').hide();
    }
}
function addPhoto(data,urla) {
    $.ajax({
                url: urla,
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function () {}
            });
}
