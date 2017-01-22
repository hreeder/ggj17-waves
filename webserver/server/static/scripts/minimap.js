function loadMinimap() {
    getTemplate('minimap', function (tpl_source) {
        var tpl = Handlebars.compile(tpl_source);
        $('#minimap-container').html(tpl());

    });
    getTemplate('puzzle-map', function (map_src) {
        var map_tpl = Handlebars.compile(map_src);
        $('#main-area').html(map_tpl());

    });
};