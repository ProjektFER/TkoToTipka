$(document).ready(function () {

        var measuredInput = [];

        /* 
        * Prevent copy-paste to textbox. 
        */
        $("#userInput").bind("cut copy paste", function (e) {
            e.preventDefault();
        });

 
        /* 
        *  Measure and collect the keystrokes as a person is typing.
        */
        $("#userInput").keydown(function (e_down) {
            //Demo:
            console.log("Keydown:\t" + e_down.key + "\t" + e_down.timeStamp);
            measuredInput.push({
                key_down: e_down.key,
                time_down: e_down.timeStamp
            });
        });
        $("#userInput").keyup(function (e_up) {
            //Demo:
            console.log("Keyup: \t" + e_up.key + "\t" + e_up.timeStamp);
            measuredInput.push({
                key_up: e_up.key,
                time_up: e_up.timeStamp
            });
        });

        $("#submitBtn").click(function () {
            var data = {
                input: measuredInput
            };

            var correctInput = $("#correctInput").text().trim();
            var userInput = $("#userInput").val().trim();            
            var distance = levenshteinAlgorithm(userInput, correctInput);
            var percentage = distance / correctInput.length;

            if (percentage < 0.3) {
                ajaxCall(data);
            } else {
                measuredInput = [];
                $("#resultBox").replaceWith("<p>Vaš unos previše se razlikuje od zadanog teksta. Pokušajte ponovno.</p>");
            }
            return false;
        });
});