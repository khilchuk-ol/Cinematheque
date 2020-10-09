function searchFailed() {
    $("#searchresults").html("Sorry, there was a problem with the search.");
}

$("input[data-autocomplete-source]").each(function () {
    var target = $(this);
    target.autocomplete({ source: target.attr("data-autocomplete-source") });
});

$("#filmSearch").submit(function (event) {
    event.preventDefault();

    var form = $(this);
    $.ajax({
        url: form.attr("action"),
        data: form.serialize(),
        beforeSend: function () {
            $("#ajax-loader").show();
        },
        complete: function () {
            $("#ajax-loader").hide();
        },
        error: searchFailed,
        success: function (data) {
            var html = Mustache.to_html($("#filmTemplate").html(),
                { films: data });
            $("#searchresults").empty().append(html);
        }
    });
});