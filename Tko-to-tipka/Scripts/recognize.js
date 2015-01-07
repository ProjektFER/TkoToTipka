function ajaxCall(input) {
    return $.ajax({
        type: "POST",
        url: '/Home/ParseUserInput',
        data: JSON.stringify(input),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        traditional: true,
        success: function (data) {
            $("#recognizeForm").replaceWith("<div class='results'>Korisnik koji tipka je: <br/ > <span class='orange'><h3>" + data.username + "</h3></span> </div>");
        },
        error: function () {
            $("#resultBox").replaceWith("<p>Došlo je do greške, pokušajte kasnije.</p>");
        }
    });
};