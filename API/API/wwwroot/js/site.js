// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ImprovePageSubmit() {
    //Set the URL.
    var url = $("#ImprovePageForm").attr("action");

    var data = new Object();
    data.name = $('#ImprovePageForm #Username').val();
    data.email = $('#ImprovePageForm #Email').val();
    data.message = $('#ImprovePageForm #Message').val();
    data.link = window.location.href;

    $.ajax({
        type: 'POST',
        url: "/ImprovePage",
        data: JSON.stringify(data),
        dataType: 'html',
        contentType: "application/json",
        success: function () {
            console.log("success", arguments);
            $("#ImprovePageForm #status").text("Got it! Thank you 👌")
        },
        error: function () {
            console.log("error", arguments);
            $("#ImprovePageForm #status").text("😣 Something went wrong. Please try again.")
        },
        complete: function () {
            console.log("complete", arguments);
        }
    }).done(function () {
        console.log("done", arguments);
    });
}