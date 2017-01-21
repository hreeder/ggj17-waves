function loadEntryPuzzle(msg) {
    console.log(msg);
    window.entryAmplitude = 2;
    window.entryFrequency = 2;
    window.entryPhase = 2;

    window.entryTargetAmplitude = msg.amplitude;
    window.entryTargetFrequency = msg.frequency;
    window.entryTargetPhase = msg.phase;

    $('#waves').css({'border':'2px black solid',
                    'background-image':'url("static/images/grid.png")'});

    // Setup
    create_wave();

    // Add callbacks
    addActionCallback('puzzle-entry', manipulateWave);
}

function manipulateWave(msg) {
    console.log(msg);
    window.entryAmplitude = msg.amplitude;
    window.entryFrequency = msg.frequency;
    window.entryPhase = msg.phase;
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
                lineWidth: 2,
                strokeStyle: 'rgba(255, 0, 0, 1)',
                type: function(x, waves) {
                    return window.entryTargetAmplitude * Math.sin(window.entryTargetFrequency * x + window.entryTargetPhase);
                }
            },
            {
                timeModifier: 1,
                lineWidth: 8,
                type: function (x, waves) {
                    return window.entryAmplitude * Math.sin(window.entryFrequency * x + window.entryPhase);
                }
            },
        ]
    });
}
