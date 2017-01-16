var DesktopNotificationsFactory = function (webNotification) {
    return function(title,text)
    {
        webNotification.showNotification(title, {
            body: text,
            icon: 'my-icon.ico',
            onClick: function onNotificationClicked() {
                console.log('Notification clicked.');
            },
            autoClose: 4000 //auto close the notification after 4 seconds (you can manually close it via hide function)
        }, function onShow(error, hide) {
            if (error) {
                window.alert('Unable to show notification: ' + error.message);
            } else {
                setTimeout(function hideNotification() {
                    hide();
                }, 5000);
            }
        });
    }
}

DesktopNotificationsFactory.$inject = ['webNotification'];