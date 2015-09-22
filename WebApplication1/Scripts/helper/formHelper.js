var FormHelper = function () {
    return {
        setFormValue: function (data, form) {
            for (var key in data) {
                if (key === 'length' || !data.hasOwnProperty(key)) continue;
                form.find("*[type!='checkbox'][name='" + key + "']").val(data[key]);
                form.find("input[type='checkbox'][name='" + key + "']").prop("checked", data[key] == "true");
            }
        }
    };
}();