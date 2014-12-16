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
        var pressed = (e_down.key != undefined) ? e_down.key : keycode_dictionary[e_down.which];
        //Demo:
        //console.log("Keydown:\t" + pressed + "\t" + Date.now());
        measuredInput.push({
            key_down: pressed,
            time_down: Date.now()
                
        });
    });
    $("#userInput").keyup(function (e_up) {
        var pressed = (e_up.key != undefined) ? e_up.key : keycode_dictionary[e_up.which];
        //Demo:
        //console.log("Keydown:\t" + pressed + "\t" + Date.now());
        measuredInput.push({
            key_up: pressed,
            time_up: Date.now()
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



    keycode_dictionary = {
        //0: "\\",
        8: "Backspace",
        9: "Tab",
        //12: "num",
        13: "Enter",
        16: "Shift",
        17: "Control",
        18: "Alt",
        //19: "pause",
        20: "CapsLock",
        27: "Esc",
        32: "",
        33: "PageUp",
        34: "PageDown",
        35: "End",
        36: "Home",
        37: "Left",
        38: "Up",
        39: "Right",
        40: "Down",
        //44: "print",
        45: "Insert",
        46: "Del",
        48: "0",
        49: "1",
        50: "2",
        51: "3",
        52: "4",
        53: "5",
        54: "6",
        55: "7",
        56: "8",
        57: "9",
        65: "a",
        66: "b",
        67: "c",
        68: "d",
        69: "e",
        70: "f",
        71: "g",
        72: "h",
        73: "i",
        74: "j",
        75: "k",
        76: "l",
        77: "m",
        78: "n",
        79: "o",
        80: "p",
        81: "q",
        82: "r",
        83: "s",
        84: "t",
        85: "u",
        86: "v",
        87: "w",
        88: "x",
        89: "y",
        90: "z",
        91: "cmd",
        92: "cmd",
        93: "cmd",
        96: "0",
        97: "1",
        98: "2",
        99: "3",
        100: "4",
        101: "5",
        102: "6",
        103: "7",
        104: "8",
        105: "9",
        106: "*",
        107: "+",
        108: "Enter",
        109: "-",
        110: ",",
        111: "/",
        112: "F1",
        113: "F2",
        114: "F3",
        115: "F4",
        116: "F5",
        117: "F6",
        118: "F7",
        119: "F8",
        120: "F9",
        121: "F10",
        122: "F11",
        123: "F12",
        //124: "print",
        144: "NumLock",
        //145: "scroll",
        //186: ";",
        186: "č",
        187: "=",
        188: ",",
        189: "-",
        190: ".",
        191: "/",
        192: "`",
        //219: "[",
        219: "š",
        //220: "\\",
        220: "ž",
        //221: "]",
        221: "đ",
        //222: "\'",
        222: "ć",
        223: "`",
        224: "cmd",
        225: "alt",
        57392: "ctrl",
        63289: "num",
        59: ";",
        61: "-",
        173: "="
    };


});