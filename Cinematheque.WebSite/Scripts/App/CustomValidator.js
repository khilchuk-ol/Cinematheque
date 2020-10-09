$.validator.unobtrusive.adapters.addSingleVal("flname", "regex");

$.validator.addMethod("flname", function (value, element, regex) {
    if (value) {
        if (value.match(regex)) {
            return true;
        }
    }
    return false;
});