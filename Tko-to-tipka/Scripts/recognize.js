function ajaxCall(input) {
    return $.ajax({
                type: "GET",
                url: '/Home/ParseUserInput',
                data: { data: JSON.stringify(input) },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                traditional: true,
                success: function (data, status) {
                    $("#recognizeForm").replaceWith("<div class='results'> Korisnik koji tipka je: " + data.username + ".<br />Vjerojatnost: " + data.score + "%. </div>");
                },
                error: function () {
                    $("#resultBox").replaceWith("<p>Došlo je do greške, pokušajte kasnije.</p>");
                }
        });
};


