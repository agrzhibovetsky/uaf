$(document).ready(function () {
    $(".tbManualDateEntry").keyup(function (ev) {
        var curVal = $(this).val();
        $(this).val(curVal.replace(",", "."));
    });

    if (document.title === "")
        document.title = "Ua Football 3.0";
    else document.title =  document.title + " - Ua Football 3.0"
});


function initMatchLog() {
    $(document).ready(function () {
        $(".trMatchRow").mouseover(function () {
            this.className = this.className.replace("trMatchRow", "trMatchRowHover");
        });
        $(".trMatchRow").mouseout(function () {
            this.className = this.className.replace("trMatchRowHover", "trMatchRow");
        });
        $(".trMatchRow").click(function () {
            $this = $(this);
            var matchId = $this.attr("matchId");
            window.location = "Game.aspx?objectId=" + matchId;
        });
    });
}

function open_colorbox(sender, e) {
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();

    var url = sender.href;
    $.colorbox({ width: "900px", height: "800px", href: url });
}

function open_colorbox_video(e, url) {
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
    $.colorbox({ width: "900px", height: "800px", href: url });
}

function createDropdown(id, style, data, createOptionCallback) {
    var newSelect = document.createElement("select");
    newSelect.id = id;
    newSelect.name = id;
    if (data !== null) {
        for (var i = 0; i < data.length; i++) {
            var option = createOptionCallback(data[i]);
            newSelect.appendChild(option);
        }
    }
    $(newSelect).attr("style", style);
    return newSelect;
}

function updateDropdown(selector, data, createOptionCallback) {
    var select = $(selector)[0];
    var selectLength = select.options.length;
    for (var i = selectLength-1; i >-1; i--) {
        select.options.remove(i);
    }
    for (i = 0; i < data.length; i++) {
        var option = createOptionCallback(data[i]);
        select.options.add(option);
    }
}

function createTextArea(id, rows, style) {
    var newInput = document.createElement("textarea");
    newInput.id = id;
    newInput.name = id;
   // $(newInput).attr("name", id);
    $(newInput).attr("rows", rows);
    $(newInput).attr("style", style);

    return newInput;
}