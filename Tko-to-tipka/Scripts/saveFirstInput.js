function ajaxCall(input) {
    console.log(input);
    return $.ajax({
        type: "GET",
        url: '/Home/SaveFirstInput',
        data: { data: JSON.stringify(input) },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        traditional: true,
        success: function (data, status) {
            if (data.saved) {
                $("#learningForm").replaceWith("<div class='results'> Uzorak tipkanja spremljen je u bazu, sada možete na korak prepoznavanja. </div>");
            }
        },
        error: function () {
            $("#resultBox").append("<p>Došlo je do greške, pokušajte kasnije.</p>");
        }
    });
};