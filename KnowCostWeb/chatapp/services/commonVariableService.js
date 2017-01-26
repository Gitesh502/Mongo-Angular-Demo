function commonVariableService() {


    var lstfromMessages = [];
    var lstprivateChatBoxes = [];
    return {
        getfromMessagesProperties: function () {
            return lstfromMessages;
        },
        setfromMessagesProperties: function (fromMessages) {
            lstfromMessages.push(fromMessages);
        },
        getPrivateChatBoxes: function () {
            return lstprivateChatBoxes;
        },
        setPrivateChatBoxes: function (chatBox) {
            lstprivateChatBoxes.push(chatBox);
        }
    }

}
commonVariableService.$inject = [];