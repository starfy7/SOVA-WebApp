require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/dist/knockout",
        jquery: "lib/jQuery/dist/jquery.min",
        text: "lib/text/text",
        postman: "app/services/postman"
    }
});


require(['knockout'], function (ko) {

    ko.components.register("my-posts", {
        viewModel: { require: "app/components/myposts"},
        template: { require: "text!app/components/myposts.html"}
    });

    ko.components.register("my-list", {
        viewModel: { require: "app/components/mylist" },
        template: { require: "text!app/components/mylist.html" }
    });
   
    ko.applyBindings({
        currentComponent: ko.observable("my-posts"),
        changeComponent: function () {
            if (this.currentComponent() === 'my-list') this.currentComponent("my-posts");
            else this.currentComponent('my-list');
        }
    });
});