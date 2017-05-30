define(['knockout', 'postman'], function (ko, postman) { // needed fro require modules
    return function (params) { // needed for knockout components
        let names = ko.observableArray(['Peter', 'Joe', 'John']);
        let message = ko.observable();

        let selectedName = ko.observable();
        
        let selectName = function (element) {
            console.log(element);
            selectedName(element);
        };

        let isSelected = function (name) {
            return name === selectedName();
        };
        
        //postman.subscribe("someEvent", function (data) {
        //    message(data.msg);
        //});

        return {
            names,
            message,
            selectName,
            isSelected
        };
    };
});