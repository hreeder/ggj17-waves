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
    startWebsocket();
    getTemplate("puzzle-entry", function(tpl_source) {
        var tpl = Handlebars.compile(tpl_source);
        $('#container').html(tpl({
            title: "Test",
            body: "testtesttest"
        }))
        create_wave();
        $('#waves').css({'border':'2px black solid'});

    })
    addWebsocketCallback('waiting-game', function(data) {

    });

    addWebsocketCallback('ready', function(data) {

    });

    addWebsocketCallback('start-game', function(data) {
        getTemplate("puzzle-entry", function(tpl_source) {
            var tpl = Handlebars.compile(tpl_source);
            $('#container').html(tpl({
                title: "Test",
                body: "testtesttest"
            }));
            setTimeout(function() {
                create_wave();
            }, 500);
        });
    });
    getTemplate("header", function(tpl_source) {
        var tpl = Handlebars.compile(tpl_source);
        $('#header').html(tpl({

        }))
    })


});


function create_wave() {
    var waves = new SineWaves({
        el: document.getElementById('waves'),
        speed: 2,
        width: function() {
            return $(window).width()*0.8;
        },
        height: function() {
            return $(window).height()*0.7;
        },
        wavesWidth: '105%',
        ease: 'Linear',
        waves: [
            {
                timeModifier: 1,
                lineWidth: 3,
                amplitude: 150,
                wavelength: 120,
                segmentLength: 20,
                //       strokeStyle: 'rgba(255, 255, 255, 0.5)'
            },
            {
                timeModifier: 1,
                lineWidth: 2,
                amplitude: 170,
                wavelength: 80,
                strokeStyle: 'rgba(255, 0, 0, 1)'
            },
        ]
    });

}
