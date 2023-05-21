const showNotification = (message) => {
    var arr = message.toString().split(',');
    $.toast({
        heading: arr[1].trim().toUpperCase(),
        text: arr[0].trim(),
        showHideTransition: 'fade',
        icon: arr[1].trim()
    })
}
