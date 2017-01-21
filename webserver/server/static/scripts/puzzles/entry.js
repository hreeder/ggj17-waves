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
    addActionCallback('puzzle-entry-wave', manipulateWave);
    addActionCallback('puzzle-entry-correct', function(msg){
        var canvas = document.getElementById('waves'),
            ctx = canvas.getContext('2d');
        ctx.clearRect(0,0,canvas.width, canvas.height);

        passed();
    });
}

function manipulateWave(msg) {
    console.log(msg);
    window.entryTargetAmplitude = msg.amplitude;
    window.entryTargetFrequency = msg.frequency;
    window.entryTargetPhase = msg.phase;
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

function passed() {
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
                strokeStyle: 'rgba(0, 250, 0, 1)',
                type: function(x, waves) {
                    return window.entryAmplitude * Math.sin(window.entryFrequency * x + window.entryPhase);
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