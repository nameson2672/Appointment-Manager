var routeURL = location.protocol + "//" + location.host;
console.log(routeURL);
$(document).ready(function () {
  $("#appointmentDate").kendoDateTimePicker({
    value: new Date(),
    dateInput: false,
  });
  InitializeCalendar();
});

let calendar;

function InitializeCalendar() {
  try {
    const calenderE1 = document.getElementById("calendar");
    if (calenderE1 != null) {
      calendar = new FullCalendar.Calendar(calenderE1, {
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
        eventClick: function (info) {
          getEventDetailsByEventId(info.event);
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

function onShowModal(obj, isEventDetail) {
  if (isEventDetail != null) {
    $("#title").val(obj.title);
    $("#description").val(obj.description);
    $("#appointmentDate").val(obj.startDate);
    $("#duration").val(obj.duration);
    $("#doctorId").val(obj.doctorId);
    $("#patientId").val(obj.patientId);
    $("#id").val(obj.id);
    $("#lblPatientName").html(obj.patientName);
    $("#lblDoctorName").html(obj.doctorName);

    if (obj.isDoctorApproved) {
      $("#lblStatus").html("Approved");
      //$("#btnConfirm").addClass("d-none");
      //$("#btnSubmit").addClass("d-none");
    } else {
      $("#lblStatus").html("Pending");
      //$("#btnConfirm").removeClass("d-none");
      //$("#btnSubmit").removeClass("d-none");
    }
  } else {
    $("#appointmentDate").val(
      obj.startStr + " " + new moment().format("hh:mm A")
    );
    $("#id").val(0);
  }
  $("#appointmentInput").modal("show");
}

function onCloseModal() {
  // $("#appointmentForm")[0].reset();
  $("#title").val("");
  $("#description").val("");
  $("#appointmentDate").val("");
  $("#duration").val("");
  $("#id").val(0);
  $("#lblPatientName").html("");
  $("#lblDoctorName").html("");
  $("#appointmentInput").modal("hide");
  $("#lblPatientName").html(obj.patientName);
  $("#lblDoctorName").html(obj.doctorName);
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
          calendar.refetchEvents();
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

function getEventDetailsByEventId(info) {
  $.ajax({
    url: routeURL + "/api/Appointment/GetCalendarDataById/" + info.id,
    type: "GET",
    dataType: "JSON",
    success: function (response) {
      if (response.status === 1 && response.dataenum !== undefined) {
        onShowModal(response.dataenum, true);
      }
      //successCallback(events);
    },
    error: function (xhr) {
      $.notify("Error", "error");
    },
  });
}

function onDoctorChange() {
  calendar.refetchEvents();
  console.log("running");
}
