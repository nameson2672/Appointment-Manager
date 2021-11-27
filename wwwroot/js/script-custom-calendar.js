$(document).ready(function () {
    InitializeCalendar();
})

let calander;
function InitializeCalendar() {
    try {
        const calenderE1 = document.getElementById('calendar')

        const calendar = new FullCalendar.Calendar(calenderE1,{
            initialView: 'dayGridMonth',
            headerToolbar: {
                left: 'prev,next,today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            selectable: true,
            editable: false,
            select: function (event) {
                onShowModal(event, null);
            }
        });
        calendar.render();
    }
    catch (e) {
        alert(e);
    }
}

function onShowModel(obj, isEventDetails) {
    $("appointmentInput").modal("show");
}