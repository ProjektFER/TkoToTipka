$(document).ready(function () {

    $("#submitUsernameBtn").click(function () {
        var username = $("#username").val();
        $.ajax({
            type: "GET",
            url: "/Home/SaveUsername",
            data: { data: username },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            traditional: true,
            success: function (data, status) {
                if (data.saved) {
                    window.location.href = "/Home/LearningForm/";
                } else {
                    $("#usernameResult").append("Korisnik već postoji.");
                }
            },
            error: function () { 
                $("#usernameResult").append("<p>Došlo je do greške, pokušajte ponovno.</p>");
            }
        });
        return false;
    });
});

