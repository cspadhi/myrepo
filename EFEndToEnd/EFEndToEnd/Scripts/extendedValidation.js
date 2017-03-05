(function ($) {
    $.validator.addMethod('dropdownrequired', function (value, element, param) {
        if (!value) return false;
        if (parseInt(value.toString()) > 0) {
            return true;
        }
        return false;
    });

    $.validator.unobtrusive.adapters.add(
        'dropdownrequired',
        [],
        function (options) {
            options.rules['dropdownrequired'] = {};
            if (options.message) {
                options.messages['dropdownrequired'] = options.message;
            }
        });
    $.validator.unobtrusive.parse('form');
}(jQuery));