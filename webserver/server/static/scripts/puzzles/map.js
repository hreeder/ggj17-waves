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
    manipulateMap(msg)
}

//generate the table for the map
// 0, 0 in bottom left corner
function create_table() {

    var table_str = "<table id='table_grid'>";
    for (var i = 14; i >= 0; i--){
        table_str += "<tr>";

        for (var j = 0; j < 15; j++) {
            table_str += "<td><div id='" + j+"-"+i+ "'></div></div></td>";
        }
        table_str += "</tr>";
    }
    table_str += "</table>";
    $('#full_map').append(table_str);
    $('#full_map').show();
}

function manipulateMap(msg){
    //$("td div").css('background','none');
    $("#"+msg.x1+"-"+msg.y1).css('background','red');
    $("#"+msg.x2+"-"+msg.y2).css('background','red');
}