$(document).ready(function () {
    $(".tbManualDateEntry").keyup(function (ev) {
        var curVal = $(this).val();
        $(this).val(curVal.replace(",", "."));
    });
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