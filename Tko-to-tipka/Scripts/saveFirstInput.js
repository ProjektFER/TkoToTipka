function ajaxCall(input) {

    return $.ajax({
        type: "POST",
        url: '/Home/SaveFirstInput',
        data: JSON.stringify(input),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        traditional: true,
        success: function (data) {
            if (data.saved) {
                $("#learningForm").replaceWith("<div class='results'> Uzorak tipkanja spremljen je u bazu, sada možete na korak prepoznavanja. </div>");
            }
        },
        error: function () {
            $("#resultBox").append("<p>Došlo je do greške, pokušajte kasnije.</p>");
        }
    });
};