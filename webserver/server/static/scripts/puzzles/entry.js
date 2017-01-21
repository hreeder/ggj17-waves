function loadEntryPuzzle() {
    create_wave();
    $('#waves').css({'border':'2px black solid'});
    addActionCallback('puzzle-entry', manipulateWave);
}

function manipulateWave(action) {
    console.log(action);
}

function create_wave() {
    var waves = new SineWaves({
        el: document.getElementById('waves'),
        speed: 2,
        width: function() {
            return $('#main-area').width();
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
