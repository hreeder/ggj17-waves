function loadMinimap() {
    getTemplate('minimap', function(tpl_source){
        var tpl = Handlebars.compile(tpl_source);
        $('#minimap-container').html(tpl());
    });
}