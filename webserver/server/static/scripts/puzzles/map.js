function loadMapPuzzle(msg) {
    // getTemplate('puzzle-map', function (tpl_source) {
    //     var tpl = Handlebars.compile(tpl_source);
    //     $('#main-area').html(tpl());
    // });

    load_map(msg);

    addActionCallback('puzzle-map-update', manipulateMap);
}

function load_map(msg){
    create_table();
    // create_canvas();
    manipulateMap(msg)
}

//generate the table for the map
// 0, 0 in bottom left corner
function create_table() {

    var table_str = "<table id='table_grid'>";
    for (var i = 5; i >= 0; i--){
        table_str += "<tr>";

        for (var j = 0; j < 6; j++) {
            table_str += "<td><div id='" + j+"-"+i+ "'></div></div></td>";
        }
        table_str += "</tr>";
    }
    table_str += "</table>";
    $('#full_map').append(table_str);
    $('#full_map').show();
    $('#table_grid td').click(function(){
        var coordinates = $(this).find('div').attr('id').split('-');
        console.log("x: " + coordinates[0] + " y: " + coordinates[1]);
    })
}

function manipulateMap(msg){
    $("td div").removeClass('enemy');
    $("#"+msg.x1+"-"+msg.y1).addClass('enemy');
    $("#"+msg.x2+"-"+msg.y2).addClass('enemy');
}

function create_canvas() {
    var newCanvas =
        $('<canvas/>',{'id':'map_canvas'})
            .width(800)
            .height(800);

    $('#full_map').append(newCanvas);

    var canvas = $('#map_canvas')[0];
    var ctx = canvas.getContext("2d");
    var img = new Image;
    img.onload = function() {
        ctx.drawImage(this, 10,10,0,0,0,0,800,800);
    };
    img.src = "/static/images/map.png";
}