// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    const wrapper = document.querySelector("#filters")
    const filters = wrapper.querySelectorAll('div > select');

    console.log("on load filters ", filters)

    for (const filter of filters) {
        console.log("filter ", filter)
        filter.addEventListener('change', (event) => {
            console.log("filters change", event.target.value)
            buildLink()
        });
    }
}


function buildLink() {
    var $wrapper = $('#filters');

    var res = $wrapper.find('select').sort(function (a, b) {
        return a.dataset.prior - b.dataset.prior;
    })

    console.log("result selects", res)

    let link = ""

    for (let s of res) {
        s.value ? link = link + s.value + "-and" : null
        console.log("res", s.value)
    }

    link = link.substring(0, link.lastIndexOf("-"))

    console.log("result", link)

    window.history.pushState("filter", "", `/visa${link}`);
    document.title = `Best visas for ${link}`
    $('meta[name="description"]').attr("content", `Here are best visas for ${link}`);


    $.ajax({
        type: 'GET',
        url: `api/Visas/GetVisas/${link}`,
        //data: JSON.stringify(data),
        dataType: 'html',
        contentType: "application/json",
        success: function () {
            console.log("success", arguments);
            //$("#ImprovePageForm #status").text("Got it! Thank you 👌")
        },
        error: function () {
            console.log("error", arguments);
            //$("#ImprovePageForm #status").text("😣 Something went wrong. Please try again.")
        },
        complete: function () {
            console.log("complete", arguments);
        }
    }).done(function () {
        console.log("done", arguments);
    });
    
}

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