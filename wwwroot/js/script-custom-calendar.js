$(document).ready(function () {
  InitializeCalendar();
});

function InitializeCalendar() {
  try {
    const calenderE1 = document.getElementById("calendar");

    let calendar = new FullCalendar.Calendar(calenderE1, {
      initialView: "dayGridMonth",
      headerToolbar: {
        left: "prev,next,today",
        center: "title",
        right: "dayGridMonth,timeGridWeek,timeGridDay",
      },
      selectable: true,
      editable: false,
      select: function (event) {
        console.log(event);
        onShowModal(event, null);
      },
    });
    calendar.render();
  } catch (e) {
    alert(e);
  }
}

function onShowModal(obj, isEventDetails) {
  $("#appointmentInput").modal("show");
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
}