// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    //const wrapper = document.querySelector("#filters")
    //const filters = wrapper.querySelectorAll('div > select');

    //console.log("on load filters ", filters)

    //for (const filter of filters) {
    //    console.log("filter ", filter)
    //    filter.addEventListener('change', selectFilter);
    //}
}

function selectFilter(e) {
    console.log("filters change", e)
    let text = e.options[e.selectedIndex].text;
    let value = e.options[e.selectedIndex].value;

    const div = document.querySelector("#criterias")

    if (!document.getElementById(`span-${text}`)) {
        const span = document.createElement("span");
        span.setAttribute("id", `span-${text}`)
        span.setAttribute("data-prior", e.dataset.prior)
        span.setAttribute("class", "cursor-out p-2 ps-3 me-2 badge rounded-pill text-dark border border-secondary border-1 shadow-sm")
        span.textContent = text
        span.title = value

        const btn = document.createElement("img");
        btn.src = "../css/times-circle.svg"
        btn.alt = "Remove this filter"
        btn.setAttribute("class", "remove-filter")
        span.addEventListener('click', () => { span.remove(); buildLink(); })

        span.appendChild(btn);

        
        div.appendChild(span);
    }
    buildLink()
}

function indexMatchingText(ele, text) {
    for (var i = 0; i < ele.length; i++) {
        if (ele[i].childNodes[0].nodeValue === text) {
            return i;
        }
    }
    return undefined;
}


function buildLink() {
    var div = $('#criterias');

    var res = div.find('span').sort(function (a, b) {
        return a.dataset.prior - b.dataset.prior;
        //if (indexMatchingText(a, a.text) < indexMatchingText(b, b.text)) return -1;
    })

    console.log("result selects", res)

    const priors = new Set();
    for (let s of res) {
        priors.add(s.dataset.prior);
    }

    console.log("priors", priors)

    let link = "";

    for (let prior of priors) {
        var spans = $(`span[data-prior=${prior}]`)
        spans = spans.sort(function (a, b) {
            console.log("data", a.textContent, b.textContent)
            return a.textContent.localeCompare(b.textContent);
        })
        for (var i = 0 in spans) {
            spans[i].title ? link = link + spans[i].title : null

            if (i < spans.length - 1) link = link + "-or-"
        }
        link = link + "-and-"
        //console.log("spans", spans)
    }

    //console.log("link", link)

    //if (res[0]) {
    //    startPrior = res[0].dataset.prior;
    //    link = res[0].value;
    //}
    //else return;

    //for (let s of res) {
    //    s.value ? link = link + s.value + "-and" : null
    //    //console.log("res", s.value)
    //}

    link = link.substring(0, link.lastIndexOf("-and-"))

    if (link) window.history.pushState("filter", "", `/visa-${link}`);
    else window.history.pushState("filter", "", "/");

    document.title = `Best visas for ${link}`
    $('meta[name="description"]').attr("content", `Here are best visas for ${link}`);


    $.ajax({
        type: 'GET',
        url: `api/Visas/GetVisas/${link}`,
        //data: JSON.stringify(data),
        dataType: 'html',
        //contentType: "application/json",
        success: function (result) {
            //console.log("success", result);
            //const div = document.querySelector("#res")
            //div.append(result)
            //$("#res").append(result);
            document.getElementById("res").innerHTML = result;
            //$("#ImprovePageForm #status").text("Got it! Thank you 👌")
        },
        error: function (req, status, error) {
            console.log(error, status);
            //$("#ImprovePageForm #status").text("😣 Something went wrong. Please try again.")
        },
        complete: function () {
            //console.log("complete", arguments);
        }
    }).done(function () {
        //console.log("done", arguments);
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