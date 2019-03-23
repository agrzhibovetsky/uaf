function autocompleteTextBox() {
    this.autocompletePath = "";
    this.autocompleteType = "";
    this.autocompleteTextBoxId = "";
    this.autocompleteHiddenFieldId = "";
    this.tb = "";
    this.hf = "";
}

autocompleteTextBox.prototype.getAutocompleteUrl = function () {
    return (this.autocompletePath + "?type="+ this.autocompleteType);
}

autocompleteTextBox.prototype.setAutocompletePath = function (path) {
    this.autocompletePath = path;
}

autocompleteTextBox.prototype.setAutocompleteType = function (type) {
    this.autocompleteType = type;
}

autocompleteTextBox.prototype.setTextBoxId = function (id) {
    this.autocompleteTextBoxId = id;
}

autocompleteTextBox.prototype.setHiddenFiedlId = function (id) {
    this.autocompleteHiddenFieldId = id;
}


autocompleteTextBox.prototype.init = function () {
    this.tb = $("#" + this.autocompleteTextBoxId);
    this.hf = $("#" + this.autocompleteHiddenFieldId);
    var source = this.getAutocompleteUrl();
    
    this.tb.autocomplete();
    this.tb.autocomplete("option", "minLength", 2);
    this.tb.autocomplete("option", "delay", 1500);
    this.tb.autocomplete("option", "source", source);
    this.tb.autocomplete("option", "search", function (e, ui) {

        var matchedName = "";
        var matchedId = "";
        var matchesCount = 0;
        if (this.id.indexOf("actbEventPlayer") > 0 && document.location.href.indexOf("MatchEdit.aspx") > 0)
        {
            var matchedPlayers = [];
            var playersTextboxes = $("[id*='PlayerAutocomplete']");
            for (var i = 0; i < playersTextboxes.length; i++) {
                if ($(playersTextboxes[i]).val().toUpperCase().indexOf($(this).val().toUpperCase()) > 0) {
                    matchedName = $(playersTextboxes[i]).val();
                    matchedId = $("#" + playersTextboxes[i].id.replace("tb", "hf")).val();
                    matchedPlayers.push({ value: matchedName, id: matchedId });
                }
            }

            $(this).autocomplete("option", "source", matchedPlayers);
        }
            
    });
    this.tb.autocomplete("option", "select", function (event, ui) {
        var autocompleteObj = eval(this.id.substring(2, this.id.length));
        autocompleteObj.tb.val(ui.item.value);
		autocompleteObj.tb.css("border", "1px solid black");
        autocompleteObj.hf.val(ui.item.id);
    });
    this.tb.autocomplete("option", "response", function (event, ui) {

        if (ui.content.length == 1) {
            var autocompleteObj = eval(this.id.substring(2, this.id.length));
            autocompleteObj.tb.val(ui.content[0].value);
			autocompleteObj.tb.css("border", "1px solid black");
            autocompleteObj.hf.val(ui.content[0].id);
            autocompleteObj.tb.autocomplete("close");
            autocompleteObj.tb.blur();
            //debugger;
        }

        if (ui.content.length == 0) {
            var autocompleteObj = eval(this.id.substring(2, this.id.length));
            autocompleteObj.tb.css("border", "1px solid red");
            autocompleteObj.hf.val("");
        }
    });

    this.tb.autocomplete("option", "change", function (event, ui) {

        var autocompleteObj = eval(this.id.substring(2, this.id.length));
        if (autocompleteObj.tb[0].value.length == 0) {
            autocompleteObj.hf.val("0");
        }
    });

}



function validateAutocomplete(source, arguments) {

    var hfObj = document.getElementById(source.valueHolderId);
    var tbObj = document.getElementById(source.textHolderId);
    arguments.IsValid = (hfObj.value.length > 0) && (tbObj.value.length > 0);
}