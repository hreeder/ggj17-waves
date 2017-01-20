/**
 * Created by szczocik on 20/01/17.
 */
function getTemplate(templateName, callback) {
    $.ajax({
        url: "/static/templates/" + templateName + ".hbs",
        cache: false,
        success: callback
    });
}

$(function(){
    getTemplate("test", function(tpl_source) {
        var tpl = Handlebars.compile(tpl_source);
        $('#container').html(tpl({
            title: "Test",
            body: "testtesttest"
        }))
    })
});
