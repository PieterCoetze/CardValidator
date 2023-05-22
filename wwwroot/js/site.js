$(document).ready(() => {
    InitializeDataTable();
});

const showNotification = (message) => {
    var arr = message.toString().split(',');
    $.toast({
        heading: arr[1].trim().toUpperCase(),
        text: arr[0].trim(),
        showHideTransition: 'fade',
        icon: arr[1].trim()
    })
},

InitializeDataTable = () => {
    $('.dataTable').DataTable({
        "iDisplayLength": 25
    });
},

ShowModal = () => {
    $('.modal').modal('show');
},

HideModal = () => {
    $('.modal').modal('hide');
},

Redirect = (url) => {
    location.href = url;
}