// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    loadNoVisaEntry()
    //loadBookingW()

    var country = document.getElementById("CitizenshipId")

    var ind = [...country.options].findIndex(op => op.text === getCookie("myCountry"))
    console.log(ind)

    if (ind > -1)
        country.selectedIndex = ind
    else        
        country.selectedIndex = 0

    //$('#CitizenshipId').tooltip('show');
    $('#filters').tooltip('show');
    setTimeout(function () { $('#filters').tooltip('hide'); }, 10000);
    //const wrapper = document.querySelector("#filters")
    //const filters = wrapper.querySelectorAll('div > select');

    //console.log("on load filters ", filters)

    //for (const filter of filters) {
    //    console.log("filter ", filter)
    //    filter.addEventListener('change', selectFilter);
    //}
}

function wait(ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
        end = new Date().getTime();
    }
}

function loadNoVisaEntry() {
    const div = document.querySelector("#noVisaCountry")
    if (!div) return

    const l = document.getElementById("loader")
    div.innerHTML = ''
    div.append(l.content.cloneNode(true));

    $.ajax({
        type: 'GET',
        url: `api/Visas/NoVisasEntry`,
        dataType: 'html',
        success: function (result) {
            div.innerHTML = result
        },
        error: function (req, status, error) {
            console.log(error, status);
        },
        complete: function () {
            //console.log("complete", arguments);
        }
    }).done(function () {
        //console.log("done", arguments);
    });
}

function loadBookingW() {
    const div = document.querySelector("#bookingW")
    if (!div) return

    const l = document.getElementById("loader")
    div.innerHTML = ''
    div.append(l.content.cloneNode(true));

    $.ajax({
        type: 'GET',
        url: `api/Visas/BookingW`,
        dataType: 'html',
        success: function (result) {
            div.innerHTML = result
        },
        error: function (req, status, error) {
            console.log(error, status);
        },
        complete: function () {
            //console.log("complete", arguments);
        }
    }).done(function () {
        //console.log("done", arguments);
    }); 
}

function buildClearFilters() {

    const id = "span-clear-filters"
    if (document.getElementById(id))
        document.getElementById(id).remove();

    if (!document.querySelector("#criterias > span")) return

    const div = document.querySelector("#criterias")

    const span = document.createElement("span");
    span.setAttribute("id", id)
    span.setAttribute("class", "cursor-out p-2 ps-3 me-2 badge rounded-pill bg-danger border border-1 shadow-sm")
    span.textContent = "Clear filters"

    const btn = document.createElement("img");
    btn.src = "../css/times-circle.svg"
    btn.alt = "Clear filters"
    btn.setAttribute("class", "remove-filter")
    span.addEventListener('click', () => { clearFilters() })
    span.appendChild(btn);
    div.appendChild(span);
}

function clearFilters() {
    document.querySelector("#criterias").replaceChildren()
    buildLink()
}

function remove(el) {
    var elem = el
    elem.remove()
    buildClearFilters()
    buildLink()
}

function selectFilter(e) {
    //console.log("filters change", e)
    let text = e.options[e.selectedIndex].text;
    let value = e.options[e.selectedIndex].value;

    const div = document.querySelector("#criterias")

    if (!document.getElementById(`span-${text}`)) {
        const span = document.createElement("span");
        span.setAttribute("id", `span-${text}`)
        span.setAttribute("data-prior", e.dataset.prior)
        span.setAttribute("class", "cursor-out p-2 ps-3 me-2 badge rounded-pill text-dark border border-1 shadow-sm")
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

    e.selectedIndex = "0";

    buildClearFilters()
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

function toggleDisplay(el) {
    console.log($(`#${el}`))
    $(`#${el}`).toggle();
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function setCountry(e) {
    var countryName = e.options[e.selectedIndex].text
    var CapitalCode = e.options[e.selectedIndex].value

    setCookie("myCountry", countryName, 14)

    if (location.pathname != "/") {
        //var loc = window.location.href + "citizen-" + countryName;
        //alert(loc);
        window.location.href = window.location.href;
        return;
    }
    //console.log(location.pathname);

    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "//tp.media/content?currency=usd&promo_id=4044&shmarker=14510&campaign_id=100&trs=21653&target_host=search.jetradar.com&locale=en_us&limit=6&powered_by=false&destination=" + CapitalCode;
    s.innerHTML = null;
    s.id = "map123";
    document.getElementById("bookingW").innerHTML = "";
    document.getElementById("bookingW").appendChild(s);

    loadNoVisaEntry();

    //CapitalCode.length == 0 ? "BKK" : CapitalCode;
}


function buildLink() {
    var div = document.getElementById('criterias')
    var spans = div.querySelectorAll('[data-prior]');

    console.log("div > ", div)
    console.log("div > spans", spans)
    

    var res = [].slice.call(spans).sort(function (a, b) {
        return a.dataset.prior - b.dataset.prior;
    })
    //console.log("result selects", res)

    const priors = new Set();
    for (let s of res) {
        priors.add(s.dataset.prior);
    }
    //console.log("priors", priors)

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
    }

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
            document.getElementById("res").innerHTML = result;
        },
        error: function (req, status, error) {
            console.log(error, status);
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

function SubscribeUpdatesSubmit() {
    var data = new Object();
    data.name = $('#SubscribeForm #Username').val();
    data.email = $('#SubscribeForm #Email').val();
    data.link = window.location.href;

    $.ajax({
        type: 'POST',
        url: "/SubscribeUpdates",
        data: JSON.stringify(data),
        dataType: 'html',
        contentType: "application/json",
        success: function () {
            console.log("success", arguments);
            $("#SubscribeForm #status").text("Subscribed! Thank you 👌")
        },
        error: function () {
            console.log("error", arguments);
            $("#SubscribeForm #status").text("😣 Something went wrong. Please try again.")
        },
        complete: function () {
            console.log("complete", arguments);
        }
    }).done(function () {
        console.log("done", arguments);
    });
}

function ShareExpSubmit() {
    var data = new Object();
    data.email = $('#ShareExpForm #Email').val();
    data.message = $('#ShareExpForm #Message').val();
    data.link = window.location.href;

    $.ajax({
        type: 'POST',
        url: "/ShareExperience",
        data: JSON.stringify(data),
        dataType: 'html',
        contentType: "application/json",
        success: function () {
            console.log("success", arguments);
            $("#ShareExpForm #status").text("Got it! Thank you 👌")
        },
        error: function () {
            console.log("error", arguments);
            $("#ShareExpForm #status").text("😣 Something went wrong. Please try again.")
        },
        complete: function () {
            console.log("complete", arguments);
        }
    }).done(function () {
        console.log("done", arguments);
    });
}

function FocusSubscribeForm() {
    var inputEmail = document.getElementById("Email");

    //$('#Email').tooltip('show');
    //setTimeout(function () { $('#Email').tooltip('hide'); }, 3000);
    
    inputEmail.focus();
}