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
    })

    setTimeout(
        function()
        {
            create_wave();
        }, 500);
});


function create_wave() {
    var waves = new SineWaves({
        el: document.getElementById('waves'),
        speed: 8,
        width: function() {
            return $(window).width();
        },
        height: function() {
            return $(window).height();
        },
        wavesWidth: '95%',
        ease: 'SineInOut',
        waves: [
            {
                timeModifier: 1,
                lineWidth: 3,
                amplitude: 150,
                wavelength: 200,
                segmentLength: 20,
                //       strokeStyle: 'rgba(255, 255, 255, 0.5)'
            },
            {
                timeModifier: 1,
                lineWidth: 2,
                amplitude: 150,
                wavelength: 100,
                //       strokeStyle: 'rgba(255, 255, 255, 0.3)'
            },
            {
                timeModifier: 1,
                lineWidth: 1,
                amplitude: -150,
                wavelength: 50,
                segmentLength: 10,
                //       strokeStyle: 'rgba(255, 255, 255, 0.2)'
            },
            {
                timeModifier: 1,
                lineWidth: 0.5,
                amplitude: -100,
                wavelength: 100,
                segmentLength: 10,
                //       strokeStyle: 'rgba(255, 255, 255, 0.1)'
            }
        ],
        initialize: function (){
        },
        resizeEvent: function() {
            var gradient = this.ctx.createLinearGradient(0, 0, this.width, 0);
            gradient.addColorStop(0,"rgba(0, 0, 0, 0)");
            gradient.addColorStop(0.5,"rgba(255, 255, 255, 0.5)");
            gradient.addColorStop(1,"rgba(0, 0, 0, 0)");
            var index = -1;
            var length = this.waves.length;
            while(++index < length){
                this.waves[index].strokeStyle = gradient;
            }
            // Clean Up
            index = void 0;
            length = void 0;
            gradient = void 0;
        }
    });

}
