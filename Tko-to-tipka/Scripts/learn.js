$(document).ready(function () {

    $("#submitUsernameBtn").click(function () {
        var username = $("#username").val();
        $.ajax({
            type: "POST",
            url: "/Home/SaveUsername",
            data: JSON.stringify({ username: username }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            traditional: true,
            success: function (data, status) {
                if (data.saved) {
                    window.location.href = "/Home/LearningForm/";
                } else {
                    $("#usernameResult").replaceWith("Korisnik već postoji.");
                }
            },
            error: function () { 
                $("#usernameResult").replaceWith("<p>Došlo je do greške, pokušajte ponovno.</p>");
            }
        });
        return false;
    });
});