var routeURL = location.protocol + "//" + location.host;
console.log(routeURL);
$(document).ready(function () {
  $("#appointmentDate").kendoDateTimePicker({
    value: new Date(),
    dateInput: false,
  });
  InitializeCalendar();
});

function InitializeCalendar() {
  try {
    const calenderE1 = document.getElementById("calendar");
    if (calenderE1 != null) {
      let calendar = new FullCalendar.Calendar(calenderE1, {
        initialView: "dayGridMonth",
        headerToolbar: {
          left: "prev,next,today",
          center: "title",
          right: "dayGridMonth,timeGridWeek,timeGridDay",
        },
        selectable: true,
        editable: false,
        eventDisplay: "block",
        events: function (fetchInfo, successCallback, failureCallback) {
          $.ajax({
            url:
              routeURL +
              "/api/Appointment/GetCalendarData?doctorId=" +
              $("#doctorId").val(),
            type: "GET",
            dataType: "JSON",
            success: function (response) {
              var events = [];
              if (response.status === 1) {
                $.each(response.dataenum, function (i, data) {
                  events.push({
                    title: data.title,
                    description: data.description,
                    start: data.startDate,
                    end: data.endDate,
                    backgroundColor: data.isDoctorApproved
                      ? "#28a745"
                      : "#dc3545",
                    borderColor: "#162466",
                    textColor: "white",
                    id: data.id,
                  });
                });
              }
              successCallback(events);
            },
            error: function (xhr) {
              $.notify("Error", "error");
            },
          });
        },
        select: function (event) {
          console.log(event);
          onShowModal(event, null);
        },
      });
      calendar.render();
    }
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

function onSubmitForm() {
  console.log($("#patientId").val());

  if (checkValidation()) {
    var requestData = {
      Id: parseInt("asd"),
      Title: $("#title").val(),
      Description: $("#description").val(),
      StartDate: $("#appointmentDate").val(),
      Duration: $("#duration").val(),
      DoctorId: $("#doctorId").val(),
      PatientId: $("#patientId").val(),
    };

    $.ajax({
      url: routeURL + "/api/Appointment/SaveCalendarData",
      type: "POST",
      data: JSON.stringify(requestData),
      contentType: "application/json",
      success: function (response) {
        if (response.status === 1 || response.status === 2) {
          //   calendar.refetchEvents();
          $.notify(response.message, "success");
          onCloseModal();
        } else {
          $.notify(response.message, "error");
        }
      },
      error: function (xhr) {
        $.notify("Error", "error");
      },
    });
  }
}

function checkValidation() {
  var isValid = true;
  if ($("#title").val() === undefined || $("#title").val() === "") {
    isValid = false;
    $("#title").addClass("error");
    console.log("me 2");
  } else {
    $("#title").removeClass("error");
  }

  if (
    $("#appointmentDate").val() === undefined ||
    $("#appointmentDate").val() === ""
  ) {
    isValid = false;
    $("#appointmentDate").addClass("error");
  } else {
    $("#appointmentDate").removeClass("error");
  }

  return isValid;
}
